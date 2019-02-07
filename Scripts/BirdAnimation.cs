using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAnimation : MonoBehaviour
{
    public Animator myAnimator;
    public AudioSource myAudio;

    private bool flying;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !flying)
        {
            myAnimator.SetTrigger("Fly");
            flying = true;

            Invoke("AutoDestroy", 2.0f);

            myAudio.Play();
        }
    }

    private void AutoDestroy()
    {
        gameObject.SetActive(false);
    }
}
