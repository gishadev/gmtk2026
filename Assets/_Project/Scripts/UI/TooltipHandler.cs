using System;
using gishadev.gmtk.kids;
using gishadev.gmtk.LocationManager;
using TMPro;
using UnityEngine;
using VContainer;

namespace gishadev.walkingSimulator.UI
{
    public class TooltipHandler : MonoBehaviour
    {
        [SerializeField] private TMP_Text label;
        
        [SerializeField, TextArea(5,10)] private string defaultText;
        [SerializeField, TextArea(5,10)] private string foundText;

        [Inject] private IKidsController _kidsController;
        [Inject] private ILocationController _locationController;
        
        private void OnEnable()
        {
            _kidsController.AllKidsFound += OnAllKidsFound;
            _locationController.LocationLoaded += OnLocationLoaded;
        }

        private void OnDisable()
        {
            _kidsController.AllKidsFound -= OnAllKidsFound;
            _locationController.LocationLoaded -= OnLocationLoaded;
        }
        
        private void OnLocationLoaded(Location location)
        {
            label.text = defaultText;
        }

        private void OnAllKidsFound()
        {
            label.text = foundText;
        }
    }
}