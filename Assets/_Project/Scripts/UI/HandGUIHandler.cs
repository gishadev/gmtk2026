using System;
using gishadev.gmtk.Core;
using gishadev.walkingSimulator.EventsManager;
using UnityEngine;
using VContainer;

namespace gishadev.walkingSimulator.UI
{
    [RequireComponent(typeof(Animator))]
    public class HandGUIHandler : MonoBehaviour
    {
        [Inject] private IEventBus _eventBus;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _eventBus.Subscribe<SeekRadiusWithKidChangedEvent>(SeekRadiusWithKidChanged);
        }

        private void OnDisable()
        {
            _eventBus.Unsubscribe<SeekRadiusWithKidChangedEvent>(SeekRadiusWithKidChanged);
        }

        private void SeekRadiusWithKidChanged(SeekRadiusWithKidChangedEvent obj)
        {
            _animator.SetBool(Constants.IS_RAISED_HAND_ANIM, obj.IsInRadius);
        }
    }
}