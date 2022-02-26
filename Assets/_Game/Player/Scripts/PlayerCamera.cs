using UnityEngine;

namespace LOK1game.Player
{
    public class PlayerCamera : MonoBehaviour, IPawnInput
    {
        public float Tilt;

        [SerializeField] private float _sensivity = 8f;
        [SerializeField] private Transform _cameraTransform;

        [Space]
        [SerializeField] private float _maxRightViewAngle = 30f;
        [SerializeField] private float _maxLeftViewAngle = 30f;

        [Space]
        [SerializeField] private float _maxUpViewAngle = 15f;
        [SerializeField] private float _maxDownViewAngle = 15f;

        private float _xRotation;
        private float _yRotation;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            _cameraTransform.localRotation = Quaternion.Euler(_yRotation, _xRotation, Tilt);
        }

        public void OnInput(object sender)
        {
            var x = Input.GetAxis("Mouse X");
            var y = Input.GetAxis("Mouse Y");

            _xRotation += x * (_sensivity * 10) * Time.deltaTime;
            _yRotation -= y * (_sensivity * 10) * Time.deltaTime;

            _xRotation = Mathf.Clamp(_xRotation, -_maxLeftViewAngle, _maxRightViewAngle);
            _yRotation = Mathf.Clamp(_yRotation, -_maxUpViewAngle, _maxDownViewAngle);
        }

        public Transform GetCameraTransform()
        {
            return _cameraTransform;
        }
    }
}