using System;
using gishadev.tools.Audio;
using UnityEngine;
using VContainer;

namespace gishadev.gmtk.LocationManager
{
    public class ClipRepeater : MonoBehaviour
    {
        [SerializeField] private float timeToRepeat = 5f;
        [SerializeField] private SFXAudioEnum audioToRepeatID;

        [Inject] private IAudioManager _audioManager;

        private float _time;

        private void Start()
        {
            _time = timeToRepeat;
        }

        private void Update()
        {
            if (_time < 0f)
            {
                _time = timeToRepeat;
                _audioManager.PlaySFX(audioToRepeatID);
            }
            else
                _time -= Time.deltaTime;
        }
    }
}