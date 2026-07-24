using System;
using gishadev.gmtk.LocationManager;
using UnityEngine;

namespace gishadev.gmtk.Core
{
    [CreateAssetMenu(fileName = "GameDataSO", menuName = "ScriptableObjects/GMTK/GameData")]
    public class GameDataSO : ScriptableObject
    {
        [field: SerializeField] public GameObject CharacterPrefab { get; private set; }
        [field: SerializeField] public LocationData[] Locations { get; private set; }
    }

    [Serializable]
    public class LocationData
    {
        [field: SerializeField] public Location LocationPrefab { get; private set; }

        [Tooltip("How many of this location's kids run away to the next location; the rest are caught (happy).")]
        [field: SerializeField] public int FleeAmount { get; private set; }
    }
}
