using UnityEngine;

namespace gishadev.walkingSimulator.InteractionManager
{
    /// <summary>
    /// Applies outline on hovered
    /// </summary>
    public class OutlineHoverable : MonoBehaviour, IHoverable
    {
        public void OnHoverEnter()
        {
            Debug.Log("Outline enabled");
        }

        public void OnHoverExit()
        {
            Debug.Log("Outline disabled");
        }
    }
}