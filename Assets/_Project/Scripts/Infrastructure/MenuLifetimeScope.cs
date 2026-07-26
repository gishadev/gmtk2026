using VContainer;

namespace gishadev.walkingSimulator.Infrastructure
{
    public class MenuLifetimeScope : AutoInjectLifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            // Menu-specific registrations only. SceneLoader / AudioManager come from the root.
        }
    }
}
