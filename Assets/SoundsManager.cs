using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioSources;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    public void Play(int soundIndex)
    {
        audioSources[soundIndex].Play();
    }
    
    public void Stop(int soundIndex)
    {
        audioSources[soundIndex].Stop();
    }
}
