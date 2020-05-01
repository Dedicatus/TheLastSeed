using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum GameState { MainMenu, InGame, Succeed, Failed }
    [SerializeField] private GameState curState;

    public enum GameScene { SpaceShip, PlantLand };
    private GameScene curScene;

    [Header("System")]
    public bool hasOpening;
    public bool hasTutorial;

    [Header("Controller")]
    [SerializeField] private OxygenController myOxygenController;
    [SerializeField] private UIController myUIController;
    [SerializeField] private Plant myPlantController;
    [SerializeField] private WeatherController myWeatherController;
    [SerializeField] private AudioController myAudioController;


    [Header("Scene")]
    [SerializeField] private GameObject spaceShip;
    [SerializeField] private GameObject plantLand;

    [Header("Time")]
    [SerializeField] private float secPerQuarter = 15.0f; //Seconds per 15 mins in game
    [SerializeField] private int dayStartHour = 7;
    [SerializeField] private int dayEndHour = 23;
    [SerializeField] private int curDay;

    [Header("Debug")]
    public bool timePassing;
    [SerializeField] private float timer;
    [SerializeField] private int curHour;
    [SerializeField] private int curMin;


    // Start is called before the first frame update
    void Start()
    {
        //initialize variables
        curState = GameState.MainMenu;
        initialization();
    }

    private void initialization()
    {
        myOxygenController.oxygenConsuming = false;
        spaceShip.SetActive(true);
        plantLand.SetActive(false);
        curScene = GameScene.SpaceShip;
        timer = 0;
        curHour = dayStartHour;
        myWeatherController.changeWeather();
        curMin = 0;
        curDay = 1;
        myPlantController.initialization();
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

    private void inputHandler()
    {
        
    }

    private void dayPass()
    {
        curHour = dayStartHour;
        curMin = 0;
        ++curDay;
        myUIController.updateDayText();
        myOxygenController.addOxygen(myPlantController.getCurOxygenSupply());
        myPlantController.dayPassed();
        myWeatherController.changeWeather();
    }

    public void changeScene(GameScene scene)
    {
        myAudioController.PlayOpenDoorSound();
        switch (scene)
        {
            case GameScene.SpaceShip:
                myAudioController.StopBreathSound();
                myUIController.updateUI((int)GameScene.SpaceShip);
                myOxygenController.oxygenConsuming = false;
                spaceShip.SetActive(true);
                plantLand.SetActive(false);
                curScene = GameScene.SpaceShip;
                break;
            case GameScene.PlantLand:
                myAudioController.PlayBreathSound();
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

    public GameScene getCurScene()
    {
        return curScene;
    }

    public void gameStart()
    {
        curState = GameState.InGame;
        initialization();
        myUIController.gameStart();
        if (!hasOpening && !hasTutorial) { timePassing = true; }
    }

    public void gameSucceed()
    {
        timePassing = false;
        curState = GameState.Succeed;
        myUIController.showEndScreen(curState);
    }

    public void gameFailed()
    {
        timePassing = false;
        curState = GameState.Failed;
        myUIController.showEndScreen(curState);
    }

    public void gameEnd()
    {
        curState = GameState.MainMenu;
        initialization();
    }
}
