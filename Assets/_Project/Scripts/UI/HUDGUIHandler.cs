using gishadev.gmtk.Core;
using gishadev.walkingSimulator.EventsManager;
using TMPro;
using UnityEngine;
using VContainer;

namespace gishadev.walkingSimulator.UI
{
    public class HUDGUIHandler : MonoBehaviour
    {
        [SerializeField] private TMP_Text childCountTMP;
        [Inject] private IEventBus _eventBus;
        private void OnEnable() => _eventBus.Subscribe<KidFoundEvent>(OnKidFound);
        private void OnDisable() => _eventBus.Unsubscribe<KidFoundEvent>(OnKidFound);
        private void OnKidFound(KidFoundEvent obj) => childCountTMP.text = obj.KidsToFindCount.ToString();
    }
}