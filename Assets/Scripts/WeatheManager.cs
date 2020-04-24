using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeatheManager : MonoBehaviour
{
    public enum weatherList { AcidRain, SandStorm, HighTemp, Cold, Normal }

    //DEFINE VARIABLES
    [SerializeField] float lastTime;
    private float lastTimer;
    [SerializeField] float normalTime;
    private float normalTimer;
    public weatherList curWeather;
    [SerializeField] bool inAccident;
    [SerializeField] Text weatherMassage;

    [Header("Controllers")]
    [SerializeField] private GameController GC;
    [SerializeField] private ItemsManager IM;
    [SerializeField] private Plant plant;

    public bool isChanged;
    // Start is called before the first frame update
    void Start()
    {
        isChanged = false;
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

        if (GC.getCurHour() == 7 || GC.getCurHour() == 15)
        {
            if (isChanged == false)
            {
                ChangeWeather();
                isChanged = true;
            }
        }

      
        
    }

 

    //GENERATE NEXT WEATHER AND INVOKE FUNCTIONS
    void ChangeWeather() {

        curWeather = (weatherList)Random.Range(0, System.Enum.GetValues(typeof(weatherList)).Length);
        Debug.Log(curWeather);
        weatherMassage.text = $"Current weather is {curWeather.ToString()}";

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
                BackNormal();
                break;
            case weatherList.SandStorm:
                fooSandStorm();
                break;
        }
    }

    // FUNCTIONS HANDLING DIFFERENT WEATHER CHANGE
    void BackNormal() {
        return;
    }

    void fooAcidRain() {
        plant.addHealth(-10);
        IM.purifier.SetActive(false);
        IM.cover.SetActive(false);


    }

    void fooSandStorm() {
        plant.addHealth(-10);

        IM.pipe.SetActive(false);

    }

    void fooHighTemp() {
        plant.addHealth(-10);

        IM.sprinkler.SetActive(false);

    }

    void fooCold() {
        plant.addHealth(-10);

        IM.artificialsun.SetActive(false);
    }

    public weatherList GetCurWeather() {
        return curWeather;
    }
}

