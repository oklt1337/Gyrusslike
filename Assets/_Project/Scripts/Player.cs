using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float angularSpeed = 1f;
        [SerializeField] private float rotationModifier = 1f;
        [SerializeField] private float circleRad = 1f;
        [SerializeField] private Transform fixedPoint;
        
        private float _currentAngle;
        private float _angularSpeed;

        private void Start ()
        {
            var offset = new Vector2 (Mathf.Sin (0), Mathf.Cos (0)) * circleRad;
            transform.position = ((Vector2) fixedPoint.position) + offset;
        }

        private void Update ()
        {
            HandleMovement();
        }
        
        private void HandleMovement()
        {
            if (!Input.GetButton("Horizontal")) 
                return;
            _angularSpeed = Input.GetAxis("Horizontal") < 0 ? -angularSpeed : angularSpeed;

            _currentAngle += _angularSpeed * Time.deltaTime;
            Move(_currentAngle);
        }
        
        private void Move(float currentAngle)
        {
            var offset = new Vector2 (Mathf.Sin (currentAngle), Mathf.Cos (currentAngle)) * circleRad;
            transform.position = ((Vector2) fixedPoint.position) + offset;
            LookAtCenter(offset);
        }

        private void LookAtCenter(Vector3 offset)
        {
            var rotation = transform.rotation.eulerAngles;
            rotation.z += _angularSpeed * Time.deltaTime;
            
            transform.rotation = Quaternion.Euler(offset);
        }
    }
}
