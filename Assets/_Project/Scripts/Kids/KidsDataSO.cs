using UnityEngine;

namespace gishadev.gmtk.kids
{
    [CreateAssetMenu(fileName = "KidsDataSO", menuName = "ScriptableObjects/GMTK/KidsDataSO")]
    public class KidsDataSO : ScriptableObject
    {
        [field: Header("Spawning")]
        [field: SerializeField] public Kid KidPrefab { get; private set; }
        [field: SerializeField] public int KidsCount { get; private set; } = 4;

        [field: Header("Round Flow")]
        [field: Tooltip("Seconds the seeker must wait before hunting begins.")]
        [field: SerializeField] public float HideCountdown { get; private set; } = 10f;

        [field: SerializeField] public int HappyCount { get; private set; } = 1;

        [field: Header("Seeker Detection")]
        [field: Tooltip("Max distance at which the seeker can spot a hiding kid.")]
        [field: SerializeField] public float DetectRadius { get; private set; } = 6f;

        [field: Tooltip("Half-angle (degrees) of the seeker's view cone around the camera forward.")]
        [field: SerializeField] public float ViewHalfAngle { get; private set; } = 35f;

        [field: Tooltip("Layers that block line of sight between the seeker and a kid.")]
        [field: SerializeField] public LayerMask LineOfSightMask { get; private set; } = ~0;
    }
}
