using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum GameScene {SpaceShip, PlantLand };
    private GameScene myScene;

    [Header("Controller")]
    [SerializeField] private OxygenController myOxygenController;
    [SerializeField] private UIController myUIController;
    [SerializeField] private Plant myPlant;
    [SerializeField] private WeatheManager WM;


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
        curMin = 0;
        curDay = 1;
        timePassing = true;
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
                    WM.isChanged = false;
                    curMin = 0;
                    curHour += 1;

                    if (curHour == dayEndHour)
                    {
                        changeScene(GameScene.SpaceShip);
                        dayPass();
                    }
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
    }

    public void changeScene(GameScene scene)
    {
        switch (scene)
        {
            case GameScene.SpaceShip:
                myUIController.btnSpaceShip.gameObject.SetActive(false);
                myOxygenController.oxygenConsuming = false;
                spaceShip.SetActive(true);
                plantLand.SetActive(false);
                break;
            case GameScene.PlantLand:
                myUIController.btnSpaceShip.gameObject.SetActive(true);
                myOxygenController.oxygenConsuming = true;
                spaceShip.SetActive(false);
                plantLand.SetActive(true);
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
}
