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

    [Header("Cooldown")]
    [SerializeField] private float nextStepCD = 0.5f;
    [SerializeField] private float nextStepTimer;

    [Header("Debug")]
    [SerializeField] private int curStep;

    private GameController myGameController;

    private void Update()
    {
        if (nextStepTimer > 0f)
        {
            nextStepTimer -= Time.deltaTime;
        }
        else
        {
            nextStepTimer = 0f;
        }
    }

    public void startTutorial()
    {
        myGameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        tutorialPanel.SetActive(true);
        plantLand.SetActive(true);
        spaceShip.SetActive(false);
        curStep = 0;
        nextStepTimer = 0f;
        steps[curStep].SetActive(true);
    }

    public void nextStep()
    {
        if (nextStepTimer > 0f) { return; }

        ++curStep;
        nextStepTimer = nextStepCD;
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
