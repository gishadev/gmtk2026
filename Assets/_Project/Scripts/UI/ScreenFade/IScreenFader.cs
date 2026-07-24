using System;
using Cysharp.Threading.Tasks;

namespace gishadev.walkingSimulator.UI
{
    public interface IScreenFader
    {
        /// <summary>Fade to black, run <paramref name="whileBlack"/> while hidden, then fade back in.</summary>
        void FadeThrough(Action whileBlack, Action onComplete = null);

        /// <summary>Fade the screen to black and stay there. Awaitable.</summary>
        UniTask FadeIn();

        /// <summary>Fade the black overlay back out to reveal the scene. Awaitable.</summary>
        UniTask FadeOut();

        /// <summary>Immediately set the screen to black (no tween).</summary>
        void FadeInInstant();

        /// <summary>Immediately clear the black overlay to reveal the scene (no tween).</summary>
        void FadeOutInstant();
    }
}
