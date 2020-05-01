using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeatherController : MonoBehaviour
{
    public enum weatherList { AcidRain, SandStorm, HighTemp, Cold, Normal }

    //DEFINE VARIABLES
    [SerializeField] float lastTime;
    private float lastTimer;
    [SerializeField] float normalTime;
    private float normalTimer;
   
    [SerializeField] bool inAccident;
    [SerializeField] Text weatherMassage;
    [SerializeField] Text comingWeatherMessage;

    private GameController myGameController;
    private ItemController myItemController;
    private UIController myUIController;
    private AudioController myAudioController;


    [SerializeField] private Plant plant;
    [SerializeField] private GameObject cover;

    [Header("AcidDamageValue")]
    [SerializeField] float AcidDamLv1 = 1f;
    [SerializeField] float AcidDamLv2 = 2f;
    [SerializeField] float AcidDamLv3 = 3f;

    [Header("SandDamageValue")]
    [SerializeField] float SandDamLv1 = 1f;
    [SerializeField] float SandDamLv2 = 2f;
    [SerializeField] float SandDamLv3 = 3f;

    [Header("HotDamageValue")]
    [SerializeField] float HotDamLv1 = 1f;
    [SerializeField] float HotDamLv2 = 2f;
    [SerializeField] float HotDamLv3 = 3f;

    [Header("ColdDamageValue")]
    [SerializeField] float ColdDamLv1 = 1f;
    [SerializeField] float ColdDamLv2 = 2f;
    [SerializeField] float ColdDamLv3 = 3f;

    [SerializeField] int curLevel;

    [Header("Debug")]
    public weatherList curWeather;
    public weatherList comingWeather;

    public Dictionary <ItemController.items, weatherList> weatherPairs;

    public bool usedItemLatsPhase;


    // Start is called before the first frame update
    void Awake()
    {
        usedItemLatsPhase = false;
        myGameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        myItemController = GameObject.FindWithTag("GameController").transform.parent.Find("ItemController").GetComponent<ItemController>();
        myUIController = GameObject.FindWithTag("GameController").transform.parent.Find("UIController").GetComponent<UIController>();
        myAudioController = GameObject.FindWithTag("GameController").transform.parent.Find("AudioController").GetComponent<AudioController>();

        comingWeather = weatherList.Normal;
        weatherMassage.text = "";

        //Initiate weatherpairs
        weatherPairs = new Dictionary<ItemController.items, weatherList>();
        weatherPairs.Add( ItemController.items.Cover,weatherList.AcidRain);
        weatherPairs.Add( ItemController.items.Artificialsun, weatherList.Cold);
        weatherPairs.Add( ItemController.items.Sprinkler, weatherList.HighTemp);
        weatherPairs.Add(ItemController.items.Lamp, weatherList.SandStorm);



        // IN CASE TO ADD MORE FEATURES 
        inAccident = false;
        lastTime = 15f;
        lastTimer = 0f;
        normalTime = 40f;
        normalTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 

    //GENERATE NEXT WEATHER AND INVOKE FUNCTIONS
    public void changeWeather()
    {
        myAudioController.PlayWeatherChangeSound();
        weatherList lastweather = curWeather;
        if (usedItemLatsPhase)
        {
            if (weatherPairs[myItemController.lastUsedItem] == curWeather)
            {
                do
                {
                    curWeather = (weatherList)Random.Range(0, System.Enum.GetValues(typeof(weatherList)).Length);

                } while (curWeather == lastweather);
            }

            else
            {
                curWeather = weatherPairs[myItemController.lastUsedItem];
            }
        }
        else { 
                    curWeather = (weatherList)Random.Range(0, System.Enum.GetValues(typeof(weatherList)).Length);

        }

        usedItemLatsPhase = false;
        myItemController.itemInUse.sprite = null;
        myItemController.itemInUse.enabled = false;

        //curWeather = (weatherList)Random.Range(0, System.Enum.GetValues(typeof(weatherList)).Length);
        float rateOfLevel = (float)Random.Range(0f, 1f);
        
        //GET A RANDOM LEVEL
            if (0f <= rateOfLevel && rateOfLevel < 0.33f) {
                curLevel = 1;
            }

            if (0.33f <= rateOfLevel && rateOfLevel < 0.66f)
            {
                curLevel = 2;
            }

            if (0.66f <= rateOfLevel && rateOfLevel < 1f)
            {
                curLevel = 3;
            }
        
        
        weatherMassage.text = curWeather.ToString();
        comingWeatherMessage.text = comingWeather.ToString();

  

        plant.applyWeatherCondition();
        myUIController.updateWeather(curWeather);
    }



    public weatherList GetCurWeather()
    {
        return curWeather;
    }

    public int getWeatherLevel()
    {
        return curLevel;
    }

    public float getCurDamage() {
        float curDamage = 0;
        switch (curWeather) {
            case weatherList.AcidRain:
                switch (curLevel) {
                    case 1:
                        curDamage = AcidDamLv1;
                        break;
                    case 2:
                        curDamage = AcidDamLv2;
                        break;
                    case 3:
                        curDamage = AcidDamLv3;
                        break;
                }
                break;
            case weatherList.SandStorm:
                switch (curLevel)
                {
                    case 1:
                        curDamage = SandDamLv1;
                        break;
                    case 2:
                        curDamage = SandDamLv2;
                        break;
                    case 3:
                        curDamage = SandDamLv3;
                        break;
                }
                break;
            case weatherList.HighTemp:
                switch (curLevel)
                {
                    case 1:
                        curDamage = HotDamLv1;
                        break;
                    case 2:
                        curDamage = HotDamLv2;
                        break;
                    case 3:
                        curDamage = HotDamLv3;
                        break;
                }
                break;
            case weatherList.Cold:
                switch (curLevel)
                {
                    case 1:
                        curDamage = ColdDamLv1;
                        break;
                    case 2:
                        curDamage = ColdDamLv2;
                        break;
                    case 3:
                        curDamage = ColdDamLv3;
                        break;
                }
                break;

        }
        return curDamage;
    }

    public void Initialization() { 
        usedItemLatsPhase = false;

    }
}

