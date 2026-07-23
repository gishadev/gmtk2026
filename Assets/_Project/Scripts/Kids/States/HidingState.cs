using gishadev.tools.StateMachine;

namespace gishadev.gmtk.kids.States
{
    /// <summary>
    /// Kid walks to its assigned spot and stays hidden until found.
    /// </summary>
    public class HidingState : IState
    {
        private readonly Kid _kid;

        public HidingState(Kid kid) => _kid = kid;

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _kid.OnEnteredHiding();
            _kid.AI.MoveToPOI(_kid.AssignedSpot);
        }

        public void OnExit()
        {
        }
    }
}
