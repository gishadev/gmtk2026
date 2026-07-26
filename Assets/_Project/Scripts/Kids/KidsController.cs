using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using gishadev.gmtk.LocationManager;
using gishadev.gmtk.Core;
using gishadev.walkingSimulator.EventsManager;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace gishadev.gmtk.kids
{
    public class KidsController : IInitializable, IDisposable, IKidsController
    {
        public event Action AllKidsFound;

        public bool IsSeeking { get; private set; }
        public int RemainingToFind { get; private set; }
        public IEnumerable<Kid> HidingKids => _kids.Where(k => k != null && k.IsFindable);

        private readonly IObjectResolver _objectResolver;
        private readonly KidsDataSO _kidsData;
        private readonly ILocationController _locationController;
        private readonly IEventBus _eventBus;

        private readonly List<Kid> _kids = new();
        private readonly List<KidHidingSpot> _spots = new();
        private readonly HashSet<Kid> _designatedFleers = new();

        public KidsController(IObjectResolver objectResolver, KidsDataSO kidsData,
            ILocationController locationController, IEventBus eventBus)
        {
            _objectResolver = objectResolver;
            _kidsData = kidsData;
            _locationController = locationController;
            _eventBus = eventBus;
        }

        public void Initialize()
        {
        }

        public void Dispose()
        {
            foreach (var kid in _kids)
                if (kid != null)
                    kid.Escaped -= OnKidEscaped;
        }

        public void SpawnAndHideKids()
        {
            ClearKids();
            IsSeeking = false;

            var location = _locationController.CurrentLocation;
            if (location == null)
            {
                Debug.LogWarning("KidsController: no current location loaded; cannot spawn kids.");
                return;
            }

            _spots.Clear();
            if (location.HidingSpots != null)
                _spots.AddRange(location.HidingSpots);

            var count = Mathf.Min(_kidsData.KidsCount, _spots.Count);
            if (count < _kidsData.KidsCount)
                Debug.LogWarning(
                    $"KidsController: requested {_kidsData.KidsCount} kids but only {_spots.Count} hiding spots exist. Spawning {count}.");

            var factory = new KidsFactory(_objectResolver, _kidsData);

            for (var i = 0; i < count; i++)
            {
                var spot = GetRandomFreeSpot();
                if (spot == null)
                    break;

                spot.Occupy();
                // Exactly one Adam is spawned; all other hiding spots are taken by regular kids.
                var kid = i == 0
                    ? factory.CreateAdam(spot.transform.position)
                    : factory.Create(spot.transform.position);
                kid.Escaped += OnKidEscaped;
                kid.HideAt(spot);
                _kids.Add(kid);
            }

            RemainingToFind = _kids.Count;
            AssignFleeRoles();
        }

        // Decide up-front (and at random) which kids will flee once found, so the outcome
        // no longer depends on the order in which the player finds them. The counts stay the
        // same as before: FleeAmount kids flee in total, and Adam is always one of them.
        private void AssignFleeRoles()
        {
            _designatedFleers.Clear();

            // With no onward location, every kid is caught and made happy.
            if (GetNextLocationPOI() == null)
                return;

            var adam = _kids.FirstOrDefault(k => k is Adam);
            if (adam != null)
                _designatedFleers.Add(adam);

            var fleeAmount = _locationController.CurrentLocationData?.FleeAmount ?? 0;
            var regulars = _kids
                .Where(k => k != null && !(k is Adam))
                .OrderBy(_ => UnityEngine.Random.value)
                .ToList();

            var regularFleers = Mathf.Clamp(fleeAmount - _designatedFleers.Count, 0, regulars.Count);
            for (var i = 0; i < regularFleers; i++)
                _designatedFleers.Add(regulars[i]);
        }

        public async void BeginSeeking()
        {
            IsSeeking = true;
            await UniTask.Yield();
            _eventBus.Publish(new KidFoundEvent(RemainingToFind));
        }

        public void NotifyKidFound(Kid kid)
        {
            if (!IsSeeking || kid == null || !kid.IsFindable)
                return;

            RemainingToFind--;
            kid.AssignedSpot?.Free();

            // Flee vs. happy is decided at spawn time (see AssignFleeRoles), so the order the
            // player finds kids in doesn't affect who runs away.
            var nextLocation = GetNextLocationPOI();
            if (nextLocation != null && _designatedFleers.Contains(kid))
                kid.FleeTo(nextLocation);
            else
                kid.MakeHappy();

            _eventBus.Publish(new KidFoundEvent(RemainingToFind));

            if (RemainingToFind <= 0)
                AllKidsFound?.Invoke();
        }

        private void OnKidEscaped(Kid kid)
        {
            kid.Escaped -= OnKidEscaped;
            _kids.Remove(kid);
            if (kid != null)
                Object.Destroy(kid.gameObject);
        }

        private void ClearKids()
        {
            foreach (var kid in _kids)
            {
                if (kid == null)
                    continue;

                kid.Escaped -= OnKidEscaped;
                Object.Destroy(kid.gameObject);
            }

            _kids.Clear();
        }

        private KidHidingSpot GetRandomFreeSpot()
        {
            var free = _spots.Where(s => !s.IsOccupied).ToList();
            return free.Count == 0 ? null : free[UnityEngine.Random.Range(0, free.Count)];
        }

        private IPOI GetNextLocationPOI()
        {
            var current = _locationController.CurrentLocation;
            return current != null ? current.NextLocationPOI : null;
        }
    }
}