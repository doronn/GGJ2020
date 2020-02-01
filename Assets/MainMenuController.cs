using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class MainMenuController : MonoBehaviour
{
    private bool _isHelpShown;
    [SerializeField] private AudioSource backgroundAudioSource;
    [SerializeField] private AudioSource hornAudioSource;
    [SerializeField] private GameObject[] objectsToHide;

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
        backgroundAudioSource.Stop();
        hornAudioSource.Play();

        foreach (var objectToHide in objectsToHide)
        {
            objectToHide.SetActive(false);
        }
        
        Invoke(nameof(LoadLevelScene), 2f);
    }

    public void LoadLevelScene()
    {
        SceneManager.LoadScene("idan");
    }
}