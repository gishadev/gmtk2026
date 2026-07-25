using UnityEngine;
using Random = UnityEngine.Random;

namespace gishadev.gmtk.NPC
{
    public class MaterialRandomizer : MonoBehaviour
    {
        [SerializeField] private Material[] materials;
        [SerializeField] private SkinnedMeshRenderer targetMR;

        private void Start()
        {
            targetMR.material = materials[Random.Range(0, materials.Length - 1)];
        }
    }
}