using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public class BlindController : MonoBehaviour {

    #region Public Variables
    [Header("Movement Variables")]
    public float speed;
    public float speedRotation;

    [Header("Player Reference")]
    public Transform lightPlayer;

    [Header("Ray Variables")]
    public float rayDistance = 2;

    public float whiskersAngle = 30;
    public float wallsCheckerDistance = 3;
    public float yDetectionPoint = 0.2f;

    [Header("Component Variables")]
    public AudioSource myAudio;
    public Rigidbody myRGB;
    public Animator myAnimator;

    public GameObject objec;

    [Header("Animator Variables")]
    public float minSpeedWalk = 0.8f;
    public float maxSpeedWalk = 1.8f;
    #endregion

    #region Private Variables
    private bool movementBlocked;
    private float forwardInput;
    private float lateralInput;

    private int enemiesBlocking = 0;
    #endregion

    private void Start()
    {
        if (myAudio == null)
            myAudio = GetComponent<AudioSource>();

        if (myRGB == null)
            myRGB = GetComponent<Rigidbody>();

        if (lightPlayer == null)
            lightPlayer = GameManager.Instance.GetLightPlayerTransform();

        if (myAnimator == null)
            myAnimator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Time.timeScale == 0)
            return;

        InputHandler();
        MoveHandler();
        CheckWalls();
    }

    private void InputHandler()
    {
        lateralInput = InputsManager.Instance.GetMovementX(true);
        forwardInput = InputsManager.Instance.GetMovementY(true);

        if (InputsManager.Instance.GetActionButtonInputDown(true))
        {
            Action();
        }
    }

    private void MoveHandler()
    {
        if (movementBlocked)
            return;

        //Movement
        Vector3 direction = new Vector3(lateralInput, 0, forwardInput);

        myRGB.velocity = direction * speed * Time.deltaTime;

        if(lightPlayer.GetComponent<LightController>().IsCallRequiered() && direction != Vector3.zero)
        {
            if (Vector3.Angle(direction, (transform.position - lightPlayer.transform.position).normalized) < 30 &&
                GameManager.Instance.GetPercentLightToPlayerDistance() < 0.25f)
            {
                lightPlayer.GetComponent<LightController>().CallBehindYou();
                RequestSound(SoundManager.SoundRequest.P_Behind);
            }
        }

        //Animation
        myAnimator.SetFloat("Walk", direction.magnitude);

        if (direction.magnitude > 0)
        {
            myAnimator.speed = Mathf.Lerp(minSpeedWalk, maxSpeedWalk, direction.magnitude);
        }
        else
        {
            myAnimator.speed = 1;
        }

        //Rotation
        if (direction != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, direction, Time.deltaTime * speedRotation);
        }

        //Move the light
        if (Vector3.Distance(transform.position, lightPlayer.transform.position) > GameManager.Instance.maxDistanceBetweenPlayers)
        {
            lightPlayer.transform.position = transform.position + (lightPlayer.transform.position - transform.position).normalized * GameManager.Instance.maxDistanceBetweenPlayers;
        }
    }

    public void CheckWalls()
    {
        if (movementBlocked)
            return;

        //Right whisker
        Ray ray = new Ray(transform.position + Vector3.up * yDetectionPoint, (Quaternion.Euler(transform.up * whiskersAngle) * transform.forward));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, wallsCheckerDistance, LayerMask.GetMask("WallLayer")))
        {
            float distanceOfHit = (hit.point - transform.position + Vector3.up * yDetectionPoint).magnitude;
            InputsManager.Instance.RightTriggerPlayer1Value((1 - (distanceOfHit / wallsCheckerDistance))/InputsManager.Instance.vibrateReductionValue);
        }
        else
        {
            InputsManager.Instance.RightTriggerPlayer1Value(0);
        }

        //Left whisker
        ray = new Ray(transform.position + Vector3.up * yDetectionPoint, (Quaternion.Euler(transform.up * -whiskersAngle) * transform.forward));

        if (Physics.Raycast(ray, out hit, wallsCheckerDistance, LayerMask.GetMask("WallLayer")))
        {
            float distanceOfHit = (hit.point - transform.position + Vector3.up * yDetectionPoint).magnitude;
            InputsManager.Instance.LeftTriggerPlayer1Value((1 - (distanceOfHit / wallsCheckerDistance))/ 1.2f);
        }
        else
        {
            InputsManager.Instance.LeftTriggerPlayer1Value(0);
        }

    }

    private void Action()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.5f, transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            //if (hit.transform.tag == "Stairs")
            //{
            //    //Subir escaleras
            //}

            objec = hit.transform.gameObject;

            if (hit.transform.tag == "Door")
            {
                if(hit.transform.name == "EndDoor")
                {
                    SceneManager.LoadScene(2);
                }

                else
                {
                    hit.transform.GetComponent<DoorScript>().OpenDoor();
                    RequestSound(SoundManager.SoundRequest.P_OpenDoor);
                }

            }
        }
    }

    public void StepSound()
    {
        RequestSound(SoundManager.SoundRequest.P_StepSound);
    }

    private void RequestSound(SoundManager.SoundRequest sr)
    {
        myAudio.clip = SoundManager.Instance.GetSoundByRequest(sr);
        myAudio.Play();
    }

    public void RequestAction()
    {
        Ray ray = new Ray(transform.position+Vector3.up*0.5f, transform.forward);

        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.transform.tag == "Door")
            {
                //Pedir abrir puerta
                myAudio.clip = SoundManager.Instance.GetSoundByRequest(SoundManager.SoundRequest.P_ActionCall);
                myAudio.Play();
            }
        }
    }

    public void BlockMovement()
    {
        movementBlocked = true;
        myAnimator.SetFloat("Walk", 0);
        myRGB.velocity = Vector3.zero;

        InputsManager.Instance.RightTriggerPlayer1Value(1 / InputsManager.Instance.vibrateReductionValue);
        InputsManager.Instance.LeftTriggerPlayer1Value(1 / 1.2f);

        lightPlayer.GetComponent<LightController>().Danger();

        enemiesBlocking++;
    }

    public void UnblockMovement()
    {
        enemiesBlocking--;

        if (enemiesBlocking <= 0)
        {
            movementBlocked = false;

            lightPlayer.GetComponent<LightController>().OutOfDanger();
        }

    }

    public void OnApplicationQuit()
    {
        InputsManager.Instance.RightTriggerPlayer1Value(0);
        InputsManager.Instance.LeftTriggerPlayer1Value(0);

        lightPlayer.GetComponent<LightController>().OutOfDanger();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position + Vector3.up, transform.position + Vector3.up + transform.forward * rayDistance);
        Gizmos.DrawWireSphere(transform.position, GameManager.Instance.maxDistanceBetweenPlayers);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position + Vector3.up * yDetectionPoint, transform.position + Vector3.up * yDetectionPoint + (Quaternion.Euler(transform.up * whiskersAngle) * transform.forward * wallsCheckerDistance));
        Gizmos.DrawLine(transform.position + Vector3.up * yDetectionPoint, transform.position + Vector3.up * yDetectionPoint + (Quaternion.Euler(transform.up * -whiskersAngle) * transform.forward * wallsCheckerDistance));
    }
}
