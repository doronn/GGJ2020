using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class MainMenuController : MonoBehaviour
{
    private bool _isHelpShown = false;

    public void Start()
    {
        var shouldShowHelpFirstTime = PlayerPrefs.GetInt(Constants.HelpWasShown, 0) == 0;
        if (shouldShowHelpFirstTime)
        {
            ShowHelp();
        }
    }

    private void HideHelp()
    {
        PlayerPrefs.SetInt(Constants.HelpWasShown, 1);
        _isHelpShown = false;
    }

    public void ShowHelp()
    {
        if (_isHelpShown)
        {
            return;
        }
        
        EventManager.GetInstance().Subscribe(GGJEventType.HideHelp, HideHelp);
        SceneManager.LoadSceneAsync("HowToPlay", LoadSceneMode.Additive);
        _isHelpShown = true;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("idan");
    }
    
    
}
