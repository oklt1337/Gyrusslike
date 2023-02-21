using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    public class CanvasManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text score;
        
        // Start is called before the first frame update
        private void Start()
        {
            ScoreManager.Instance.OnScoreUpdated += i => score.text = i.ToString();
        }
    }
}
