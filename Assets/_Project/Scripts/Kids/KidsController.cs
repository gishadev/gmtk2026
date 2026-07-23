using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace gishadev.gmtk.kids
{
    public class KidsController : IInitializable, IDisposable, IKidsController
    {
        public event Action<int> KidFound;
        public event Action AllKidsFound;

        public bool IsSeeking { get; private set; }
        public int RemainingToFind { get; private set; }
        public IEnumerable<Kid> HidingKids => _kids.Where(k => k != null && k.IsFindable);

        private readonly IObjectResolver _objectResolver;
        private readonly KidsDataSO _kidsData;

        private readonly List<Kid> _kids = new();
        private readonly List<KidHidingSpot> _spots = new();
        private readonly List<NextLocationPOI> _nextLocations = new();

        public KidsController(IObjectResolver objectResolver, KidsDataSO kidsData)
        {
            _objectResolver = objectResolver;
            _kidsData = kidsData;
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
            _spots.Clear();
            _spots.AddRange(Object.FindObjectsByType<KidHidingSpot>(FindObjectsSortMode.None));

            _nextLocations.Clear();
            _nextLocations.AddRange(Object.FindObjectsByType<NextLocationPOI>(FindObjectsSortMode.None));

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
                var kid = factory.Create(spot.transform.position);
                kid.Escaped += OnKidEscaped;
                kid.HideAt(spot);
                _kids.Add(kid);
            }

            RemainingToFind = _kids.Count;
        }

        public void BeginSeeking() => IsSeeking = true;

        public void NotifyKidFound(Kid kid)
        {
            if (!IsSeeking || kid == null || !kid.IsFindable)
                return;

            RemainingToFind--;

            // The kid leaves its hiding spot either way.
            kid.AssignedSpot?.Free();

            var nextLocation = GetRandomNextLocation();
            if (RemainingToFind >= _kidsData.HappyCount && nextLocation != null)
                // Plenty remain -> flee to a next-location POI and disappear on arrival.
                kid.FleeTo(nextLocation);
            else
                // Few remain (or nowhere to flee) -> caught for good.
                kid.MakeHappy();

            KidFound?.Invoke(RemainingToFind);

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

        private KidHidingSpot GetRandomFreeSpot()
        {
            var free = _spots.Where(s => !s.IsOccupied).ToList();
            return free.Count == 0 ? null : free[UnityEngine.Random.Range(0, free.Count)];
        }

        private NextLocationPOI GetRandomNextLocation()
        {
            return _nextLocations.Count == 0
                ? null
                : _nextLocations[UnityEngine.Random.Range(0, _nextLocations.Count)];
        }
    }
}
