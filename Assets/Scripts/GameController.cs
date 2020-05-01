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
    public bool canChangeScene;

    [Header("Controller")]
    [SerializeField] private OxygenController myOxygenController;
    [SerializeField] private UIController myUIController;
    [SerializeField] private Plant myPlantController;
    [SerializeField] private WeatherController myWeatherController;
    [SerializeField] private ItemController myItemController;
    [SerializeField] private AudioController myAudioController;
    [SerializeField] private TutorialController myTutorialController;


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
        canChangeScene = true;
        myPlantController.initialization();
        myWeatherController.Initialization();
        myItemController.Initialization();
        myOxygenController.oxygenConsuming = false;
        myWeatherController.changeWeather();
        spaceShip.SetActive(true);
        plantLand.SetActive(false);
        curScene = GameScene.SpaceShip;
        timer = 0;
        curHour = dayStartHour;
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

    private void inputHandler()
    {
        
    }

    private void dayPass()
    {
        myUIController.fadeScreen();
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
        if (!canChangeScene) { return; }
        myAudioController.PlayOpenDoorSound();
        myUIController.fadeScreen();
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
        canChangeScene = false;
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
        if (!hasOpening)
        {
            if (hasTutorial) { startTutorial(); }
            else { timePassing = true; }
        }
    }

    public void gameSucceed()
    {
        myUIController.fadeScreen();
        timePassing = false;
        curState = GameState.Succeed;
        myUIController.showEndScreen(curState);
    }

    public void gameFailed()
    {
        myUIController.fadeScreen();
        timePassing = false;
        curState = GameState.Failed;
        myUIController.showEndScreen(curState);
    }

    public void gameEnd()
    {
        curState = GameState.MainMenu;
        myUIController.resetUI();
        initialization();
    }

    public void startTutorial()
    {
        myTutorialController.startTutorial();
        hasTutorial = false;
    }
}
