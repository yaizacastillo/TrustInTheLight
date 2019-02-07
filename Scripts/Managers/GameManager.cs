using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager> {

    #region Public Variables
    [Header("Players References")]
    public GameObject mainPlayer;
    public GameObject lightPlayer;
    public float maxDistanceBetweenPlayers = 4;
    public GameObject pauseMenu;
    public bool isPaused = false;

    [Header("Music References")]
    public GameObject outsideMusic;
    public GameObject schoolMusic;

    [Header("Shaders Variables")]
    public string shaderProperty;
    public float maxRingRadius = 5;
    public float minRingRadius = 3.5f;
    #endregion

    private void Update()
    {
        if (InputsManager.Instance.GetStartButtonDown())
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }

            else
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    #region Getters
    public Transform GetMainPlayerTransform()
    {
        return mainPlayer.transform;
    }

    public Transform GetLightPlayerTransform()
    {
        return lightPlayer.transform;
    }

    public string GetShaderPropertyName()
    {
        return shaderProperty;
    }

    public BlindController GetBlindController()
    {
        return mainPlayer.GetComponent<BlindController>();
    }

    public float GetPercentLightToPlayerDistance()
    {
        return (1 - Vector3.Distance(GetMainPlayerTransform().position, GetLightPlayerTransform().position) / maxDistanceBetweenPlayers);
    }

    public float GetRadiusOfLight()
    {
        return Mathf.Lerp(minRingRadius, maxRingRadius, GetPercentLightToPlayerDistance());
    }

    public void ChangeMusic()
    {
        outsideMusic.SetActive(false);
        schoolMusic.SetActive(true);
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(GetMainPlayerTransform().position + Vector3.up, GetMainPlayerTransform().position + Vector3.up + (GetLightPlayerTransform().position - GetMainPlayerTransform().position) * maxDistanceBetweenPlayers);
    }

}
