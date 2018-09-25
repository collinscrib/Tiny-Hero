using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTrap : MonoBehaviour {

    public int speed = 300;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0,Time.deltaTime * speed, 0));
	}
}
