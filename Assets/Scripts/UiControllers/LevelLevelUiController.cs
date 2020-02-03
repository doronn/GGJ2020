using TMPro;
using UnityEngine;
using UnityTemplateProjects.Level;

namespace UnityTemplateProjects.UiControllers
{
    public class LevelLevelUiController : MonoBehaviour
    {
        private const string LEVEL_FORMAT = "LEVEL {0}";
        [SerializeField]
        private TextMeshProUGUI _levelText;
        
        private void Start()
        {
            _levelText.text = string.Format(LEVEL_FORMAT, LevelManager.GetInstance().CurrentLevel);
        }
    }
}