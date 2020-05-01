using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public enum PlantState { Growing, Corrupting, Stifling, OverHeating, Freezing };

    [SerializeField] private WeatherController myWeatherController;
    [SerializeField] private GameController myGameController;
    [SerializeField] private PlantDisplay myPlantDisplay;

    [Header("Stage")]
    [SerializeField] private float[] healthToStages;
    [SerializeField] private float[] oxygenSupplyOfStages;

    [Header("Status")]
    [SerializeField] private float lv1DebuffChance = 0.2f;
    [SerializeField] private float lv2DebuffChance = 0.4f;
    [SerializeField] private float lv3DebuffChance = 0.6f;

    [Header("Health")]
    [SerializeField] private float initHealth = 100f;
    [SerializeField] private float HealthRecover = 1f;

    [Header("Debug")]
    [SerializeField] private PlantState curState = PlantState.Growing;
    [SerializeField] private float curDamage;
    [SerializeField] private int maxStage = 4;
    [SerializeField] private int curStage;
    [SerializeField] private int dayUntilNextStage = 0;
    [SerializeField] private float curHealth;
    
    // Start is called before the first frame update
    void Awake()
    {
        myWeatherController = GameObject.FindWithTag("GameController").transform.parent.Find("WeatherController").GetComponent<WeatherController>();
        maxStage = myPlantDisplay.getMaxStage();
        initialization();
    }

    public void initialization()
    {
        curStage = 0;
        curHealth = initHealth;
        curState = PlantState.Growing;
        myPlantDisplay.initialization();
    }

    // Update is called once per frame
    void Update()
    {
        if (myGameController.timePassing)
        {
            if (curState != PlantState.Growing)
            {
                curHealth -= curDamage * Time.deltaTime;
                if (curHealth <= 0)
                {
                    myGameController.gameFailed();
                }
            }
            else
            {
                curHealth += HealthRecover * Time.deltaTime;
                if (curHealth >= healthToStages[curStage])
                {
                    ++curStage;
                    myPlantDisplay.nextStage();

                    if (curStage >= healthToStages.Length)
                    {
                        myGameController.gameSucceed();
                        Debug.Log("Succeed");
                    }
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
    /*
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
                myGameController.gameFailed();
            }
        }
    }
    */
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
                    curState = PlantState.Growing;
                    break;
                case WeatherController.weatherList.AcidRain:
                    curState = PlantState.Corrupting;
                    break;
                case WeatherController.weatherList.SandStorm:
                    curState = PlantState.Stifling;
                    break;
                case WeatherController.weatherList.HighTemp:
                    curState = PlantState.OverHeating;
                    break;
                case WeatherController.weatherList.Cold:
                    curState = PlantState.Freezing;
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

    public float getCurOxygenSupply()
    {
        return oxygenSupplyOfStages[curStage];
    }

    //UI
    public float getCurMaxHealth()
    {
        if (curStage <= healthToStages.Length - 1)
        {
            return healthToStages[curStage];
        }
        else
        {
            return healthToStages[healthToStages.Length - 1];
        }
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
        if (curState == PlantState.Growing)
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

    public PlantState getCurState()
    {
        return curState;
    }

    public void UseCurItems(ItemController.items curItem) {
        if (myWeatherController.curWeather == myWeatherController.weatherPairs[curItem])
        {
            curState = PlantState.Growing;
        }
    }
   
}
