using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace UnityTemplateProjects.UiControllers
{
    public class PlayLevelButtonEventDispatcher : MonoBehaviour
    {
        private IEnumerator Start()
        {
            yield return new WaitForSeconds(3);
            
            EventManager.GetInstance().Publish(GGJEventType.GameStarted);
            SceneManager.UnloadSceneAsync("LevelStartUi");
        }

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

        public void OnHideHelp()
        {
            EventManager.GetInstance().Publish(GGJEventType.HideHelp);
            SceneManager.UnloadSceneAsync("HowToPlay");
        }
    }
}