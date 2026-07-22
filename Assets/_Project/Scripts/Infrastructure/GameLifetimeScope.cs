using gishadev.walkingSimulator.MovementManager;
using gishadev.walkingSimulator.Core;
using gishadev.walkingSimulator.EventsManager;
using gishadev.walkingSimulator.InteractionManager;
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
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(characterMovementDataSO);
            builder.RegisterInstance(characterInteractionDataSO);
            builder.RegisterInstance(gameUIDataSO);
            
            builder.Register<IEventBus, EventBus>(Lifetime.Singleton);
            builder.Register<PlayerInputService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<OverlayHintController>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}
