using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenerFollower : MonoBehaviour {

    private Transform target;

    private void Start()
    {
        target = GameManager.Instance.GetMainPlayerTransform();
    }

	private void Update ()
    {
        transform.position = target.position + Vector3.up;
	}
}
