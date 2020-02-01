using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityTemplateProjects.GameConfigs;
using Utils;

namespace UnityTemplateProjects.Level
{
    public class LevelManager : BaseSingleton<LevelManager>
    {
        [SerializeField] private Map map;

        private LevelEconomy _currentLevelEconomyData;
        private int _currentScore;
        private float _elapsedTime;
        private int _currentLevel;

        private bool _isActive;

        public int CurrentScore
        {
            get => _currentScore;
            set
            {
                _currentScore = value;
                EventManager.GetInstance().Publish(GGJEventType.ScoreUpdated);
            }
        }

        public int CurrentLevel => _currentLevel;

        public float RemainingTime => Mathf.Max(0, _currentLevelEconomyData.duration - _elapsedTime);

        private void Start()
        {
            _currentLevel = 1;
            _currentLevelEconomyData = LevelEconomyProvider.GetEconomyForLevel(_currentLevel);
            _elapsedTime = 0;
            SceneManager.LoadSceneAsync("LevelStartUi", LoadSceneMode.Additive);
            GenerateMap();
            StartCoroutine(RunLevel());
            EventManager.GetInstance().Subscribe(GGJEventType.GameStarted, StartGame);
        }
        
        public void StartGame()
        {
            _isActive = true;
            SceneManager.LoadSceneAsync("OverlayUi", LoadSceneMode.Additive);
            EventManager.GetInstance().UnSubscribe(GGJEventType.GameStarted, StartGame);
            EventManager.GetInstance().Subscribe(GGJEventType.GamePaused, PauseGame);
        }
        
        public void PauseGame()
        {
            _isActive = false;
            EventManager.GetInstance().UnSubscribe(GGJEventType.GamePaused, StartGame);
            EventManager.GetInstance().Subscribe(GGJEventType.GameStarted, StartGame);
        }
        
        private IEnumerator RunLevel()
        {
            while (!_isActive)
            {
                yield return null;
            }
            
            SetLanesActivation(true);
            
            var secondsPassed = 0;
            
            while (_currentLevelEconomyData.duration > _elapsedTime)
            {
                while (!_isActive)
                {
                    SetLanesActivation(false);
                    yield return null;
                }
                
                yield return null;
                SetLanesActivation(true);
                _elapsedTime += Time.deltaTime;
                var currentElapsedSeconds = (int)Mathf.Floor(_elapsedTime);

                if (currentElapsedSeconds > secondsPassed)
                {
                    secondsPassed = currentElapsedSeconds;
                    EventManager.GetInstance().Publish(GGJEventType.TimeUpdated);
                }
            }
            
            SetLanesActivation(false);
            
            EndLevel();
        }

        private void EndLevel()
        {
            // Do end level logic
            
        }
        
        public void GenerateMap()
        {
            map.Generate(_currentLevelEconomyData.lanes);
        }

        public void SetLanesActivation(bool activate)
        {
            foreach (var laneController in map.laneControllers)
            {
                laneController.isOn = activate;
            }
        }
    }
}