using System.Globalization;
using Cysharp.Threading.Tasks;
using gishadev.walkingSimulator.UI;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace gishadev.gmtk.Countdown
{
    public class CountdownController : IInitializable, ICountdownController
    {
        [Inject] private IScreenFader _screenFader;

        private const float SECONDS_COUNTDOWN = 5;
        
        private CountdownGUIHandler _countdownGUI;

        public void Initialize()
        {
            // Include inactive: the countdown panel typically starts disabled in the scene.
            _countdownGUI = Object.FindFirstObjectByType<CountdownGUIHandler>(UnityEngine.FindObjectsInactive.Include);
            if (_countdownGUI == null)
                UnityEngine.Debug.LogError(
                    "CountdownController: no CountdownGUIHandler found in the scene.");
        }

        // Must be started at start of Game (not round) in game controller.
        public async UniTask StartCountdown()
        {
            _screenFader.FadeInInstant();
            _countdownGUI.Enable();
            await CountdownAsync(SECONDS_COUNTDOWN);
            _countdownGUI.SetText("Find em all!");
            await UniTask.WaitForSeconds(0.5f);
            _countdownGUI.Disable();

            await _screenFader.FadeOut();
        }

        private async UniTask CountdownAsync(float timeToCountdownInSeconds)
        {
            var seconds = timeToCountdownInSeconds;
            _countdownGUI.SetText(timeToCountdownInSeconds.ToString(CultureInfo.InvariantCulture));

            while (seconds > 0)
            {
                await UniTask.WaitForSeconds(1);
                seconds--;
                _countdownGUI.SetText(seconds.ToString(CultureInfo.InvariantCulture));
            }
        }
    }
}