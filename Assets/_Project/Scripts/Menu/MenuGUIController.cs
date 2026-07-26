using gishadev.gmtk.Core;
using gishadev.tools.Audio;
using gishadev.tools.SceneLoading;
using gishadev.tools.UI;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace gishadev.gmtk.Menu
{
    public class MenuGUIController : MenuController
    {
        [SerializeField] private Page settingsPopupPage;
        [SerializeField] private Page creditsPopupPage;
        [SerializeField] private Button quitButton;

        [Inject] private ISceneLoader _sceneLoader;
        [Inject] private IAudioManager _audioManager;
        
        protected override void Awake()
        {
            base.Awake();
            
            if (Application.platform == RuntimePlatform.WebGLPlayer)
                quitButton.gameObject.SetActive(false);
        }

        public void OnPlayClicked()
        {
            _sceneLoader.AsyncSceneLoad(Constants.INTRO_SCENE_NAME);
        }

        public void OnQuitClicked()
        {
            Application.Quit();
        }
        
        public void Tick()
        {
            _audioManager.PlaySFX(SFXAudioEnum.TICK);
        }
    }
}