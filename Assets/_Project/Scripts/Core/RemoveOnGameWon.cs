using UnityEngine;

namespace gishadev.gmtk.Core
{
    public class RemoveOnGameWon : MonoBehaviour
    {
        private void Awake()
        {
            if (GameSettings.IsGameWon)
                Destroy(gameObject);
        }
    }
}
