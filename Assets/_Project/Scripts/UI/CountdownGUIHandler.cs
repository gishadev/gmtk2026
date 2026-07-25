using gishadev.gmtk.Core;
using TMPro;
using UnityEngine;

namespace gishadev.walkingSimulator.UI
{
    public class CountdownGUIHandler : MonoBehaviour
    {
        [SerializeField] private TMP_Text countdownLabel;
        [SerializeField] private Animator countdownAnimator;

        public void Enable()
        {
            countdownLabel.gameObject.SetActive(true);
            countdownAnimator.SetBool(Constants.IS_COUNTING_ANIM, true);
        }

        public void Disable()
        {
            countdownLabel.gameObject.SetActive(false);
            countdownAnimator.SetBool(Constants.IS_COUNTING_ANIM, false);
        }

        public void SetText(string text)
        {
            countdownLabel.text = text;
        }
    }
}