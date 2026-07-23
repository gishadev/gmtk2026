using System;
using gishadev.gmtk.kids.States;
using gishadev.tools.StateMachine;
using UnityEngine;

namespace gishadev.gmtk.kids
{
    [RequireComponent(typeof(KidAI))]
    public class Kid : MonoBehaviour
    {
        /// <summary>Raised once the kid reaches its next-location POI and leaves the game.</summary>
        public event Action<Kid> Escaped;

        public KidAI AI { get; private set; }
        public IPOI AssignedSpot { get; private set; }
        public bool IsHiding => _stateMachine != null && _stateMachine.CurrentState is HidingState;

        /// <summary>True while the kid is hidden and not already reacting to being found.</summary>
        public bool IsFindable => IsHiding && !_fleeRequested && !_happyRequested;

        private StateMachine _stateMachine;

        private bool _hideRequested;
        private bool _fleeRequested;
        private bool _happyRequested;
        private bool _escaped;

        private void Awake()
        {
            AI = GetComponent<KidAI>();
        }

        private void Start()
        {
            InitStateMachine();
        }

        private void Update()
        {
            _stateMachine.Tick();
        }

        /// <summary>
        /// Assigns a spot and sends the kid to hide there.
        /// </summary>
        public void HideAt(IPOI spot)
        {
            AssignedSpot = spot;
            _hideRequested = true;
        }

        /// <summary>
        /// Assigns a next-location POI and makes the kid flee to it (then disappear on arrival).
        /// </summary>
        public void FleeTo(IPOI nextLocation)
        {
            AssignedSpot = nextLocation;
            _fleeRequested = true;
        }

        /// <summary>
        /// Kid has been caught for good.
        /// </summary>
        public void MakeHappy()
        {
            _happyRequested = true;
        }

        // --- Called by states ---
        public void OnEnteredHiding() => _hideRequested = _fleeRequested = _happyRequested = false;
        public void OnEnteredRunning() => _fleeRequested = false;

        /// <summary>Kid reached its next-location POI; fire once so it can be despawned.</summary>
        public void Escape()
        {
            if (_escaped)
                return;

            _escaped = true;
            Escaped?.Invoke(this);
        }

        private void InitStateMachine()
        {
            _stateMachine = new StateMachine();

            var idle = new IdleState(this);
            var hiding = new HidingState(this);
            var happy = new HappyState(this);
            var runningToNextLocation = new RunningToNextLocationState(this);

            // Assigned a spot -> go hide.
            At(idle, hiding, () => _hideRequested);
            // Found while plenty remain -> flee to the next-location POI.
            At(hiding, runningToNextLocation, () => _fleeRequested);
            // Found while few remain -> caught for good.
            At(hiding, happy, () => _happyRequested);

            _stateMachine.SetState(idle);

            void At(IState from, IState to, Func<bool> cond) => _stateMachine.AddTransition(from, to, cond);
        }
    }
}
