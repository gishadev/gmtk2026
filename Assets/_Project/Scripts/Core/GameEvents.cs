using gishadev.walkingSimulator.EventsManager;

namespace gishadev.gmtk.Core
{
    /// <summary>Raised when the player steps into a location's exit zone to move on.</summary>
    public class LocationExitRequestedEvent : GameEvent
    {
    }

    public class SeekRadiusWithKidChangedEvent : GameEvent
    {
        public bool IsInRadius { get; private set; }

        public SeekRadiusWithKidChangedEvent(bool isInRadius)
        {
            IsInRadius = isInRadius;
        }
    }

    public class KidFoundEvent : GameEvent
    {
        public int KidsToFindCount { get; private set; }

        public KidFoundEvent(int kidsToFindCount)
        {
            KidsToFindCount = kidsToFindCount;
        }
    }
}