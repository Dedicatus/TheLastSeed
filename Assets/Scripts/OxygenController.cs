﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenController : MonoBehaviour
{
    private GameController myGameController;
    private AudioController myAudioController;


    [Header("Variables")]
    [SerializeField] private float maxOxygen = 500f;
    [SerializeField] private float initialOxygen = 300f;
    //Consumption per second
    [SerializeField] private float oxygenConsumption = 10f;

    [Header("Debug")]
    [SerializeField] private float curOxygen;
    public bool oxygenConsuming = false;
    // Start is called before the first frame update
    void Awake()
    {
        myGameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        myAudioController = GameObject.FindWithTag("GameController").transform.parent.Find("AudioController").GetComponent<AudioController>();

        curOxygen = initialOxygen;
    }

    // Update is called once per frame
    void Update()
    {
        if (oxygenConsuming && myGameController.timePassing)
        {
            if (curOxygen > 0)
            {
                curOxygen -= oxygenConsumption * Time.deltaTime;
                if (curOxygen < maxOxygen * 0.6f && curOxygen >= maxOxygen * 0.3f) {
                    myAudioController.SetBreathSound(1);
                }
                if (curOxygen < maxOxygen * 0.3f) {
                    myAudioController.SetBreathSound(0);
                }
            }
            else
            {
                //Return to the spaceship when out of oxygen
                curOxygen = 0;
                myGameController.changeScene(GameController.GameScene.SpaceShip);
            }
        }
    }

    public void addOxygen(float n)
    {
        if (curOxygen + n > maxOxygen)
        {
            curOxygen = maxOxygen;
        }
        else
        {
            curOxygen += n;
            if (curOxygen >= 150f && curOxygen < 300) {
                myAudioController.SetBreathSound(1);
            }
            if (curOxygen >= 300) {
                myAudioController.SetBreathSound(2);
            }
        }
    }

    public float getCurOxygen()
    {
        return curOxygen;
    }

    public float getMaxOxygen()
    {
        return maxOxygen;
    }
}
