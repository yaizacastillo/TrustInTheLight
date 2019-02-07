using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager> {

    public enum SoundRequest { P_HereCall, P_StopCall, P_ActionCall, P_OpenDoor, P_HitSound, P_StepSound, P_Stress, E_Scape, E_Distortion , C_Sounds, P_Behind }

    #region Public Variables
    [Header("\t    Own Script Variables")]
    [Header("Player Sounds")]
    [Tooltip("List of here calls")]
    public List<AudioClip> hereCalls;
    [Tooltip("List of stop calls")]
    public List<AudioClip> stopCalls;
    [Tooltip("List of action calls")]
    public List<AudioClip> actionCalls;
    [Tooltip("List of open door sounds")]
    public List<AudioClip> openSound;
    [Tooltip("List of hit sounds")]
    public List<AudioClip> hitSound;
    [Tooltip("List of step sounds")]
    public List<AudioClip> stepsSounds;
    [Tooltip("List of stress sounds")]
    public List<AudioClip> stressSounds;
    [Tooltip("List of scape sounds")]
    public List<AudioClip> scapeEnemySounds;
    [Tooltip("List of distorsion sounds")]
    public List<AudioClip> distortionSounds;
    [Tooltip("List of car sounds")]
    public List<AudioClip> carSounds;
    [Tooltip("List of behind sounds")]
    public List<AudioClip> behindSounds;
    #endregion

    #region Sound Getters Methods
    public AudioClip GetSoundByRequest(SoundRequest sr)
    {
        //Example:
        switch (sr)
        {
            case SoundRequest.P_HereCall:
                return GetHereSound();
            case SoundRequest.P_StopCall:
                return GetStopSound();
            case SoundRequest.P_ActionCall:
                return GetActionSound();
            case SoundRequest.P_OpenDoor:
                return GetOpenDoorSound();
            case SoundRequest.P_StepSound:
                return GetStepSound();
            case SoundRequest.P_HitSound:
                return GetHitSound();
            case SoundRequest.P_Stress:
                return GetStressSound();
            case SoundRequest.E_Scape:
                return GetEnemyScapeSound();
            case SoundRequest.E_Distortion:
                return GetEnemyDistortionSound();
            case SoundRequest.C_Sounds:
                return GetCarsSound();
            case SoundRequest.P_Behind:
                return GetBehindSound();
        }

        return null;
    }

    //Player Sounds
    private AudioClip GetHereSound()
    {
        return hereCalls[Random.Range(0, hereCalls.Count)];
    }

    private AudioClip GetStopSound()
    {
        return stopCalls[Random.Range(0, stopCalls.Count)];
    }

    private AudioClip GetActionSound()
    {
        return actionCalls[Random.Range(0, actionCalls.Count)];
    }

    private AudioClip GetOpenDoorSound()
    {
        return openSound[Random.Range(0, openSound.Count)];
    }

    private AudioClip GetHitSound()
    {
        return hitSound[Random.Range(0, hitSound.Count)];
    }

    private AudioClip GetStepSound()
    {
        return stepsSounds[Random.Range(0, stepsSounds.Count)];
    }

    private AudioClip GetStressSound()
    {
        return stressSounds[Random.Range(0, stressSounds.Count)];
    }

    private AudioClip GetEnemyScapeSound()
    {
        return scapeEnemySounds[Random.Range(0, scapeEnemySounds.Count)];
    }

    private AudioClip GetEnemyDistortionSound()
    {
        return distortionSounds[Random.Range(0, distortionSounds.Count)];
    }

    private AudioClip GetCarsSound()
    {
        return carSounds[Random.Range(0, carSounds.Count)];
    }

    private AudioClip GetBehindSound()
    {
        return behindSounds[Random.Range(0, behindSounds.Count)];
    }
    #endregion

}
