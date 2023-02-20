using UnityEngine;

namespace _Project.Scripts
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;
        [SerializeField] private float maxLifetime = 2f;
        
        private Vector3 _direction;
        private float _timer;

        private void Start()
        {
            _timer = maxLifetime;
        }

        public void Init(Vector3 direction)
        {
            _direction = direction;
        }
        
        private void Update()
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
                Destroy(gameObject);
            
            transform.position += _direction * (speed * Time.deltaTime);
        }
    }
}
