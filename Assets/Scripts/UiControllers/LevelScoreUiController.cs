using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityTemplateProjects.GameConfigs;
using UnityTemplateProjects.Level;
using Utils;

namespace UnityTemplateProjects.UiControllers
{
    public class LevelScoreUiController : MonoBehaviour
    {
        private const string INITIAL_SCORE_FORMAT = "{0}";
        private const string SCORE_FORMAT = "{0}/{1}";
        
        [SerializeField] private TextMeshProUGUI scoreText;
        
        private LevelManager _levelManager;
        private LevelEconomy _levelEconomy;
        private EventManager _eventManager;
        private UnityAction _scoreUpdatedAction;

        private void Start()
        {
            _levelManager = LevelManager.GetInstance();
            _levelEconomy = LevelEconomyProvider.GetEconomyForLevel(_levelManager.CurrentLevel);
            _eventManager = EventManager.GetInstance();
            scoreText.text = string.Format(INITIAL_SCORE_FORMAT, _levelEconomy.targetScore);
            _scoreUpdatedAction = () =>
            {
                scoreText.text = string.Format(SCORE_FORMAT, _levelManager.CurrentScore, _levelEconomy.targetScore);
            };
            _eventManager.Subscribe(GGJEventType.ScoreUpdated, _scoreUpdatedAction);
        }

        private void OnDestroy()
        {
            _eventManager.UnSubscribe(GGJEventType.ScoreUpdated, _scoreUpdatedAction);
        }
    }
}