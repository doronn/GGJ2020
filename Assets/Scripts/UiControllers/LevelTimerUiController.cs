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
            _timeText.text = TimeSpan.FromSeconds(LevelEconomyProvider.GetEconomyForLevel(1).duration).ToString();
            _timeUpdatedAction = () =>
            {
                _timeText.text = TimeSpan.FromSeconds(LevelManager.GetInstance().RemainingTime).ToString(TimeFormat);
            };
            EventManager.GetInstance().Subscribe(GGJEventType.TimeUpdated, _timeUpdatedAction);
        }

        private void OnDestroy()
        {
            EventManager.GetInstance().UnSubscribe(GGJEventType.TimeUpdated, _timeUpdatedAction);
        }
    }
}