using System;
using gishadev.walkingSimulator.EventsManager;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gishadev.walkingSimulator.UI
{
    public class OverlayHintController : IInitializable, IDisposable, ITickable
    {
        [Inject] private GameUIDataSO _gameUIDataSO;
        [Inject] private IObjectResolver _objectResolver;
        [Inject] private IEventBus _eventBus;
        
        private OverlayHintHandler _overlayHintHandler;
        private bool _overlayDisplaying;
        private Transform _currentTargetTrans;
        
        public void Initialize()
        {
            InitOverlayHintObject();
            _eventBus.Subscribe<HintRequestedEvent>(OnHintRequested);
            _eventBus.Subscribe<HintDismissedEvent>(OnHintDismissed);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<HintRequestedEvent>(OnHintRequested);
            _eventBus.Unsubscribe<HintDismissedEvent>(OnHintDismissed);
        }

        public void Tick()
        {
            if (!_overlayDisplaying || _currentTargetTrans == null)
                return;
            
            _overlayHintHandler.UpdateOverlayPosition(_currentTargetTrans);
        }

        private void InitOverlayHintObject()
        {
            _overlayHintHandler = _objectResolver.Instantiate(_gameUIDataSO.OverlayHintPrefab)
                .GetComponent<OverlayHintHandler>();
            _overlayHintHandler.Hide();
        }

        private void OnHintRequested(HintRequestedEvent obj)
        {
            _overlayDisplaying = true;
            _overlayHintHandler.UpdateOverlayText(obj.Text);
            _overlayHintHandler.UpdateOverlayPosition(obj.TargetTrans);
            _currentTargetTrans = obj.TargetTrans;
            _overlayHintHandler.Show();
        }
        
        private void OnHintDismissed(HintDismissedEvent obj)
        {
            _overlayDisplaying = false;
            _currentTargetTrans = null;
            _overlayHintHandler.Hide();
        }
    }
}