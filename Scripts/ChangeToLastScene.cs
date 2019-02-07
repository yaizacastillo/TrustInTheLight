using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToLastScene : MonoBehaviour {

    public int scene = 3;

	public void ChangeScene()
    {
        SceneManager.LoadScene(scene);
    }

    public void PlayMusic()
    {
        MusicSingleton.Instance.PlayMusic();
    }
}
