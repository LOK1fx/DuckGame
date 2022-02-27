using UnityEngine;

namespace LOK1game.World
{
    [SelectionBase]
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class WaterFloater : Actor
    {
        public Rigidbody Rigidbody { get; private set; }

        [SerializeField] private float _floatStrength = 1;

        private Water _currentWater;

        private float _instanceDrag;

        private void Start()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if(_currentWater == null) { return; }

            if(transform.position.y < _currentWater.WorldLevel)
            {
                var velocty = Vector3.up * _floatStrength;

                Rigidbody.AddForce(velocty, ForceMode.Acceleration);
            }
        }

        public void EnterWater(Water water)
        {
            _currentWater = water;

            _instanceDrag = Rigidbody.drag;
            Rigidbody.drag = _floatStrength * 0.2f;
            Rigidbody.angularDrag = _floatStrength * 0.2f;

            Rigidbody.useGravity = false;

            var velocity = Rigidbody.velocity;

            Rigidbody.velocity = new Vector3(velocity.x, velocity.y * 0.8f, velocity.z);
        }

        public void LeaveWater(Water water)
        {
            _currentWater = null;

            Rigidbody.drag = _instanceDrag;
            Rigidbody.angularDrag = _instanceDrag;

            Rigidbody.useGravity = true;

            var velocity = Rigidbody.velocity;

            Rigidbody.velocity = new Vector3(velocity.x, velocity.y * 0.1f, velocity.z);
        }
    }
}