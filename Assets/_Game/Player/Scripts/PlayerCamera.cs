using UnityEngine;

namespace LOK1game.Player
{
    public class PlayerCamera : MonoBehaviour, IPawnInput
    {
        public float Tilt;

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

        private float _xRotation;
        private float _yRotation;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            SetFov(_defaultFov);
        }

        private void Update()
        {
            _cameraTransform.localRotation = Quaternion.Euler(_yRotation, _xRotation, Tilt);
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
            var x = Input.GetAxis("Mouse X");
            var y = Input.GetAxis("Mouse Y");

            _xRotation += x * (_sensivity * 10) * Time.deltaTime;
            _yRotation -= y * (_sensivity * 10) * Time.deltaTime;

            _xRotation = Mathf.Clamp(_xRotation, -_maxLeftViewAngle, _maxRightViewAngle);
            _yRotation = Mathf.Clamp(_yRotation, -_maxUpViewAngle, _maxDownViewAngle);

            _xRotation = ThreehoundredToZero(_xRotation);
            _yRotation = ThreehoundredToZero(_yRotation);
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
    }
}