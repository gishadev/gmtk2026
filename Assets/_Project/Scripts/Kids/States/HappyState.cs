using gishadev.tools.StateMachine;

namespace gishadev.gmtk.kids.States
{
    /// <summary>
    /// Kid has been caught for good and stops moving.
    /// </summary>
    public class HappyState : IState
    {
        private readonly Kid _kid;

        public HappyState(Kid kid) => _kid = kid;

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _kid.AI.Stop();
        }

        public void OnExit()
        {
        }
    }
}
