using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour {

    #region Public Variables
    [Header("Components Variables")]
    public NavMeshAgent myAgent;
    public AudioSource myAudio;
    public SkinnedMeshRenderer myRenderer;
    private Material myMat;

    [Header("Distance Variables")]
    public float scapeSpeed = 5;

    [Header("Distance Variables")]
    public float distanceToStop = 1;
    public float distanceToDesapear = 1;
    #endregion

    private Transform player;
    private Transform lightPlayer;

    private float rotationToScape;

    private Vector3 scapePoint;
    private IEnemyState currentState;

    void Start ()
    {
        player = GameManager.Instance.GetMainPlayerTransform();
        lightPlayer = GameManager.Instance.GetLightPlayerTransform();
        myAudio = GetComponent<AudioSource>();

        myMat = myRenderer.material;
    }
	
	void Update ()
    {
        if (currentState == null)
            return;

        currentState.Execute();

        CheckLightDistance();
    }

    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);
    }

    public void StartBehaviour()
    {
        myAgent.enabled = true;
        myAgent.isStopped = false;

        if (myMat == null)
        {
            myMat = myRenderer.material;
        }
        myMat.SetColor("_EmissionColor", Color.red);

        ChangeState(new ChaseState());
    }

    public void ChasePlayer()
    {
        myAgent.isStopped = false;
        myAgent.SetDestination(player.position);
    }

    public void LookAtPlayer()
    {
        Vector3 positionToLook = player.position;
        positionToLook.y = transform.position.y;
        transform.LookAt(positionToLook);
    }

    public void ChangeScapeMaterial()
    {
        //cambiar mat
        myMat.SetColor("_EmissionColor", Color.yellow);
    }

    public void PlayScapeSound()
    {
        myAudio.clip = SoundManager.Instance.GetSoundByRequest(SoundManager.SoundRequest.E_Scape);
        myAudio.Play();
    }

    public void BlockPlayerActions()
    {
        player.GetComponent<BlindController>().BlockMovement();
        //Sound 2d
        myAudio.spatialBlend = 0;
    }

    public void UnblockPlayerActions()
    {
        player.GetComponent<BlindController>().UnblockMovement();
        //Sound 3d
        myAudio.spatialBlend = 1;
    }

    public void MoveAgain()
    {
        myAgent.isStopped = false;
    }

    public void StopMovement()
    {
        myAgent.isStopped = true;
    }

    public void DetectScapePoint()
    {
        Vector3 vectorReference = (transform.position - player.position);
        vectorReference.y = 0;
        vectorReference.Normalize();

        scapePoint = transform.position + vectorReference * 10;
        myAgent.enabled = false;

        rotationToScape = Vector3.Angle(transform.forward, (scapePoint - transform.position).normalized) + transform.eulerAngles.y;
    }

    public void RunToScapePoint()
    {
        transform.position += (scapePoint - transform.position).normalized * scapeSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(new Vector3(0, Mathf.Lerp(transform.eulerAngles.y, rotationToScape, Time.deltaTime * 2),0));
    }

    public void CheckLightDistance()
    {
        if (Vector3.Distance(lightPlayer.position, transform.position) < GameManager.Instance.GetRadiusOfLight() && currentState.GetType() != typeof(ScapeState))
            ChangeState(new ScapeState());
    }

    public void Destroy()
    {
        //Particulas de desparacer
        gameObject.SetActive(false);
        EnemyManager.Instance.ReturnEnemy(gameObject);
    }

    public bool IsPlayerInRange()
    {
        return Vector3.Distance(player.position, transform.position) < distanceToStop;
    }

    public Vector3 GetScapePoint()
    {
        return scapePoint;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceToStop);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, distanceToDesapear);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, GameManager.Instance.GetRadiusOfLight());
    }

}
