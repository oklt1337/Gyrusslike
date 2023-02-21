using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float angularSpeed = 2f;
        [SerializeField] private float circleRad = .1f;
        [SerializeField] private float circleRadMultiplier = .01f;
        
        private readonly Vector3 _fixedPoint = Vector3.zero;
        private float _currentAngle;
        private float _angularSpeed;
        private const float MaxCircleRad = 11.0f;
        private bool _dead;

        public float CircleRad => circleRad;

        public event Action<Enemy> OnDeath;

        public void Init(int startDirection)
        {
            _angularSpeed = angularSpeed * startDirection;
        }
        
        private void Update()
        {
            if (_dead)
                return;
            if (circleRad >= MaxCircleRad)
                Die();
            
            Move();
            IncreaseRadius();
        }
        
        private void Move()
        {
            _currentAngle += _angularSpeed * Time.deltaTime;
            var offset = new Vector2 (Mathf.Sin (_currentAngle), Mathf.Cos (_currentAngle)) * circleRad;
            transform.position = ((Vector2) _fixedPoint) + offset;
        }

        private void IncreaseRadius()
        {
            circleRad += Time.deltaTime * circleRadMultiplier;
        }

        private void Die()
        {
            _dead = true;
            OnDeath?.Invoke(this);
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Projectile"))
                Die();
        }
    }
}
