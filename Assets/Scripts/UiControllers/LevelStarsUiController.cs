using System;
using UnityEngine;
using UnityTemplateProjects.Level;

namespace UnityTemplateProjects.UiControllers
{
    public class LevelStarsUiController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _startArray;

        private void Start()
        {
            var amountOfStars = LevelManager.GetInstance().CalculateStars();

            var starsArrayLength = _startArray.Length;
            for (int i = 0; i < amountOfStars && i < starsArrayLength; i++)
            {
                _startArray[i].SetActive(true);
            }
        }
    }
}