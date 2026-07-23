using System;
using PrimeTween;
using UnityEngine;

namespace gishadev.walkingSimulator.UI
{
    /// <summary>
    /// Full-screen black overlay. Fades to black, runs a callback while hidden, then fades back in.
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class ScreenFaderHandler : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float fadeDuration = 0.4f;

        private void Reset() => canvasGroup = GetComponent<CanvasGroup>();

        public void SetBlack(bool black)
        {
            canvasGroup.alpha = black ? 1f : 0f;
            canvasGroup.blocksRaycasts = black;
        }

        public Sequence FadeThrough(Action whileBlack, Action onComplete)
        {
            return Sequence.Create()
                .ChainCallback(() => canvasGroup.blocksRaycasts = true)
                .Chain(Tween.Alpha(canvasGroup, 1f, fadeDuration))
                .ChainCallback(() => whileBlack?.Invoke())
                .Chain(Tween.Alpha(canvasGroup, 0f, fadeDuration))
                .ChainCallback(() =>
                {
                    canvasGroup.blocksRaycasts = false;
                    onComplete?.Invoke();
                });
        }
    }
}
