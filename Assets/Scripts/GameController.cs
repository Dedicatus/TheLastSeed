using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum GameScene {SpaceShip, PlantLand };
    private GameScene curScene;

    [Header("Controller")]
    [SerializeField] private OxygenController myOxygenController;
    [SerializeField] private UIController myUIController;
    [SerializeField] private Plant myPlant;
    [SerializeField] private WeatherController myWeatherController;


    [Header("Scene")]
    [SerializeField] private GameObject spaceShip;
    [SerializeField] private GameObject plantLand;  

    [Header("Time")]
    [SerializeField] private float secPerQuarter = 15.0f; //Seconds per 15 mins in game
    [SerializeField] private int dayStartHour = 7;
    [SerializeField] private int dayEndHour = 23;
    [SerializeField] private int curDay;

    [Header("Oxygen")]
    [SerializeField] private float dailyOxygenSupply = 200.0f;

    [Header("Debug")]
    public bool timePassing;
    [SerializeField] private float timer;
    [SerializeField] private int curHour;
    [SerializeField] private int curMin;
    

    // Start is called before the first frame update
    void Start()
    {
        //initialize variables
        changeScene(GameScene.SpaceShip);
        timer = 0;
        curHour = dayStartHour;
        myWeatherController.changeWeather();
        curMin = 0;
        curDay = 1;
    }

    // Update is called once per frame
    void Update()
    {
        timerHandler();
    }

    private void timerHandler()
    {
        if (timePassing)
        {
            timer += Time.deltaTime;
            if (timer >= secPerQuarter)
            {

                timer -= secPerQuarter;
                curMin += 15;
                if (curMin == 60)
                {
                    curMin = 0;
                    curHour += 1;

                    if (curHour == dayEndHour)
                    {
                        changeScene(GameScene.SpaceShip);
                        dayPass();
                    }
                    else if (curHour == (dayStartHour + dayEndHour) / 2) { myWeatherController.changeWeather(); }
                }
                myUIController.updateTimeText();
            }
        }
    }
    private void dayPass()
    {
        curHour = dayStartHour;
        curMin = 0;
        ++curDay;
        myUIController.updateDayText();
        myOxygenController.addOxygen(dailyOxygenSupply);
        myPlant.dayPassed();
        myWeatherController.changeWeather();
    }

    public void changeScene(GameScene scene)
    {
        switch (scene)
        {
            case GameScene.SpaceShip:
                myUIController.updateUI((int)GameScene.SpaceShip);
                myOxygenController.oxygenConsuming = false;
                spaceShip.SetActive(true);
                plantLand.SetActive(false);
                curScene = GameScene.SpaceShip;
                break;
            case GameScene.PlantLand:
                myUIController.updateUI((int)GameScene.PlantLand);
                myOxygenController.oxygenConsuming = true;
                spaceShip.SetActive(false);
                plantLand.SetActive(true);
                curScene = GameScene.PlantLand;

                break;
            default:
                break;
        }
    }

    public int getCurDay()
    {
        return curDay;
    }

    public int getCurHour()
    {
        return curHour;
    }

    public int getCurMin()
    {
        return curMin;
    }

    public float getSecPerQuarter()
    {
        return secPerQuarter;
    }

    public GameScene getCurScene() {
        return curScene;
    }
}
