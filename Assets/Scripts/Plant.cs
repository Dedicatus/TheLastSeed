using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public enum PlantStatus { Growing, Corrupting, Stifling, OverHeating, Freezing };

    [SerializeField] private WeatherController myWeatherController;
    [SerializeField] private GameController myGameController;
    [SerializeField] private PlantDisplay myPlantDisplay;

    [Header("Stage")]
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
    [SerializeField] private PlantStatus myStatus = PlantStatus.Growing;
    [SerializeField] private float curDamage;
    [SerializeField] private int maxStage = 4;
    [SerializeField] private int curStage;
    [SerializeField] private int dayUntilNextStage = 0;
    [SerializeField] private float curHealth;
    
    // Start is called before the first frame update
    void Awake()
    {
        myWeatherController = GameObject.FindWithTag("GameController").transform.parent.Find("WeatherController").GetComponent<WeatherController>();
        curStage = 0;
        maxStage = myPlantDisplay.getMaxStage();
        curHealth = initHealth;
        dayUntilNextStage = stageDayGap;
    }

    // Update is called once per frame
    void Update()
    {
        if (myGameController.timePassing)
        {
            if (myStatus != PlantStatus.Growing)
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
    }

    private void nextStage()
    {
        if (curStage + 1 <= maxStage)
        {
            ++curStage;
            myPlantDisplay.nextStage();
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
                    myStatus = PlantStatus.Growing;
                    break;
                case WeatherController.weatherList.AcidRain:
                    myStatus = PlantStatus.Corrupting;
                    break;
                case WeatherController.weatherList.SandStorm:
                    myStatus = PlantStatus.Stifling;
                    break;
                case WeatherController.weatherList.HighTemp:
                    myStatus = PlantStatus.OverHeating;
                    break;
                case WeatherController.weatherList.Cold:
                    myStatus = PlantStatus.Freezing;
                    break;
                default:
                    break;
            }
            curDamage = myWeatherController.getCurDamage();
        }   

    }

    public int getCurStage()
    {
        return curStage;
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

    public float getLastStageMaxHealth()
    {
        if (curStage >= 1)
        {
            return healthToStages[curStage - 1];
        }
        else
        {
            return 0f;
        }
        
    }

    public string getEstimationText()
    {
        string estimationString = "";
        int hour = 0;
        if (myStatus == PlantStatus.Growing)
        {
            hour = (int)Mathf.Round(((healthToStages[curStage] - curHealth) / HealthRecover) / (myGameController.getSecPerQuarter() * 4.0f));
            //Debug.Log(hour);
            estimationString = "Next Stage in " + hour.ToString() + " hour";
            if (hour >= 1) { estimationString += "s"; }
        }
        else
        {
            hour = (int)Mathf.Round(((curHealth - getLastStageMaxHealth()) / curDamage) / (myGameController.getSecPerQuarter() * 4.0f));
            //Debug.Log(hour);
            estimationString = "Withering in " + hour.ToString() + " hour";
            if (hour >= 1) { estimationString += "s"; }
        }
        return estimationString;
    }

    public PlantStatus getCurState()
    {
        return myStatus;
    }

    public void UseCurItems(ItemController.items curItem) {
        if (myWeatherController.curWeather == myWeatherController.weatherPairs[curItem])
        {
            myStatus = PlantStatus.Growing;
        }
    }
   
}
