using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour {
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.layer == 9)
		{
			other.gameObject.GetComponent<Player>().TriggerDeath();
			Destroy(other.gameObject);
		}
	}
}
