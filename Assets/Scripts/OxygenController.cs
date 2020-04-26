using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenController : MonoBehaviour
{
    private GameController myGameController;

    [Header("Variables")]
    [SerializeField] private float maxOxygen = 500f;
    [SerializeField] private float initialOxygen = 300f;
    //Consumption per second
    [SerializeField] private float oxygenConsumption = 10f;

    [Header("Debug")]
    [SerializeField] private float curOxygen;
    public bool oxygenConsuming = false;
    // Start is called before the first frame update
    void Start()
    {
        myGameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        curOxygen = initialOxygen;
    }

    // Update is called once per frame
    void Update()
    {
        if (oxygenConsuming)
        {
            if (curOxygen > 0)
            {
                curOxygen -= oxygenConsumption * Time.deltaTime;
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
