﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    private GameController myGameController;
    private WeatherController myWeatherController;

    [Header("System")]
    [SerializeField] private GameObject openingVideo;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject succeedMenu;
    [SerializeField] private GameObject failedMenu;
    [SerializeField] private GameObject screenFadeMask;
    [SerializeField] private Animator screenFadeAnimator;

    [Header("Clock")]
    [SerializeField] private TextMeshProUGUI hour1Text;
    [SerializeField] private TextMeshProUGUI hour2Text;
    [SerializeField] private TextMeshProUGUI min1Text;
    [SerializeField] private TextMeshProUGUI min2Text;

    [Header("Date")]
    [SerializeField] private TextMeshProUGUI dateText;

    [Header("Oxygen")]
    [SerializeField] private Animator oxygenBarAnimator;

    [Header("Weather")]
    [SerializeField] private TextMeshProUGUI weatherText;
    [SerializeField] private TextMeshProUGUI intensityText;
    [SerializeField] private UnityEngine.UI.Image weatherImg;
    [SerializeField] private UnityEngine.UI.Image weatherBackgroundImg;
    [SerializeField] private Animator weatherChangeAnimator;

    [Header("Weather Sprites")]
    [SerializeField] private Sprite acidRainSprite;
    [SerializeField] private Sprite sandStormSprite;
    [SerializeField] private Sprite highTempSprite;
    [SerializeField] private Sprite coldSprite;
    [SerializeField] private Sprite normalSprite;

    [Header("Plant Info")]
    [SerializeField] private GameObject plantInfoMenu;

    // Start is called before the first frame update
    void Awake()
    {
        myGameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        myWeatherController = GameObject.FindWithTag("GameController").transform.parent.Find("WeatherController").GetComponent<WeatherController>();
        mainMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void updateDayText()
    {
        dateText.text = myGameController.getCurDay().ToString();
    }

    public void updateTimeText()
    {
        int hour = myGameController.getCurHour();
        int min = myGameController.getCurMin();

        if (hour >= 0 && hour < 10)
        {
            hour1Text.text = "0";
            hour2Text.text = hour.ToString();
        }
        else
        {
            //string[] s = hour.ToString().Split();
            hour1Text.text = (hour / 10).ToString();
            hour2Text.text = (hour % 10).ToString();
        }

        if (min >= 0 && min < 10)
        {
            min1Text.text = "0";
            min2Text.text = min.ToString();
        }

        else
        {
            // string[] s = min.ToString().Split();
            min1Text.text = (min / 10).ToString();
            min2Text.text = (min % 10).ToString();
        }

        // timeText.text = myGameController.getCurHour().ToString() + ":" + myGameController.getCurMin().ToString("00");
    }

    public void updateUI(int targetScene)
    {
        if (targetScene == (int)GameController.GameScene.PlantLand)
        {
            plantInfoMenu.SetActive(true);
            oxygenBarAnimator.SetTrigger("Exit");
        }
        else
        {
            plantInfoMenu.SetActive(false);
            oxygenBarAnimator.SetTrigger("Enter");
        }
    }

    public void updateWeather(WeatherController.weatherList weather)
    {
        switch ((int)weather)
        {
            case 0:
                weatherText.text = "Acid Rain";
                weatherImg.GetComponent<UnityEngine.UI.Image>().sprite = acidRainSprite;
                //weatherImg.GetComponent<UnityEngine.UI.Image>().color = Color.green;
                //weatherBackgroundImg.GetComponent<UnityEngine.UI.Image>().color = Color.green;
                break;
            case 1:
                weatherText.text = "Sand Storm";
                weatherImg.GetComponent<UnityEngine.UI.Image>().sprite = sandStormSprite;
                //weatherImg.GetComponent<UnityEngine.UI.Image>().color = Color.cyan;
                //weatherBackgroundImg.GetComponent<UnityEngine.UI.Image>().color = Color.cyan;
                break;
            case 2:
                weatherText.text = "High Temp";
                weatherImg.GetComponent<UnityEngine.UI.Image>().sprite = highTempSprite;
                //weatherImg.GetComponent<UnityEngine.UI.Image>().color = Color.red;
                //weatherBackgroundImg.GetComponent<UnityEngine.UI.Image>().color = Color.red;
                break;
            case 3:
                weatherText.text = "Cold";
                weatherImg.GetComponent<UnityEngine.UI.Image>().sprite = coldSprite;
                //weatherImg.GetComponent<UnityEngine.UI.Image>().color = Color.blue;
                //weatherBackgroundImg.GetComponent<UnityEngine.UI.Image>().color = Color.blue;
                break;
            case 4:
                weatherText.text = "Normal";
                weatherImg.GetComponent<UnityEngine.UI.Image>().sprite = normalSprite;
                //weatherImg.GetComponent<UnityEngine.UI.Image>().color = Color.white;
                //weatherBackgroundImg.GetComponent<UnityEngine.UI.Image>().color = Color.white;
                break;
            default:
                break;
        }

        intensityText.text = "";
        for (int i = 0; i < myWeatherController.getWeatherLevel(); ++i)
        {
            intensityText.text += "I";
        }

        weatherChangeAnimator.SetTrigger("Play");
    }

    public void gameStart()
    {
        if (myGameController.hasOpening) { openingVideo.SetActive(true); }
        mainMenu.SetActive(false);
        updateTimeText();
    }

    public void showEndScreen(GameController.GameState state)
    {
        if (state == GameController.GameState.Succeed)
        {
            succeedMenu.SetActive(true);
        }
        else if (state == GameController.GameState.Failed)
        {
            failedMenu.SetActive(true);
        }
        else
        {
            Debug.LogError("Game State Error");
        }
    }

    public void resetUI()
    {
        mainMenu.SetActive(true);
        succeedMenu.SetActive(false);
        failedMenu.SetActive(false);
    }

    public void fadeScreen()
    {
        screenFadeMask.SetActive(true);
    }
}
