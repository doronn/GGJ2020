using UnityEngine;

public class SoundsManagerProvider : MonoBehaviour
{
    [SerializeField] private int[] playOnAwakeIndices;
    private static SoundsManager soundsManager;

    private void Start()
    {
        if (playOnAwakeIndices == null)
        {
            return;
        }
        
        foreach (var index in playOnAwakeIndices)
        {
            Play(index);
        }
    }

    public void Play(int index)
    {
        GetSoundsManager().Play(index);
    }
    
    public void Stop(int index)
    {
        GetSoundsManager().Stop(index);
    }

    private static SoundsManager GetSoundsManager()
    {
        if (soundsManager == null)
        {
            soundsManager = GameObject.FindWithTag("SoundsManager").GetComponent<SoundsManager>();
        }

        return soundsManager;
    }
}