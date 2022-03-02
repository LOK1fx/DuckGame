using UnityEngine;
using UnityEngine.Events;

namespace LOK1game
{
    [RequireComponent(typeof(Duck))]
    public class DuckMovement : MonoBehaviour
    {
        public UnityAction<DestinationPoint> OnChangeDistanationPoint;

        public DuckMovementZone CurrentMovementZone { get; private set; }
        public DestinationPoint CurrentDestinationPoint { get; private set; }

        [Range(0f, 35f)]
        [SerializeField] private float _moveSpeed = 2f;
        [SerializeField] private float _achivePointThreshold = 0.1f;

        private Duck _duck;

        private void Awake()
        {
            _duck = GetComponent<Duck>();
        }

        private void Update()
        {
            if(CurrentMovementZone == null) { return; }

            if(CurrentDestinationPoint == null)
            {
                SetDistanation(GetRandomPoint());
            }

            if(Vector3.Distance(transform.position, CurrentDestinationPoint.Position) < _achivePointThreshold)
            {
                SetDistanation(GetRandomPoint());
            }

            transform.position = Vector3.MoveTowards(transform.position, CurrentDestinationPoint.Position, Time.deltaTime * _moveSpeed);
        }

        public void SetDistanation(DestinationPoint point)
        {
            if(point.IsFull())
            {
                throw new System.Exception("Point is full!");
            }

            CurrentDestinationPoint = point;

            OnChangeDistanationPoint?.Invoke(CurrentDestinationPoint);
        }

        public void SetMovementZone(DuckMovementZone zone)
        {
            CurrentMovementZone = zone;

            var point = zone.GetRandomPoint();

            zone.TryToSetPositionToPoint(_duck, point);         
        }

        private DestinationPoint GetRandomPoint()
        {
            var point = CurrentMovementZone.GetRandomPoint();

            if(point.IsFull() || point == CurrentDestinationPoint)
            {
                return GetRandomPoint();
            }

            return point;
        }
    }
}