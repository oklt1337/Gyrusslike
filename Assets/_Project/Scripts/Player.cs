using UnityEngine;

namespace _Project.Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float angularSpeed = 2f;
        [SerializeField] private float circleRad = 1f;
        
        private readonly Vector3 _fixedPoint = Vector3.zero;
        private float _currentAngle;
        private float _angularSpeed;

        private void Start ()
        {
            var offset = new Vector2 (Mathf.Sin (0), Mathf.Cos (0)) * circleRad;
            transform.position = ((Vector2) _fixedPoint) + offset;
        }

        private void Update ()
        {
            HandleMovement();
            LookAtCenter();
            HandleShooting();
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
            transform.position = ((Vector2) _fixedPoint) + offset;
        }

        private void LookAtCenter()
        {
            var dir = _fixedPoint - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        
        private void HandleShooting()
        {
            if (!Input.GetButtonDown("Jump")) 
                return;
            Shoot();
        }
        
        private void Shoot()
        {
            var position = transform.position;
            var projectile = Instantiate(projectilePrefab, position, Quaternion.identity);
            projectile.GetComponent<Projectile>().Init(_fixedPoint - position);
        }
    }
}
