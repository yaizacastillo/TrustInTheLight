using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LightController : MonoBehaviour {

    #region Public Variables
    [Header("Movement Variables")]
    public float speed;
    public float rotationSpeed;

    [Header("Character Reference")]
    public Transform mainPlayerTrans;
    public Transform lightTrans;

    [Header("Time Variables")]
    public float timeBetweenCalls = 3;
    public float callTime = 2;

    [Header("Distance Variables")]
    [Range(0, 5)]public float groundDistance;

    [Header("Component Variables")]
    public AudioSource myAudio;
    #endregion

    #region Private Variables
    private float forwardInput;
    private float lateralInput;
    private float timerBetweenCalls;
    private float callTimer;
    private bool callRequiered;
    #endregion

    private void Start()
    {
        if (myAudio == null)
            myAudio = GetComponent<AudioSource>();

        transform.position = mainPlayerTrans.position + mainPlayerTrans.forward + Vector3.up * groundDistance;
    }

    private void Update ()
    {
        InputHandler();

        if (!myAudio.isPlaying)
        {
            MoveHandler();
        }

        Timer();
	}

    private void InputHandler()
    {
        forwardInput = InputsManager.Instance.GetMovementY(false);
        lateralInput = InputsManager.Instance.GetMovementX(false);

        if (InputsManager.Instance.GetHereButtonDown() && timerBetweenCalls > timeBetweenCalls)
        {
            RequestSound(SoundManager.SoundRequest.P_HereCall);
            callRequiered = true;
            timerBetweenCalls = 0;
        }
        if (InputsManager.Instance.GetStopButtonDown())
        {
            RequestSound(SoundManager.SoundRequest.P_StopCall);
        }
        if (InputsManager.Instance.GetActionButtonInputDown(false))
        {
            GameManager.Instance.GetBlindController().RequestAction();
        }
    }

    private void MoveHandler()
    {
        Ray ray = new Ray(transform.position, -transform.up);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, groundDistance * 2, LayerMask.GetMask("GroundLayer")))
        {
            transform.position = new Vector3(transform.position.x, (hit.point + (Vector3.up * groundDistance)).y, transform.position.z);
        }

        Vector3 direction = new Vector3(lateralInput, 0, forwardInput);

        if (!(Vector3.Distance(transform.position, mainPlayerTrans.position) > GameManager.Instance.maxDistanceBetweenPlayers &&
            Vector3.Angle(direction, (mainPlayerTrans.position - transform.position).normalized) > 100))
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    public void Danger()
    {
        InputsManager.Instance.RightTriggerPlayer2Value(1 / InputsManager.Instance.vibrateReductionValue);
        InputsManager.Instance.LeftTriggerPlayer2Value(1 / 1.2f);
    }

    public void OutOfDanger()
    {
        InputsManager.Instance.RightTriggerPlayer2Value(0);
        InputsManager.Instance.LeftTriggerPlayer2Value(0);
    }

    public bool IsCallRequiered()
    {
        return callRequiered;
    }

    public void CallBehindYou()
    {
        Debug.Log("behind you");
        callRequiered = false;
        callTimer = 0;
    }

    public void Timer()
    {
        if (callRequiered)
        {
            callTimer += Time.deltaTime;

            if (callTimer >= callTime)
            {
                callTimer = 0;
                callRequiered = false;
            }
        }

        timerBetweenCalls += Time.deltaTime;
    }

    private void RequestSound(SoundManager.SoundRequest sr)
    {
        myAudio.clip = SoundManager.Instance.GetSoundByRequest(sr);
        myAudio.Play();
    }

}
