using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
	private float offsetX;
	private float offsetZ;

	void Start()
	{
		offsetX = target.transform.position.x - transform.position.x;
		offsetZ = target.transform.position.z - transform.position.z;
	}

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 gotoLocation = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
			gotoLocation.x -= offsetX;
			gotoLocation.z -= offsetZ;

            transform.position = Vector3.Lerp(transform.position, gotoLocation, speed * Time.deltaTime);
        }
    }

	internal void setTarget(GameObject obj)
	{
		target = obj.transform;
	}
}
