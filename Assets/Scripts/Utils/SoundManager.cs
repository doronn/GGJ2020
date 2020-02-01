using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace UnityTemplateProjects
{
    public class SoundManager : BaseSingleton<SoundManager>
    {
        //start should subscribe (eventmanager.subscribe) give it a function that resource.loads the sound
        [SerializeField]
        public AudioSource beepSource;
        public void Start()
        {
            EventManager.GetInstance().Subscribe(GGJEventType.SoundHornEvent, SoundHorn);
        }

        public void SoundHorn()
        {
            //var sound = Resources.Load("Sounds/Horn");
            if (Random.Range(0f, 4f) < 1f)
            {

                beepSource.Play();
            }
        }
    }
}