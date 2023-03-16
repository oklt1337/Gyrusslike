using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        public static EnemySpawner Instance;
        
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private Vector2Int spawnRange = new(2, 10);
        [SerializeField] private float reSpawnDelay = 2f;
        [SerializeField] private float spawnDelay = .2f;

        private readonly List<Enemy> _enemies = new();
        private Coroutine _spawnCo;

        public event Action<float> OnEnemyRemoved;

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;
        }

        private void Start()
        {
            _spawnCo = StartCoroutine(EnemySpawnDelayCo());
        }

        private void SpawnEnemies()
        {
            var enemySpawnCount = Random.Range(spawnRange.x, spawnRange.y);
            StartCoroutine(EnemySpawnCo(enemySpawnCount, -1));
        }

        private void SpawnEnemy(int direction)
        {
            var enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity).GetComponent<Enemy>();
            enemy.Init(direction);
            enemy.OnDeath += RemoveEnemy;
            _enemies.Add(enemy);
        }

        private void RemoveEnemy(Enemy enemy)
        {
            if (_enemies.Contains(enemy))
            {
                OnEnemyRemoved?.Invoke(enemy.CircleRad);
                _enemies.Remove(enemy);
            }

            if (_enemies.Count > 0)
                return;

            if (_spawnCo != null)
                StopCoroutine(_spawnCo);
            _spawnCo = StartCoroutine(EnemySpawnDelayCo());
        }

        private IEnumerator EnemySpawnDelayCo()
        {
            yield return new WaitForSeconds(reSpawnDelay);
            SpawnEnemies();
        }

        private IEnumerator EnemySpawnCo(int enemySpawnCount,int direction)
        {
            for (var i = 0; i < enemySpawnCount; i++)
            {
                yield return new WaitForSeconds(spawnDelay);
                SpawnEnemy(direction);
            }
        }
    }
}
