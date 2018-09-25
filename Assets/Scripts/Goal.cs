using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

	public Transform teleportPosition;
	public Material skyboxChange;
	public Game_Manager gm;
	public int songNumber;

	void OnTriggerEnter(Collider other)
	{
		gm.addCoins(other.gameObject.GetComponent<Player>().getDistance());
		gm.setArea(gm.area + 1);
		gm.TriggerRespawn();
		other.transform.position = teleportPosition.position;
		RenderSettings.skybox = skyboxChange;

		gm.play_song(songNumber);
		if(other.gameObject.layer == 9)
		{
			gm.addCoins(1000);
		}
	}
}
