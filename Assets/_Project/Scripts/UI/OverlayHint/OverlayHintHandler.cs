using System;
using TMPro;
using UnityEngine;

namespace gishadev.walkingSimulator.UI
{
    public class OverlayHintHandler : MonoBehaviour
    {
        [SerializeField] private GameObject overlayHintContent;
        [SerializeField] private TMP_Text overlayTMP;

        private Camera _cam;

        private void Awake()
        {
            _cam = Camera.main;
        }

        public void UpdateOverlayText(string newText)
        {
            overlayTMP.text = newText;
        }

        public void UpdateOverlayPosition(Transform targetTrans)
        {
            var screenPos = _cam.WorldToScreenPoint(targetTrans.position);
            overlayTMP.rectTransform.position = screenPos;
        }
        
        public void Show()
        {
            overlayHintContent.SetActive(true);
        }

        public void Hide()
        {
            overlayHintContent.SetActive(false);
        }
    }
}