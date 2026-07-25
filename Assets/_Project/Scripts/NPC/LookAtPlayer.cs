using gishadev.gmtk.Core;
using UnityEngine;

namespace gishadev.gmtk.NPC
{
    /// <summary>
    /// Lightweight "fake IK": turns the head to face the player, and rotates the body
    /// once the required angle exceeds the head's limit. Runs in LateUpdate so it
    /// overrides whatever pose the Animator produced this frame.
    /// </summary>
    public class LookAtPlayer : MonoBehaviour
    {
        [SerializeField] private Transform headTrans;
        [SerializeField] private Transform bodyTrans;

        [Header("Limits")]
        [Tooltip("Max yaw the head turns from the body before the body starts rotating.")]
        [SerializeField] private float maxHeadYaw = 70f;

        [Tooltip("Max up/down tilt of the head.")]
        [SerializeField] private float maxHeadPitch = 40f;

        [Tooltip("Once the player is back within this yaw of the body's front, the body relaxes to neutral. " +
                 "Keep it below maxHeadYaw so the body doesn't hunt back and forth.")]
        [SerializeField] private float bodyRelaxYaw = 30f;

        [Header("Feel")]
        [Tooltip("How fast the head catches up to the target, in degrees per second.")]
        [SerializeField] private float turnSpeed = 360f;

        [Tooltip("How fast the body turns, in degrees per second. Usually slower than the head.")]
        [SerializeField] private float bodyTurnSpeed = 180f;

        [Tooltip("Vertical offset on the player to aim at (roughly the player's head height).")]
        [SerializeField] private float aimHeightOffset = 1.6f;

        private PlayerCharacter _playerCharacter;

        // Rest-pose local rotations, restored each frame so our look offset never compounds.
        private Quaternion _headBaseLocal;
        private Quaternion _bodyBaseLocal;

        // Smoothed angles currently applied on top of the rest pose.
        private float _headYaw;
        private float _bodyYaw;
        private float _headPitch;

        private void Start()
        {
            _playerCharacter = FindFirstObjectByType<PlayerCharacter>();

            if (headTrans != null) _headBaseLocal = headTrans.localRotation;
            if (bodyTrans != null) _bodyBaseLocal = bodyTrans.localRotation;
        }

        // LateUpdate runs after the Animator, so writing bone rotations here overrides the animation.
        private void LateUpdate()
        {
            if (_playerCharacter == null || headTrans == null || bodyTrans == null)
                return;

            var aimPoint = _playerCharacter.transform.position + Vector3.up * aimHeightOffset;
            var toPlayer = aimPoint - headTrans.position;

            var flat = Vector3.ProjectOnPlane(toPlayer, Vector3.up);
            var flatDist = flat.magnitude;
            if (flatDist < 0.001f)
                return;

            var flatDir = flat / flatDist;

            // Total yaw the character needs, measured from its home facing.
            var totalYaw = Vector3.SignedAngle(transform.forward, flatDir, Vector3.up);

            // Body stays neutral and lets the head do the glancing; it only turns when the
            // head can't reach, and then it squares up to the player so the head re-centers.
            var headRelToBody = totalYaw - _bodyYaw;
            float targetBodyYaw;
            if (Mathf.Abs(headRelToBody) > maxHeadYaw)
                targetBodyYaw = totalYaw;                    // out of reach → turn to face the player
            else if (Mathf.Abs(totalYaw) <= bodyRelaxYaw)
                targetBodyYaw = 0f;                          // player back in front → relax to neutral
            else
                targetBodyYaw = _bodyYaw;                    // hold (hysteresis, avoids hunting)

            _bodyYaw = Mathf.MoveTowards(_bodyYaw, targetBodyYaw, bodyTurnSpeed * Time.deltaTime);

            // Head takes whatever yaw is left over, clamped to its limit.
            var targetHeadYaw = Mathf.Clamp(totalYaw - _bodyYaw, -maxHeadYaw, maxHeadYaw);

            // Head pitch, positive when the player is above the head.
            var targetPitch = Mathf.Clamp(Mathf.Atan2(toPlayer.y, flatDist) * Mathf.Rad2Deg,
                -maxHeadPitch, maxHeadPitch);

            var headStep = turnSpeed * Time.deltaTime;
            _headYaw = Mathf.MoveTowards(_headYaw, targetHeadYaw, headStep);
            _headPitch = Mathf.MoveTowards(_headPitch, targetPitch, headStep);

            // Restore the rest pose first, then layer the look offset on top (in world space,
            // so it works regardless of the bones' local axis orientation).
            bodyTrans.localRotation = _bodyBaseLocal;
            bodyTrans.rotation = Quaternion.AngleAxis(_bodyYaw, Vector3.up) * bodyTrans.rotation;

            // Head is a child of the body, so it already inherits the body yaw above;
            // here it adds its own yaw and pitch.
            headTrans.localRotation = _headBaseLocal;
            var pitchAxis = Vector3.Cross(Vector3.up, flatDir); // horizontal, to the right of the look dir
            headTrans.rotation = Quaternion.AngleAxis(_headYaw, Vector3.up)
                                 * Quaternion.AngleAxis(-_headPitch, pitchAxis)
                                 * headTrans.rotation;
        }
    }
}
