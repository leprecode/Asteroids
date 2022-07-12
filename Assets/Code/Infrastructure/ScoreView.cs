using Assets.Code.UI.Menu;
using TMPro;
using UnityEngine;

namespace Assets.Code.Infrastructure
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        private void Start()
        {
            Score.ValueIncreased += UpdateScore;
        }

        private void UpdateScore(int newScore)
        {
            _scoreText.SetText(newScore.ToString());
        }
    }
}