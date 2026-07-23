using gishadev.tools.StateMachine;

namespace gishadev.gmtk.kids.States
{
    /// <summary>
    /// Kid has spawned and is waiting to be assigned a hiding spot.
    /// </summary>
    public class IdleState : IState
    {
        private readonly Kid _kid;

        public IdleState(Kid kid) => _kid = kid;

        public void Tick()
        {
        }

        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }
    }
}
