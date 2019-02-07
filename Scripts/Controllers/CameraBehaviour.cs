using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    #region Public Variables
    [Header("Camera Variables")]
    public float camaraDistance = 10;
    public float cameraRotation;
    //public float xOffset = 2;
    public float yOffset = 5;

    [Header("Player References")]
    public Transform lightPlayer;
    #endregion

    private void Start () {
        lightPlayer = GameManager.Instance.GetLightPlayerTransform();
    }
	
	void Update () {
        transform.position = lightPlayer.position - transform.forward * camaraDistance + Vector3.up * yOffset;
        transform.eulerAngles = new Vector3(cameraRotation, transform.eulerAngles.y, transform.eulerAngles.z);
	}

    private void OnDrawGizmosSelected()
    {
        transform.position = lightPlayer.position - transform.forward * camaraDistance + Vector3.up * yOffset;
        transform.eulerAngles = new Vector3(cameraRotation, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
