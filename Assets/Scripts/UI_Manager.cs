using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject isoCamera;
    public Transform spawn1;
    public Transform spawn2;
    public Transform spawn3;
    public Transform spawn4;
    public Text NotEnoughMoneyText;
    public GameObject[] balistas;
    public AudioClip song1;
    public AudioClip song2;
    public AudioClip song3;
    public AudioClip song4;

    [SerializeField]
    private GameObject lastPlayer;
    private Game_Manager gm;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<Game_Manager>();
        disableAll();
        PlaySong(1);
    }

    public void PlaySong(int n)
    {
        Debug.Log("Playing song");
        if (n == 1)
        {
            isoCamera.GetComponent<AudioSource>().clip = song1;
        }
        if (n == 2)
        {
            isoCamera.GetComponent<AudioSource>().clip = song2;
        }
        if (n == 3)
        {
            isoCamera.GetComponent<AudioSource>().clip = song3;
        }
        if (n == 4)
        {
            isoCamera.GetComponent<AudioSource>().clip = song4;
        }
        isoCamera.GetComponent<AudioSource>().Play();
    }

    public void StopMusic()
    {

    }

    public void RespawnPlayer()
    {
        Destroy(lastPlayer);

        //gm.RecolorKnight();

        GameObject obj;
        Vector3 pos = Vector3.up;

        if (gm.getArea() == 1)
        {
            pos = spawn1.position;
        }
        if (gm.getArea() == 2)
        {
            pos = spawn2.position;
        }
        if (gm.getArea() == 3)
        {
            pos = spawn3.position;
        }
        if (gm.getArea() == 4)
        {
            pos = spawn4.position;
        }

        obj = Instantiate(playerPrefab, pos, Quaternion.identity) as GameObject;
        isoCamera.GetComponent<CameraFollow>().setTarget(obj);
        lastPlayer = obj;

        disableAll();

        foreach (GameObject b in balistas)
        {
            b.GetComponent<Balista>().ReloadBalista();
        }

        //gm.updateChestSword();
        //gm.RecolorKnight();
    }

    public GameObject getCurrentPlayer()
    {
        return lastPlayer;
    }

    public void openShop()
    {
        gm.enableShop();
        gm.disableEscapeMenu();
        NotEnoughMoneyText.enabled = false;
    }

    public void closeShop()
    {
        gm.disableShop();
        gm.enableEscapeMenu();
        NotEnoughMoneyText.enabled = false;
    }

    public void Quit()
    {
        gm.enableAreYouSure();
        //gm.disableEscapeMenu();
    }

    public void Yes()
    {
        Application.Quit();
    }

    public void No()
    {
        gm.disableAreYouSure();
        gm.enableEscapeMenu();
        NotEnoughMoneyText.enabled = false;
    }

    public void disableAll()
    {
        gm.disableAreYouSure();
        gm.disableEscapeMenu();
        gm.disableShop();
        gm.disableHelpButton();
        gm.disableHelpMenu();
        NotEnoughMoneyText.enabled = false;
    }

    public void NotEnoughMoney()
    {
        Debug.Log("Not Enough Money.");
        NotEnoughMoneyText.enabled = true;
    }

    public void cancelMoneyNotification()
    {
        NotEnoughMoneyText.enabled = false;
    }

}