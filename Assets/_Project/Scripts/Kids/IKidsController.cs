using System;
using System.Collections.Generic;

namespace gishadev.gmtk.kids
{
    public interface IKidsController
    {
        /// <summary>Raised each time a hiding kid is found (arg is the new remaining-to-find count).</summary>
        event Action<int> KidFound;

        /// <summary>Raised once every kid has been found.</summary>
        event Action AllKidsFound;

        bool IsSeeking { get; }
        int RemainingToFind { get; }
        IEnumerable<Kid> HidingKids { get; }

        void SpawnAndHideKids();
        void BeginSeeking();
        void NotifyKidFound(Kid kid);
    }
}
