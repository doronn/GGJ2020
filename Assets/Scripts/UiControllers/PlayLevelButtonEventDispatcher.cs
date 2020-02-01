using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace UnityTemplateProjects.UiControllers
{
    public class PlayLevelButtonEventDispatcher : MonoBehaviour
    {
        public void OnPlayLevelClicked()
        {
            EventManager.GetInstance().Publish(GGJEventType.GameStarted);
            SceneManager.UnloadSceneAsync("LevelStartUi");
        }

        public void OnLevelFinishContinue()
        {
            EventManager.GetInstance().Publish(GGJEventType.LevelFinishContinue);
            SceneManager.UnloadSceneAsync("SumupUiScreen");
        }
    }
}