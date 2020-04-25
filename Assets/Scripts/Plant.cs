using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public enum PlantStatus { Normal, A, B, C, D }  // ABCD suppose to be changed base on design later on 

    private WeatherController myWeatherController;

    [Header("Stage")]
    [SerializeField] private Sprite[] plantSprites;
    [SerializeField] private float[] healthToStages;

    [Header("Status")]
    [SerializeField] private float lv1DebuffChance = 0.2f;
    [SerializeField] private float lv2DebuffChance = 0.4f;
    [SerializeField] private float lv3DebuffChance = 0.6f;

    [Header("Time")]
    [SerializeField] private int stageDayGap = 3;

    [Header("Health")]
    [SerializeField] private float initHealth = 100f;
    [SerializeField] private float HealthRecover = 1f;

    [Header("Debug")]
    [SerializeField] private PlantStatus myStatus = PlantStatus.Normal;
    [SerializeField] private float curDamage;
    [SerializeField] private int maxStage;
    [SerializeField] private int curStage;
    [SerializeField] private int dayUntilNextStage = 0;
    [SerializeField] private float curHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        myWeatherController = GameObject.FindWithTag("GameController").transform.parent.Find("WeatherController").GetComponent<WeatherController>();
        curStage = 0;
        maxStage = plantSprites.Length;
        gameObject.GetComponent<SpriteRenderer>().sprite = plantSprites[curStage];
        curHealth = initHealth;
        dayUntilNextStage = stageDayGap;
    }

    // Update is called once per frame
    void Update()
    {
        if (myStatus != PlantStatus.Normal)
        {
            curHealth -= curDamage * Time.deltaTime;
        }
        else
        {
            curHealth += HealthRecover * Time.deltaTime;
            if (curHealth >= healthToStages[curStage])
            {
                ++curStage;
            }
        }
    }

    private void nextStage()
    {
        if (curStage + 1 <= maxStage)
        {
            ++curStage;
            gameObject.GetComponent<SpriteRenderer>().sprite = plantSprites[curStage];
        }
        else
        {
            Debug.LogError("Plant max stage reached!");
        }
    }

    private void addHealth(int n)
    {
        if (curStage >= maxStage)
        {
            curHealth = healthToStages[healthToStages.Length - 1];
        }
        else
        {
            curHealth += n;
            if (curHealth <= 0)
            {
                //GameOver
            }
        }
    }

    public void dayPassed()
    {
       
    }

    public void applyWeatherCondition()
    {
        float debuffChance = 0f;
        switch (myWeatherController.getWeatherLevel())
        {
            case 1:
                debuffChance = lv1DebuffChance;
                break;
            case 2:
                debuffChance = lv2DebuffChance;
                break;
            case 3:
                debuffChance = lv3DebuffChance;
                break;
            default:
                break;
        }
        if (Random.Range(0.0f, 1.0f) <= debuffChance)
        {
            switch (myWeatherController.GetCurWeather())
            {
                case WeatherController.weatherList.Normal:
                    //Do something
                    break;
                case WeatherController.weatherList.AcidRain:
                    myStatus = PlantStatus.A;
                    break;
                case WeatherController.weatherList.SandStorm:
                    myStatus = PlantStatus.B;
                    break;
                case WeatherController.weatherList.HighTemp:
                    myStatus = PlantStatus.C;
                    break;
                case WeatherController.weatherList.Cold:
                    myStatus = PlantStatus.D;
                    break;
                default:
                    break;
            }
            curDamage = myWeatherController.getCurDamage();
        }   

    }

    
    public int getDayUntilNextStage()
    {
        return dayUntilNextStage;
    }

    public float getCurHealth()
    {
        return curHealth;
    }

    //UI
    public float getCurMaxHealth()
    {
        return healthToStages[curStage];
    }
}
