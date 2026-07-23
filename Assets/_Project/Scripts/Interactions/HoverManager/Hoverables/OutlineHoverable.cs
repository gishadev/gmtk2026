using UnityEngine;

namespace gishadev.gmtk.Interactions.HoverManager.Hoverables
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