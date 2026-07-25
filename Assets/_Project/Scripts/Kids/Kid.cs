using System;
using gishadev.gmtk.kids.States;
using gishadev.tools.StateMachine;
using Pathfinding;
using UnityEngine;

namespace gishadev.gmtk.kids
{
    [RequireComponent(typeof(FollowerEntity))]
    public class Kid : MonoBehaviour
    {
        /// <summary>Raised once the kid reaches its next-location POI and leaves the game.</summary>
        public event Action<Kid> Escaped;

        public bool ReachedDestination => _followerEntity.reachedDestination;
        public IPOI AssignedSpot { get; private set; }
        public bool IsHiding => _stateMachine != null && _stateMachine.CurrentState is HidingState;

        /// <summary>True while the kid is hidden and not already reacting to being found.</summary>
        public bool IsFindable => IsHiding && !_fleeRequested && !_happyRequested;

        public IState CurrentState => _stateMachine.CurrentState;

        private StateMachine _stateMachine;
        private FollowerEntity _followerEntity;

        private bool _hideRequested;
        private bool _fleeRequested;
        private bool _happyRequested;
        private bool _escaped;

        private void Awake()
        {
            _followerEntity = GetComponent<FollowerEntity>();
        }

        private void Start()
        {
            InitStateMachine();
        }

        private void Update()
        {
            _stateMachine.Tick();
        }

        private void InitStateMachine()
        {
            _stateMachine = new StateMachine();

            var idle = new IdleState(this);
            var hiding = new HidingState(this);
            var happy = new HappyState(this);
            var runningToNextLocation = new RunningToNextLocationState(this);

            At(idle, hiding, () => _hideRequested);
            At(hiding, runningToNextLocation, () => _fleeRequested);
            At(hiding, happy, () => _happyRequested);

            _stateMachine.SetState(idle);

            void At(IState from, IState to, Func<bool> cond) => _stateMachine.AddTransition(from, to, cond);
        }

        public void HideAt(IPOI spot)
        {
            AssignedSpot = spot;
            _hideRequested = true;
        }

        /// <summary>Sends the kid fleeing to a next-location POI, where it disappears on arrival.</summary>
        public void FleeTo(IPOI nextLocation)
        {
            AssignedSpot = nextLocation;
            _fleeRequested = true;
        }

        public void MakeHappy()
        {
            _happyRequested = true;
        }

        // --- Movement ---
        public void MoveToPOI(IPOI poi) => _followerEntity.destination = poi.transform.position;
        public void Stop() => _followerEntity.destination = transform.position;

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
    }
}