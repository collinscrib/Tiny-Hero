using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordChanger : MonoBehaviour
{
    Game_Manager gm;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<Game_Manager>();
        gm.updateSword(gameObject);
    }
}
