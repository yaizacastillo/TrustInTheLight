using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSoundScript : MonoBehaviour {

    public float timeBetweenCars;

    private AudioSource myAudio;

    private float timer;

    private void Start()
    {
        myAudio = GetComponent<AudioSource>();
        timeBetweenCars = 1;
    }

    private void Update()
    {
        if (!myAudio.isPlaying)
        {
            timer += Time.deltaTime;

            if (timer > timeBetweenCars)
            {
                PlayRandomSound();
                timeBetweenCars = Random.Range(3, 5);
                timer = 0;
            }
        }
    }

    public void PlayRandomSound()
    {
        myAudio.clip = SoundManager.Instance.GetSoundByRequest(SoundManager.SoundRequest.C_Sounds);
        myAudio.Play();
    }

}
