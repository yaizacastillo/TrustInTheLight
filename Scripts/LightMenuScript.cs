using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMenuScript : MonoBehaviour {

    public AnimationCurve curve;
    public Light myLight;

    public float speed = 1;

    public float minLight = 30f;
    public float maxLight = 45f;

    private float timer;

	void Update ()
    {
        timer += Time.deltaTime * speed;

        if (timer > 1)
            timer = 0;

        myLight.range = Mathf.Lerp(minLight, maxLight, curve.Evaluate(timer));
	}
}
