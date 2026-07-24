using System;
using System.Collections.Generic;

namespace gishadev.gmtk.kids
{
    public interface IKidsController
    {
        event Action AllKidsFound;

        bool IsSeeking { get; }
        int RemainingToFind { get; }
        IEnumerable<Kid> HidingKids { get; }

        void SpawnAndHideKids();
        void BeginSeeking();
        void NotifyKidFound(Kid kid);
    }
}
