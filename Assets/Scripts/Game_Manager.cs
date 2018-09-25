using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
    public UI_Manager uim;
    public GameObject playerPrefab;
    public Text coinText;
    public GameObject respawnButton;
    public Text respawnButton_text;
    public GameObject shopButton;
    public Text shopButton_text;
    public GameObject quitButton;
    public Text quitButton_text;
    public GameObject areYouSure;
    public Text areYouSure_text;
    public GameObject YesButton;
    public Text yes_text;
    public GameObject NoButton;
    public Text no_text;
    public Text speedText;
    public Text jumpText;
    public Text aeroText;
    public Text speedPrice;
    public Text jumpPrice;
    public Text aeroPrice;
    public GameObject[] shopButtons;
    public Text[] shopTexts;
    public Image[] shopImages;
    public Image greenX;
    public Image redX;
    public Image purpleX;
    public Image whiteX;
    public Image speedX;
    public Image jumpX;
    public Image aeroX;
    public Material Green;
    public Material Red;
    public Material Purple;
    public Material White;
    public GameObject Chest;
    public GameObject Sword;
    public Text winText;
    public Button winButton;
    public Text helpText;
    public Text deathStats;
    public Text meterStats;
    public Button helpButton;
    public Image helpImage;
    public Image helpBG;
    private bool help = false;
    
    [SerializeField]
    private int coins = 0;
    [SerializeField]
    private int deathCount = 0;
    [SerializeField]
    private int metersWalked = 0;
    private int speedCount = 0;
    private int jumpCount = 0;
    private int aeroCount = 0;
    private int price_speed = 100;
    private int price_jump = 100;
    private int price_aero = 100;
    public int area;
    private bool purchasedGreen = false;
    private bool purchasedRed = false;
    private bool purchasedPurple = false;
    private bool purchasedWhite = false;
    private bool isGreen;
    private bool isRed;
    private bool isPurple;
    private bool isWhite;
    private int maxPrice = 10000;

    void Start()
    {
        playerPrefab.GetComponent<Player>().walkSpeed = 6f;
        playerPrefab.GetComponent<Player>().jumpSpeed = 14f;
        playerPrefab.GetComponent<Player>().airFrictionFactor = 10f;
        disableAreYouSure();
        disableEscapeMenu();

        //updateChestSword();

        speedCount = 0;
        jumpCount = 0;
        aeroCount = 0;

        speedX.enabled = false;
        jumpX.enabled = false;
        aeroX.enabled = false;

        area = 1;

        winText.enabled = false;
        winButton.GetComponent<Image>().enabled = false;
        deathStats.enabled = false;
        meterStats.enabled = false;
    }

    void Update()
    {
        coinText.text = coins.ToString();

        if (speedCount > 4) speedCount = 4;
        if (jumpCount > 4) jumpCount = 4;
        if (aeroCount > 4) aeroCount = 4;
        speedText.text = "Speed Level:  " + speedCount.ToString();
        jumpText.text = "Jump Level:  " + jumpCount.ToString();
        aeroText.text = "Aerodynamics Level:  " + aeroCount.ToString();

        speedPrice.text = price_speed.ToString();
        jumpPrice.text = price_jump.ToString();
        aeroPrice.text = price_aero.ToString();

        RecolorKnight();

        if(help)
        {
            enableHelpMenu();
        }
        else
        {
            disableHelpMenu();
        }
    }

    public void play_song(int n)
    {
        uim.PlaySong(n);
    }

    public void helpButtonManager()
    {
        help = !help;
    }

    public void enableHelpMenu()
    {
        helpImage.enabled = true;
        helpBG.GetComponent<Image>().enabled = true;
    }

    public void disableHelpMenu()
    {
        helpImage.enabled = false;
        helpBG.GetComponent<Image>().enabled = false;
    }

    public void enableHelpButton()
    {
        helpButton.GetComponent<Image>().enabled = true;
        helpButton.GetComponent<Button>().enabled = true;
        helpText.enabled = true;
    }

    public void disableHelpButton()
    {
        helpButton.GetComponent<Image>().enabled = false;
        helpButton.GetComponent<Button>().enabled = false;
        helpText.enabled = false;
    }

    public void playerDied()
    {
        deathCount++;
    }

    public int getCoins()
    {
        return coins;
    }

    public int getDeathCount()
    {
        return deathCount;
    }

    public int getDistanceHistory()
    {
        return metersWalked;
    }

    public void addDistance(int n)
    {
        metersWalked += n;
    }

    public void addCoins(int n) // Schmeckels....
    {
        float g = 1f;
        if(area == 1) g = 1f;
        if(area == 2) g = 2f;
        if(area == 3) g = 12f;
        coins += (int)(n * g);
        addDistance(n);
    }

    public void deductCoins(int n) // NO! NOT MY SCHMECKELS!
    {
        coins -= n;
    }

    public void buySpeed()
    {
        if (coins >= price_speed && speedCount < 4)
        {
            uim.cancelMoneyNotification();
            deductCoins(price_speed);
            speedCount += 1;

            playerPrefab.GetComponent<Player>().walkSpeed += .5f;
            if (playerPrefab.GetComponent<Player>().walkSpeed >= 8f)
                playerPrefab.GetComponent<Player>().walkSpeed = 8f;

            price_speed *= 5;
            if (price_speed > maxPrice) price_speed = maxPrice;
        }
        else
        {
            uim.NotEnoughMoney();
        }

        if (speedCount == 4)
        {
            speedX.enabled = true;
        }
        
    }

    public void buyJump()
    {
        if (coins >= price_jump && jumpCount < 4)
        {
            uim.cancelMoneyNotification();
            deductCoins(price_jump);
            jumpCount += 1;

            playerPrefab.GetComponent<Player>().jumpSpeed += 1f;
            if (playerPrefab.GetComponent<Player>().jumpSpeed >= 18f)
                playerPrefab.GetComponent<Player>().jumpSpeed = 18f;

            price_jump *= 5;
            if (price_jump > maxPrice) price_jump = maxPrice;
        }
        else
        {
            uim.NotEnoughMoney();
        }
        
        if (jumpCount == 4)
        {
            jumpX.enabled = true;
        }
    }

    public void buyAero()
    {
        if (coins >= price_aero && aeroCount < 4)
        {
            uim.cancelMoneyNotification();
            deductCoins(price_aero);
            aeroCount += 1;

            playerPrefab.GetComponent<Player>().airFrictionFactor -= 2f;
            if (playerPrefab.GetComponent<Player>().airFrictionFactor <= 2f)
                playerPrefab.GetComponent<Player>().airFrictionFactor = 2f;

            price_aero *= 5;
            if (price_aero > maxPrice) price_aero = maxPrice;
        }
        else
        {
            uim.NotEnoughMoney();
        }

        if (aeroCount == 4)
        {
            aeroX.enabled = true;
        }
    }

    public void buyColor(int n)
    {
        uim.cancelMoneyNotification();
        if (n == 1) // green
        {
            if(purchasedGreen)
            {
                isGreen = true;
                isRed = false;
                isPurple = false;
                isWhite = false;
            }
            if (coins >= 1000 && !purchasedGreen)
            {
                deductCoins(1000);

                isGreen = true;
                isRed = false;
                isPurple = false;
                isWhite = false;

                purchasedGreen = true;
                greenX.enabled = true;
            } 
            else if (coins < 1000)
            {
                uim.NotEnoughMoney();
            }
        }
        if (n == 2) // red
        {
            if(purchasedRed)
            {
                isGreen = false;
                isRed = true;
                isPurple = false;
                isWhite = false;
            }
            if (coins >= 1000 && !purchasedRed)
            {
                deductCoins(1000);

                isGreen = false;
                isRed = true;
                isPurple = false;
                isWhite = false;

                purchasedRed = true;
                redX.enabled = true;
            } 
            else if (coins < 1000)
            {
                uim.NotEnoughMoney();
            }
        }
        if (n == 3) // purple
        {
            if(purchasedPurple)
            {
                isGreen = false;
                isRed = false;
                isPurple = true;
                isWhite = false;
            }
            if (coins >= 1000 && !purchasedPurple)
            {
                deductCoins(1000);

                isGreen = false;
                isRed = false;
                isPurple = true;
                isWhite = false;

                purchasedPurple = true;
                purpleX.enabled = true;
            } 
            else if (coins < 1000)
            {
                uim.NotEnoughMoney();
            }
        }
        if (n == 4) // white
        {
            if(purchasedWhite)
            {
                isGreen = false;
                isRed = false;
                isPurple = false;
                isWhite = true;
            }
            if (coins >= 1000 && !purchasedWhite)
            {
                deductCoins(1000);

                isGreen = false;
                isRed = false;
                isPurple = false;
                isWhite = true;

                purchasedWhite = true;
                whiteX.enabled = true;
            }
            else if (coins < 1000)
            {
                uim.NotEnoughMoney();
            }
        }
    }

    public void TriggerRespawn()
    {
        uim.RespawnPlayer();
        uim.cancelMoneyNotification();
    }

    public void setArea(int n)
    {
        area = n;
    }

    public int getArea()
    {
        return area;
    }

    public void enableEscapeMenu()
    {
        respawnButton.GetComponent<Image>().enabled = true;
        respawnButton.GetComponent<Button>().enabled = true;
        respawnButton_text.enabled = true;
        shopButton.GetComponent<Image>().enabled = true;
        shopButton.GetComponent<Button>().enabled = true;
        shopButton_text.enabled = true;
        quitButton.GetComponent<Image>().enabled = true;
        quitButton.GetComponent<Button>().enabled = true;
        quitButton_text.enabled = true;
        enableHelpButton();
    }

    public void disableEscapeMenu()
    {
        respawnButton.GetComponent<Image>().enabled = false;
        respawnButton.GetComponent<Button>().enabled = false;
        respawnButton_text.enabled = false;
        shopButton.GetComponent<Image>().enabled = false;
        shopButton.GetComponent<Button>().enabled = false;
        shopButton_text.enabled = false;
        quitButton.GetComponent<Image>().enabled = false;
        quitButton.GetComponent<Button>().enabled = false;
        quitButton_text.enabled = false;
        disableHelpButton();
    }

    public void enableAreYouSure()
    {
        disableEscapeMenu();
        areYouSure.GetComponent<Image>().enabled = true;
        areYouSure.GetComponent<Button>().enabled = true;
        areYouSure_text.enabled = true;
        YesButton.GetComponent<Image>().enabled = true;
        YesButton.GetComponent<Button>().enabled = true;
        yes_text.enabled = true;
        NoButton.GetComponent<Image>().enabled = true;
        NoButton.GetComponent<Button>().enabled = true;
        no_text.enabled = true;
        disableEscapeMenu();
    }

    public void disableAreYouSure()
    {
        enableEscapeMenu();
        areYouSure.GetComponent<Image>().enabled = false;
        areYouSure.GetComponent<Button>().enabled = false;
        areYouSure_text.enabled = false;
        YesButton.GetComponent<Image>().enabled = false;
        YesButton.GetComponent<Button>().enabled = false;
        yes_text.enabled = false;
        NoButton.GetComponent<Image>().enabled = false;
        NoButton.GetComponent<Button>().enabled = false;
        no_text.enabled = false;
    }

    public void enableShop()
    {
        disableEscapeMenu();
        foreach (GameObject button in shopButtons)
        {
            button.GetComponent<Image>().enabled = true;
            button.GetComponent<Button>().enabled = true;
        }
        foreach (Text t in shopTexts)
        {
            t.enabled = true;
        }
        foreach (Image i in shopImages)
        {
            i.enabled = true;
        }

        if (purchasedGreen) // green
        {
            greenX.enabled = true;
        }
        else
        {
            greenX.enabled = false;
        }
        if (purchasedRed) // red
        {
            redX.enabled = true;
        }
        else
        {
            redX.enabled = false;
        }
        if (purchasedPurple) // purple
        {
            purpleX.enabled = true;
        }
        else
        {
            purpleX.enabled = false;
        }
        if (purchasedWhite) // white
        {
            whiteX.enabled = true;
        }
        else
        {
            whiteX.enabled = false;
        }

        if (speedCount == 4) // white
        {
            speedX.enabled = true;
        }
        else
        {
            speedX.enabled = false;
        }
        if (jumpCount == 4) // white
        {
            jumpX.enabled = true;
        }
        else
        {
            jumpX.enabled = false;
        }
        if (aeroCount == 4) // white
        {
            aeroX.enabled = true;
        }
        else
        {
            aeroX.enabled = false;
        }
    }

    public void disableShop()
    {
        enableEscapeMenu();
        foreach (GameObject button in shopButtons)
        {
            button.GetComponent<Image>().enabled = false;
            button.GetComponent<Button>().enabled = false;
        }
        foreach (Text t in shopTexts)
        {
            t.enabled = false;
        }
        foreach (Image i in shopImages)
        {
            i.enabled = false;
        }
    }

    public void updateChestSword()
    {
        Chest = GameObject.Find("Chest");
        Sword = GameObject.Find("Sword");
    }

    public void updateChest(GameObject obj)
    {
        Chest = obj;
    }

    public void updateSword(GameObject obj)
    {
        Sword = obj;
    }

    public void RecolorKnight()
    {
        if(isGreen)
        {
            Chest.GetComponent<Renderer>().material = Green;
            Sword.GetComponent<Renderer>().material = Green;
        }

        if(isRed)
        {
            Chest.GetComponent<Renderer>().material = Red;
            Sword.GetComponent<Renderer>().material = Red;
        }

        if(isPurple)
        {
            Chest.GetComponent<Renderer>().material = Purple;
            Sword.GetComponent<Renderer>().material = Purple;
        }

        if(isWhite)
        {
            Chest.GetComponent<Renderer>().material = White;
            Sword.GetComponent<Renderer>().material = White;
        }
    }

    public void Win()
    {
        Debug.Log("You win!");
        winText.enabled = true;
        winButton.GetComponent<Image>().enabled = true;
        deathStats.enabled = true;
        deathStats.text = "Total Deaths: " + deathCount;
        meterStats.enabled = true;
        meterStats.text = "Total Meters Traveled: " + metersWalked;
    }

    internal void Quit()
    {
        Application.Quit();
    }
}
