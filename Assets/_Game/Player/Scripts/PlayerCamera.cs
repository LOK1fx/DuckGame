using UnityEngine;

namespace LOK1game.Player
{
    public class PlayerCamera : MonoBehaviour, IPawnInput
    {
        public float Tilt;

        [SerializeField] private float _cameraRotateSpeed = 160f;
        [SerializeField] private float _sensivity = 8f;
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _cameraTransform;

        [Space]
        [SerializeField] private float _maxRightViewAngle = 30f;
        [SerializeField] private float _maxLeftViewAngle = 30f;

        [Space]
        [SerializeField] private float _maxUpViewAngle = 15f;
        [SerializeField] private float _maxDownViewAngle = 15f;

        [Space]
        [SerializeField] private float _defaultFov = 65f;
        [SerializeField] private float _fovChangeSpeed = 1f;

        [SerializeField] private float _cameraOffsetResetSpeed = 7f;

        private Vector3 _cameraLerpOffset;

        private float _xRotation;
        private float _yRotation;

        private Vector3 _lastMousePosition;

        private void Start()
        {
            if(Application.isMobilePlatform)
            {
                _lastMousePosition = Vector3.zero;
            }
            else
            {
                _lastMousePosition = Input.mousePosition;
            }

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            SetFov(_defaultFov);
        }

        private void Update()
        {
            var targetRot = Quaternion.identity;

            if(Input.GetButton("Fire1"))
            {
                targetRot = Quaternion.Euler(_yRotation, _xRotation, Tilt);
            }

            _cameraTransform.localRotation = Quaternion.RotateTowards(_cameraTransform.localRotation, targetRot, Time.deltaTime * _cameraRotateSpeed);

            _camera.transform.localPosition = Vector3.MoveTowards(_camera.transform.localPosition, _cameraLerpOffset, Time.deltaTime * _cameraOffsetResetSpeed);
            _cameraLerpOffset = Vector3.MoveTowards(_cameraLerpOffset, Vector3.zero, Time.deltaTime * _cameraOffsetResetSpeed);
        }

        public void SmoothSetFov(float fov)
        {
            _camera.fieldOfView = Mathf.MoveTowards(_camera.fieldOfView, fov, Time.deltaTime * _fovChangeSpeed);
        }

        public void SetFov(float fov)
        {
            _camera.fieldOfView = fov;
        }

        public void OnInput(object sender)
        {
            if(Input.GetButtonUp("Fire1"))
            {
                _xRotation = 0f;
                _yRotation = 0f;
            }

            if(Input.GetButton("Fire1"))
            {
                var x = 0f;
                var y = 0f;

                if (!Application.isMobilePlatform)
                {
                    x = Input.GetAxis("Mouse X");
                    y = Input.GetAxis("Mouse Y");
                }
                else
                {
                    if (Input.touchCount >= 1)
                    {
                        x = Input.GetTouch(0).deltaPosition.x;
                        y = Input.GetTouch(0).deltaPosition.y;
                    }
                }

                _xRotation += x * GetSensivityMultiplier();
                _yRotation -= y * GetSensivityMultiplier();

                _xRotation = Mathf.Clamp(_xRotation, -_maxLeftViewAngle, _maxRightViewAngle);
                _yRotation = Mathf.Clamp(_yRotation, -_maxUpViewAngle, _maxDownViewAngle);

                _xRotation = ThreehoundredToZero(_xRotation);
                _yRotation = ThreehoundredToZero(_yRotation);
            }
        }

        private float GetSensivityMultiplier()
        {
            var multiplier = 20f;

            if(Application.isMobilePlatform)
            {
                multiplier = 1f;
            }

            return (_sensivity * multiplier) * Time.deltaTime;
        }

        private float ThreehoundredToZero(float value)
        {
            if(value >= 360 || value <= -360)
            {
                return 0f;
            }
            else
            {
                return value;
            }
        }

        public void AddCameraOffset(Vector3 offset)
        {
            _cameraLerpOffset += offset;
        }

        public Transform GetCameraTransform()
        {
            return _cameraTransform;
        }

        public Camera GetCamera()
        {
            return _camera;
        }

        public float GetDefaultFov()
        {
            return _defaultFov;
        }

        public float GetCurrentFov()
        {
            return _camera.fieldOfView;
        }
    }
}