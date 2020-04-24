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
    public weatherList curWeather;
    public weatherList comingWeather;
    [SerializeField] bool inAccident;
    [SerializeField] Text weatherMassage;
    [SerializeField] Text comingWeatherMessage;

    private GameController myGameController;
    private ItemController myItemController;
    [SerializeField] private Plant plant;

    // Start is called before the first frame update
    void Start()
    {
        myGameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        myItemController = GameObject.FindWithTag("GameController").transform.parent.Find("ItemController").GetComponent<ItemController>();
        comingWeather = weatherList.Normal;
        weatherMassage.text = "";
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
        curWeather = comingWeather;
        comingWeather = (weatherList)Random.Range(0, System.Enum.GetValues(typeof(weatherList)).Length);
        //Debug.Log(curWeather);
        weatherMassage.text = curWeather.ToString();
        comingWeatherMessage.text = comingWeather.ToString();

        //Debug.Log(curWeather.ToString());
        switch (curWeather)
        {
            case weatherList.AcidRain:
                fooAcidRain();
                break;
            case weatherList.Cold:
                fooCold();
                break;
            case weatherList.HighTemp:
                fooHighTemp();
                break;
            case weatherList.Normal:
                backNormal();
                break;
            case weatherList.SandStorm:
                fooSandStorm();
                break;
        }
    }

    // FUNCTIONS HANDLING DIFFERENT WEATHER CHANGE
    void backNormal()
    {
        return;
    }

    void fooAcidRain()
    {
        plant.addHealth(-10);
       
        myItemController.cover.SetActive(false);


    }

    void fooSandStorm()
    {
        plant.addHealth(-10);

        myItemController.lamp.SetActive(false);

    }

    void fooHighTemp()
    {
        plant.addHealth(-10);

        myItemController.sprinkler.SetActive(false);

    }

    void fooCold()
    {
        plant.addHealth(-10);

        myItemController.artificialsun.SetActive(false);
    }

    public weatherList GetCurWeather()
    {
        return curWeather;
    }
}

