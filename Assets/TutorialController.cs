using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject plantLand;
    [SerializeField] private GameObject spaceShip;
    [SerializeField] private GameObject[] steps;
    [SerializeField] private int spaceShipStartIndex = 4;
    [SerializeField] private int spaceShipEndIndex = 6;

    [Header("Debug")]
    [SerializeField] private int curStep;

    private GameController myGameController;

    public void startTutorial()
    {
        myGameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        tutorialPanel.SetActive(true);
        plantLand.SetActive(true);
        curStep = 0;
        steps[curStep].SetActive(true);
    }

    public void nextStep()
    {
        ++curStep;
        if (curStep >= steps.Length)
        {
            endTutorial();
        }
        else
        {
            if (curStep >= spaceShipStartIndex && curStep <= spaceShipEndIndex)
            {
                plantLand.SetActive(false);
                spaceShip.SetActive(true);
            }
            else
            {
                plantLand.SetActive(true);
                spaceShip.SetActive(false);
            }
            steps[curStep - 1].SetActive(false);
            steps[curStep].SetActive(true);
        }
    }

    private void endTutorial()
    {
        tutorialPanel.SetActive(false);
        myGameController.timePassing = true;
    }
}
