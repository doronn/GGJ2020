using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToHideOnPlay;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject helpButton;
    
    private bool _isHelpShown;

    private void Awake()
    {
        SceneManager.LoadScene("Sounds", LoadSceneMode.Additive);
    }
    
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
        playButton.SetActive(true);
        helpButton.SetActive(true);
        _isHelpShown = false;
    }

    public void ShowHelp()
    {
        if (_isHelpShown)
        {
            return;
        }
        
        playButton.SetActive(false);
        helpButton.SetActive(false);
        EventManager.GetInstance().Subscribe(GGJEventType.HideHelp, HideHelp);
        SceneManager.LoadSceneAsync("HowToPlay", LoadSceneMode.Additive);
        _isHelpShown = true;
    }
    
    public void LoadLevel()
    {
        foreach (var objectToHide in objectsToHideOnPlay)
        {
            objectToHide.SetActive(false);
        }
        
        Invoke(nameof(LoadLevelScene), 2f);
    }

    private void LoadLevelScene()
    {
        SceneManager.LoadScene("Level");
    }
}