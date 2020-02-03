using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityTemplateProjects.Level;
using Utils;

namespace UnityTemplateProjects.UiControllers
{
    public class LevelTimerUiController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timeText;
        
        private UnityAction _timeUpdatedAction;
        private const string TimeFormat = @"mm\:ss";

        private void Start()
        {
            var levelManager = LevelManager.GetInstance();
            var levelEconomy = LevelEconomyProvider.GetEconomyForLevel(levelManager.CurrentLevel);
            var eventManager = EventManager.GetInstance();
            _timeText.text = TimeSpan.FromSeconds(levelEconomy.duration).ToString(TimeFormat);
            _timeUpdatedAction = () =>
            {
                _timeText.text = TimeSpan.FromSeconds(levelManager.RemainingTime).ToString(TimeFormat);
            };
            eventManager.Subscribe(GGJEventType.TimeUpdated, _timeUpdatedAction);
        }

        private void OnDestroy()
        {
            EventManager.GetInstance().UnSubscribe(GGJEventType.TimeUpdated, _timeUpdatedAction);
        }
    }
}