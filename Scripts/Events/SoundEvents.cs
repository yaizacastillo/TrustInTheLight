using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEvents : MonoBehaviour {

    private AudioSource myAudio;

    private void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    public void PlayEvent()
    {
        if (!enabled)
            return;

        myAudio.Play();
        enabled = false;
    }

}
