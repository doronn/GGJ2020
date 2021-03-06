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
        [SerializeField] private SoundsManagerProvider soundsManagerProvider;

        private LevelEconomy _currentLevelEconomyData;
        private int _currentScore;
        private float _elapsedTime;
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

        public int CurrentLevel { get; private set; }

        public float RemainingTime => Mathf.Max(0, _currentLevelEconomyData.duration - _elapsedTime);

        private void Start()
        {
            CurrentLevel = PlayerPrefs.GetInt(Constants.CurrLevelKey, 1);
            _currentLevelEconomyData = LevelEconomyProvider.GetEconomyForLevel(CurrentLevel);
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
            
            soundsManagerProvider.Stop(4);
            SceneManager.UnloadSceneAsync("OverlayUi");
            SceneManager.LoadSceneAsync("SumupUiScreen", LoadSceneMode.Additive);
            EventManager.GetInstance().Subscribe(GGJEventType.LevelFinishContinue, EndLevel);
        }

        private void EndLevel()
        {
            if (CalculateStars() > 1)
            {
                PlayerPrefs.SetInt(Constants.CurrLevelKey, CurrentLevel + 1);
                PlayerPrefs.Save();
                SceneManager.LoadScene("Level");
            }
            else
            {
                ReturnToMain();
            }
        }

        public void ReturnToMain()
        {
            SceneManager.LoadScene("MainScene");
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

        public int CalculateStars()
        {
            var targetScore = _currentLevelEconomyData.targetScore;
            
            if (_currentScore < 0.7f * targetScore)
            {
                return 0;
            }

            if (_currentScore < 0.8f * targetScore)
            {
                return 1;
            }

            if (_currentScore < 1.2f * targetScore)
            {
                return 2;
            }
            
            return 3;
        }
    }
}