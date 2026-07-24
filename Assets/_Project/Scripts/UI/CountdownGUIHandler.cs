using TMPro;
using UnityEngine;

namespace gishadev.walkingSimulator.UI
{
    public class CountdownGUIHandler : MonoBehaviour
    {
        [SerializeField] private TMP_Text countdownLabel;

        public void Enable()
        {
            countdownLabel.gameObject.SetActive(true);
        }

        public void Disable()
        {
            countdownLabel.gameObject.SetActive(false);
        }
        
        public void SetText(string text)
        {
            countdownLabel.text = text;
        }
    }
}