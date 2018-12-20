using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;


/// <summary>
/// Brian's MouseLook
/// - turn speed is proportional to mouse position
/// - provides optional hystersis
/// 
/// </summary>
namespace UnityStandardAssets.Characters.FirstPerson
{
    [Serializable]
    public class MouseLook
    {
        public float XSensitivity = 2f;
        public float YSensitivity = 2f;
        public float hysteresis = 0.3f;
        public bool clampVerticalRotation = true;
        public float MinimumX = -90F;
        public float MaximumX = 90F;
        public bool smooth;
        public float smoothTime = 5f;
        public bool lockCursor = true;
        public bool velocityMode = true;
        public Text debug;

        private Quaternion m_CharacterTargetRot;
        private Quaternion m_CameraTargetRot;
        private bool m_cursorIsLocked = true;

        //private int counter = 0;

        public void Init(Transform character, Transform camera)
        {
            m_CharacterTargetRot = character.localRotation;
            m_CameraTargetRot = camera.localRotation;

            debug.text = "Brian's MouseLook started.";
        }

        public void LookRotation(Transform character, Transform camera)
        {
            if (velocityMode)
            {
                //float yRot = CrossPlatformInputManager.GetAxis("Mouse X") * XSensitivity;
                //float xRot = CrossPlatformInputManager.GetAxis("Mouse Y") * YSensitivity;

                // Translate mouse position into rotational velocity
                Vector3 mousePos = Input.mousePosition;
                float leftRightTurnSpeed = (mousePos.x * 2f / Screen.width) - 1f;    // normalise to -1 .. +1
                float upDownTurnSpeed = (mousePos.y * 2f / Screen.height) - 1f;    // normalise to -1 .. +1

                leftRightTurnSpeed = ApplyHysteresis(leftRightTurnSpeed);
                upDownTurnSpeed = ApplyHysteresis(upDownTurnSpeed);

                leftRightTurnSpeed *= XSensitivity;
                upDownTurnSpeed *= YSensitivity;

                //m_CharacterTargetRot *= Quaternion.Euler (0f, yRot, 0f);
                //m_CameraTargetRot *= Quaternion.Euler (-xRot, 0f, 0f);
                m_CharacterTargetRot = Quaternion.Euler(0f, character.localRotation.eulerAngles.y + leftRightTurnSpeed, 0f);
                m_CameraTargetRot = Quaternion.Euler(camera.localRotation.eulerAngles.x - upDownTurnSpeed, 0f, 0f);

                //counter++;

                UpdateDebugText(leftRightTurnSpeed, upDownTurnSpeed,
                    m_CharacterTargetRot, m_CameraTargetRot,
                    character.rotation, camera.rotation,
                    character.localRotation, camera.localRotation);
            }
            else
            {
                float yRot = CrossPlatformInputManager.GetAxis("Mouse X") * XSensitivity;
                float xRot = CrossPlatformInputManager.GetAxis("Mouse Y") * YSensitivity;

                m_CharacterTargetRot *= Quaternion.Euler (0f, yRot, 0f);
                m_CameraTargetRot *= Quaternion.Euler (-xRot, 0f, 0f);

                UpdateDebugText(0f, 0f,
                    m_CharacterTargetRot, m_CameraTargetRot,
                    character.rotation, camera.rotation,
                    character.localRotation, camera.localRotation);
            }

            if (clampVerticalRotation)      // enforce max up/down limits
                m_CameraTargetRot = ClampRotationAroundXAxis (m_CameraTargetRot);

            if(smooth)
            {
                character.localRotation = Quaternion.Slerp (character.localRotation, m_CharacterTargetRot,
                    smoothTime * Time.deltaTime);
                camera.localRotation = Quaternion.Slerp (camera.localRotation, m_CameraTargetRot,
                    smoothTime * Time.deltaTime);
            }
            else
            {
                character.localRotation = m_CharacterTargetRot;
                camera.localRotation = m_CameraTargetRot;
            }

            UpdateCursorLock();
        }

        /// <summary>
        /// Takes an input value between -1 and +1.
        /// Modifies it to provide a "dead zone" above zero.
        /// </summary>
        /// <param name="upDownTurnSpeed"></param>
        /// <returns></returns>
        private float ApplyHysteresis(float turnRate)
        {
            float value = Mathf.Abs(turnRate);
            bool negative = turnRate < 0;

            value -= hysteresis;
            if (value < 0f) value = 0f;

            return negative ? -value : +value;
        }

        public void SetCursorLock(bool value)
        {
            lockCursor = value;
            if(!lockCursor)
            {
                //we force unlock the cursor if the user disable the cursor locking helper
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        public void UpdateCursorLock()
        {
            //if the user set "lockCursor" we check & properly lock the cursor
            if (lockCursor)
                InternalLockUpdate();
        }

        private void InternalLockUpdate()
        {
            if(Input.GetKeyUp(KeyCode.Escape))
            {
                m_cursorIsLocked = false;
            }
            else if(Input.GetMouseButtonUp(0))
            {
                m_cursorIsLocked = true;
            }

            if (m_cursorIsLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                //Cursor.visible = false;
            }
            else if (!m_cursorIsLocked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

            angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);

            q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }

        private void UpdateDebugText(float xVelocity, float yVelocity,
            Quaternion m_CharacterTargetRot, Quaternion m_CameraTargetRot,
            Quaternion characterRot, Quaternion cameraRot,
            Quaternion characterLocalRot, Quaternion cameraLocalRot)
        {
            string newline = "\n";

            Vector3 mousePos = Input.mousePosition;
            //Debug.Log("Mouse is " + xRot + ", " + yRot + " - " + Input.mousePosition);
            return;
            debug.text =
                "Mouse at " + mousePos.x / Screen.width + ", " + mousePos.y / Screen.height + newline
                + "Turn speed " + xVelocity + ", " + yVelocity + newline
                + "Target rot " + m_CharacterTargetRot.eulerAngles + ", " + m_CameraTargetRot.eulerAngles + newline
                + "Rotation " + characterRot.eulerAngles + ", " + cameraRot.eulerAngles + newline
                + "Local rot " + characterLocalRot.eulerAngles + ", " + cameraLocalRot.eulerAngles + newline
                + "visible: " + Cursor.visible
            ;
        }

        /// <summary>
        /// QuaternionToString
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        private string qts(Quaternion  q)
        {
            return q.ToString("0.000");
        }
    }
}
