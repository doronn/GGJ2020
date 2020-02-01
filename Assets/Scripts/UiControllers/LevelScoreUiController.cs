using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityTemplateProjects.Level;
using Utils;

namespace UnityTemplateProjects.UiControllers
{
    public class LevelScoreUiController : MonoBehaviour
    {
        private const string SCORE_FORMAT = "{0}/{1}";
        
        [SerializeField]
        private TextMeshProUGUI _scoreText;

        private UnityAction _scoreUpdatedAction;

        private void Start()
        {
            _scoreText.text = string.Format(SCORE_FORMAT, LevelManager.GetInstance().CurrentScore,
                LevelEconomyProvider.GetEconomyForLevel(1).targetScore);
            _scoreUpdatedAction = () =>
                {
                    _scoreText.text = string.Format(SCORE_FORMAT, LevelManager.GetInstance().CurrentScore,
                        LevelEconomyProvider.GetEconomyForLevel(1).targetScore);
                };
            EventManager.GetInstance().Subscribe(GGJEventType.ScoreUpdated, _scoreUpdatedAction);
        }

        private void OnDestroy()
        {
            EventManager.GetInstance().UnSubscribe(GGJEventType.ScoreUpdated, _scoreUpdatedAction);
        }
    }
}