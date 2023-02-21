using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance;
        
        [SerializeField] private int baseMultiplier = 1000;
        
        private int _score;
        private int Score
        {
            get => _score;
            set
            {
                _score = value;
                OnScoreUpdated?.Invoke(_score);
            }
        }

        public event Action<int> OnScoreUpdated; 

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;
        }
        
        private void Start()
        {
            EnemySpawner.Instance.OnEnemyRemoved += IncreaseScore;
            Score = 0;
        }

        private void IncreaseScore(float radius)
        {
            var add = (int) (baseMultiplier / radius);
            Score += add;
        }
    }
}
