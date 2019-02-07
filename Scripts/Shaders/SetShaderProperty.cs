using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetShaderProperty : MonoBehaviour {

    public bool affectedByDistance = true;
    public bool debugar;

    #region Private Variables
    private string propertyName;
    private Material mat;
    private Transform player;
    #endregion

    #region Unity Methods
    private void Start()
    {
        //Components
        if (GetComponent<MeshRenderer>() != null)
            mat = GetComponent<MeshRenderer>().material;
        else
            mat = GetComponent<SkinnedMeshRenderer>().material;

        //GameManager
        propertyName = GameManager.Instance.GetShaderPropertyName();
        player = GameManager.Instance.GetLightPlayerTransform();

    }

    private void Update()
    {
        if (player != null)
        {
            mat.SetVector(propertyName, player.position);
            if (affectedByDistance)
            {
                mat.SetFloat("_RingSize", GameManager.Instance.GetRadiusOfLight());
            }

        }
        else
            Debug.Log("No player");
    }
    #endregion

}
