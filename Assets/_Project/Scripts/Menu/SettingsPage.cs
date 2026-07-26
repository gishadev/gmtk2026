using System.Globalization;
using gishadev.gmtk.Core;
using gishadev.tools.Audio;
using gishadev.tools.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace gishadev.gmtk.Menu
{
    public class SettingsPage : Page
    {
        [SerializeField] private Slider masterVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;
        [SerializeField] private Slider ambientVolumeSlider;
        [SerializeField] private Slider mouseSensitivitySlider;
        [SerializeField] private Toggle mobileControlsToggle;

        [SerializeField] private TMP_Text masterVolumeLabel;
        [SerializeField] private TMP_Text sfxVolumeLabel;
        [SerializeField] private TMP_Text ambientVolumeLabel;
        [SerializeField] private TMP_Text mouseSensitivityVolumeLabel;
        
        [Inject] private IAudioManager _audioManager;
        
        private void OnEnable()
        {
            mobileControlsToggle.SetIsOnWithoutNotify(false);
            masterVolumeSlider.SetValueWithoutNotify(_audioManager.MasterVolumePercentage);
            sfxVolumeSlider.SetValueWithoutNotify(_audioManager.SFXVolumePercentage);
            ambientVolumeSlider.SetValueWithoutNotify(_audioManager.MusicVolumePercentage);
            mouseSensitivitySlider.SetValueWithoutNotify(GameSettings.MouseSensitivity);

            masterVolumeLabel.text = _audioManager.MasterVolumePercentage.ToString(CultureInfo.InvariantCulture);
            sfxVolumeLabel.text = _audioManager.SFXVolumePercentage.ToString(CultureInfo.InvariantCulture);
            ambientVolumeLabel.text = _audioManager.MusicVolumePercentage.ToString(CultureInfo.InvariantCulture);
            mouseSensitivityVolumeLabel.text = GameSettings.MouseSensitivity.ToString(CultureInfo.InvariantCulture);
            
            masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
            sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
            ambientVolumeSlider.onValueChanged.AddListener(OnAmbientVolumeChanged);
            mouseSensitivitySlider.onValueChanged.AddListener(OnMouseSensitivityChanged);
            mobileControlsToggle.onValueChanged.AddListener(OnMobileControlsChanged);
        }

        private void OnDisable()
        {
            masterVolumeSlider.onValueChanged.RemoveListener(OnMasterVolumeChanged);
            sfxVolumeSlider.onValueChanged.RemoveListener(OnSFXVolumeChanged);
            ambientVolumeSlider.onValueChanged.RemoveListener(OnAmbientVolumeChanged);
            mouseSensitivitySlider.onValueChanged.RemoveListener(OnMouseSensitivityChanged);
            mobileControlsToggle.onValueChanged.RemoveListener(OnMobileControlsChanged);
        }

        private void OnMasterVolumeChanged(float value)
        {
            _audioManager.SetMasterVolume(value);
            masterVolumeLabel.text = _audioManager.MasterVolumePercentage.ToString(CultureInfo.InvariantCulture);
        }

        private void OnSFXVolumeChanged(float value)
        {
            _audioManager.SetSFXVolume(value);
            sfxVolumeLabel.text = _audioManager.SFXVolumePercentage.ToString(CultureInfo.InvariantCulture);
        }

        private void OnAmbientVolumeChanged(float value)
        {
            _audioManager.SetMusicVolume(value);
            ambientVolumeLabel.text = _audioManager.MusicVolumePercentage.ToString(CultureInfo.InvariantCulture);
        }

        private void OnMouseSensitivityChanged(float value)
        {
            GameSettings.MouseSensitivity = value;
            mouseSensitivityVolumeLabel.text = GameSettings.MouseSensitivity.ToString(CultureInfo.InvariantCulture);
        }

        private void OnMobileControlsChanged(bool arg0)
        {
            // throw new NotImplementedException();
        }
    }
}