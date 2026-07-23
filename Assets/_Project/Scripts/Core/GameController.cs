using System;
using gishadev.gmtk.kids;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gishadev.gmtk.Core
{
    public class GameController : IGameController, IInitializable, ITickable, IDisposable
    {
        private enum Phase
        {
            Countdown,
            Seeking,
            Won
        }

        [Inject] private IKidsController _kidsController;
        [Inject] private KidsDataSO _kidsData;

        private Phase _phase;
        private float _countdown;

        public void Initialize()
        {
            // Kids start hiding to hiding spots poi's (only one kid at one poi)
            _kidsController.SpawnAndHideKids();
            _kidsController.AllKidsFound += OnAllKidsFound;

            _countdown = _kidsData.HideCountdown;
            _phase = Phase.Countdown;
        }

        public void Tick()
        {
            if (_phase != Phase.Countdown)
                return;

            // When countdown is over seeker (player) is seeking kids.
            _countdown -= Time.deltaTime;
            if (_countdown <= 0f)
            {
                _phase = Phase.Seeking;
                _kidsController.BeginSeeking();
            }
        }

        private void OnAllKidsFound()
        {
            _phase = Phase.Won;
            Debug.Log("GameController: all kids found - round won!");
        }

        public void Dispose()
        {
            _kidsController.AllKidsFound -= OnAllKidsFound;
        }
    }
}
