using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == 9) // if player
		{
			other.gameObject.GetComponent<Player>().TriggerDeath();
		}
	}

}
