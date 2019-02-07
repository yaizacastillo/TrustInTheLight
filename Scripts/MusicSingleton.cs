using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSingleton : MonoSingleton<MusicSingleton>
{
    public AudioSource myAudio;

	void Start () {
        myAudio = GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);
	}
	
    public void PlayMusic()
    {
        myAudio.Play();
    }

}
