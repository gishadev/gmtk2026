using gishadev.gmtk.Core;
using gishadev.gmtk.kids;
using gishadev.walkingSimulator.EventsManager;
using UnityEditor;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gishadev.gmtk.Editor
{
    /// <summary>
    /// Play-mode test actions under the Tools menu that poke live DI services.
    /// </summary>
    public static class GmtkTestMenu
    {
        private const string Root = "Tools/GMTK Test/";

        [MenuItem(Root + "Change To Next Location (Instant)")]
        private static void ChangeToNextLocation()
        {
            if (TryResolve(out ILocationController locationController))
                locationController.ChangeToNextLocation();
        }

        [MenuItem(Root + "Change To Next Location (With Fade)")]
        private static void ChangeToNextLocationFaded()
        {
            if (TryResolve(out IEventBus eventBus))
                eventBus.Publish(new LocationExitRequestedEvent());
        }

        [MenuItem(Root + "Begin Seeking Now")]
        private static void BeginSeeking()
        {
            if (TryResolve(out IKidsController kidsController))
                kidsController.BeginSeeking();
        }

        [MenuItem(Root + "Change To Next Location (Instant)", true)]
        [MenuItem(Root + "Change To Next Location (With Fade)", true)]
        [MenuItem(Root + "Begin Seeking Now", true)]
        private static bool ValidatePlaying() => Application.isPlaying;

        private static bool TryResolve<T>(out T service)
        {
            service = default;

            var scope = Object.FindAnyObjectByType<LifetimeScope>();
            if (scope == null || scope.Container == null)
            {
                Debug.LogWarning("GMTK Test: no active LifetimeScope found. Enter Play mode first.");
                return false;
            }

            service = scope.Container.Resolve<T>();
            return true;
        }
    }
}
