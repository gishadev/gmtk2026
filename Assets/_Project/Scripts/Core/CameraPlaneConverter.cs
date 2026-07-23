using UnityEngine;

namespace gishadev.gmtk.Core
{
    public static class CameraPlaneConverter
    {
        public static Vector3 ViewportToWorldPlanePoint(Camera cam, float zDepth, Vector2 viewportCord)
        {
            Vector2 angles = ViewportPointToAngle(cam, viewportCord);
            float xOffset = Mathf.Tan(angles.x) * zDepth;
            float yOffset = Mathf.Tan(angles.y) * zDepth;
            Vector3 cameraPlanePosition = new Vector3(xOffset, yOffset, zDepth);
            cameraPlanePosition = cam.transform.TransformPoint(cameraPlanePosition);
            return cameraPlanePosition;
        }

        public static Vector3 ScreenToWorldPlanePoint(Camera cam, float zDepth, Vector3 screenCoords)
        {
            var point = cam.ScreenToViewportPoint(screenCoords);
            return ViewportToWorldPlanePoint(cam, zDepth, point);
        }

        /// <summary>
        /// Returns X and Y frustum angle for the given camera representing the given viewport space coordinate.
        /// </summary>
        public static Vector2 ViewportPointToAngle(Camera cam, Vector2 viewportCord)
        {
            float adjustedAngle = AngleProportion(cam.fieldOfView / 2, cam.aspect) * 2;
            float xProportion = ((viewportCord.x - .5f) / .5f);
            float yProportion = ((viewportCord.y - .5f) / .5f);
            float xAngle = AngleProportion(adjustedAngle / 2, xProportion) * Mathf.Deg2Rad;
            float yAngle = AngleProportion(cam.fieldOfView / 2, yProportion) * Mathf.Deg2Rad;
            return new Vector2(xAngle, yAngle);
        }

        /// <summary>
        /// Distance between the camera and a plane parallel to the viewport that passes through a given point.
        /// </summary>
        public static float CameraToPointDepth(Camera cam, Vector3 point)
        {
            Vector3 localPosition = cam.transform.InverseTransformPoint(point);
            return localPosition.z;
        }

        public static float AngleProportion(float angle, float proportion)
        {
            float oppisite = Mathf.Tan(angle * Mathf.Deg2Rad);
            float oppisiteProportion = oppisite * proportion;
            return Mathf.Atan(oppisiteProportion) * Mathf.Rad2Deg;
        }
    }
}