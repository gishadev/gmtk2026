using gishadev.tools.StateMachine;

namespace gishadev.gmtk.kids.States
{
    /// <summary>
    /// A found kid flees to its next-location POI; once it arrives it leaves the game.
    /// </summary>
    public class RunningToNextLocationState : IState
    {
        private readonly Kid _kid;

        public RunningToNextLocationState(Kid kid) => _kid = kid;

        public void Tick()
        {
            if (_kid.AI.ReachedDestination)
                _kid.Escape();
        }

        public void OnEnter()
        {
            _kid.OnEnteredRunning();
            _kid.AI.MoveToPOI(_kid.AssignedSpot);
        }

        public void OnExit()
        {
        }
    }
}
