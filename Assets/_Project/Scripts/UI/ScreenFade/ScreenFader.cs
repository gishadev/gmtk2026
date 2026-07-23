using System;
using VContainer;
using VContainer.Unity;

namespace gishadev.walkingSimulator.UI
{
    public class ScreenFader : IInitializable, IScreenFader
    {
        [Inject] private GameUIDataSO _gameUIDataSO;
        [Inject] private IObjectResolver _objectResolver;

        private ScreenFaderHandler _handler;

        public void Initialize()
        {
            _handler = _objectResolver.Instantiate(_gameUIDataSO.ScreenFaderPrefab)
                .GetComponent<ScreenFaderHandler>();
            _handler.SetBlack(false);
        }

        public void FadeThrough(Action whileBlack, Action onComplete = null) =>
            _handler.FadeThrough(whileBlack, onComplete);
    }
}
