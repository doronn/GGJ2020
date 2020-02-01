using System;
using UnityEngine;
using UnityTemplateProjects.Level;

namespace UnityTemplateProjects.UiControllers
{
    public class LevelStarsUiController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _startArray;
        
        [SerializeField]
        private GameObject[] _words;

        private void Start()
        {
            var amountOfStars = LevelManager.GetInstance().CalculateStars();

            var starsArrayLength = _startArray.Length;
            for (int i = 0; i < amountOfStars && i < starsArrayLength; i++)
            {
                _startArray[i].SetActive(true);
            }

            if (_words.Length >= amountOfStars)
            {
                _words[Math.Max(0, amountOfStars - 1)].SetActive(true);
            }
        }
    }
}