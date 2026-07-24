using gishadev.gmtk.Input;
using gishadev.gmtk.LocationManager;
using gishadev.gmtk.MovementManager;
using gishadev.gmtk.Interactions;
using gishadev.gmtk.Core;
using gishadev.gmtk.Countdown;
using gishadev.gmtk.kids;
using gishadev.walkingSimulator.EventsManager;
using gishadev.walkingSimulator.UI;
using UnityEngine;
using VContainer;

namespace gishadev.walkingSimulator.Infrastructure
{
    public class GameLifetimeScope : AutoInjectLifetimeScope
    {
        [SerializeField] private CharacterMovementDataSO characterMovementDataSO;
        [SerializeField] private CharacterInteractionDataSO characterInteractionDataSO;
        [SerializeField] private GameUIDataSO gameUIDataSO;
        [SerializeField] private KidsDataSO kidsDataSO;
        [SerializeField] private GameDataSO gameDataSO;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(characterMovementDataSO);
            builder.RegisterInstance(characterInteractionDataSO);
            builder.RegisterInstance(gameUIDataSO);
            builder.RegisterInstance(kidsDataSO);
            builder.RegisterInstance(gameDataSO);

            builder.Register<IEventBus, EventBus>(Lifetime.Singleton);
            builder.Register<PlayerInputService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<OverlayHintController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ScreenFader>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<LocationController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<LocationTransitionController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<CountdownController>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<GameController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<KidsController>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
