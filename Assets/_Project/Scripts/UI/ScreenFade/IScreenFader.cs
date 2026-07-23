using System;

namespace gishadev.walkingSimulator.UI
{
    public interface IScreenFader
    {
        /// <summary>Fade to black, run <paramref name="whileBlack"/> while hidden, then fade back in.</summary>
        void FadeThrough(Action whileBlack, Action onComplete = null);
    }
}
