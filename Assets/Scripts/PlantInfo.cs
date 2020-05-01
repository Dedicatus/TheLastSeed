using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantInfo : MonoBehaviour
{
    private GameController myGameController;

    [SerializeField] private Text stateText;
    [SerializeField] private Text estimationText;
    [SerializeField] private Image vitalityBar;

    [SerializeField] private Plant myPlant;
    private Plant.PlantState myState;


    // Start is called before the first frame update
    void Awake()
    {
        myGameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        //myPlant = GameObject.FindWithTag("Plant").GetComponent<Plant>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myGameController == null) { myGameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();  }
        //if (myPlant == null) { myPlant = GameObject.FindWithTag("Plant").GetComponent<Plant>(); }
        if (myGameController.timePassing)
        {
            updateStateInfo();
            updateVitalityBar();
            estimationText.text = myPlant.getEstimationText();
        }
    }

    private void updateVitalityBar()
    {
        if (myPlant.getCurState() == Plant.PlantState.Growing)
        {
            vitalityBar.color = new Color(145.0f/255.0f, 199.0f / 255.0f, 100.0f / 255.0f);
        }
        else
        {
            vitalityBar.color = new Color(183.0f / 255.0f, 113.0f / 255.0f, 146.0f / 255.0f);
        }
        //Debug.Log("curH:" + myPlant.getCurHealth() + " LastMax" +  myPlant.getLastStageMaxHealth() + " CurMax" + myPlant.getCurMaxHealth());
        vitalityBar.fillAmount = (myPlant.getCurHealth() - myPlant.getLastStageMaxHealth()) / (myPlant.getCurMaxHealth() - myPlant.getLastStageMaxHealth());
    }

    private void updateStateInfo()
    {
        switch (myPlant.getCurState())
        {
            case Plant.PlantState.Growing:
                stateText.text = "Growing";
                break;
            case Plant.PlantState.Corrupting:
                stateText.text = "Corrupting";
                break;
            case Plant.PlantState.Stifling:
                stateText.text = "Stifling";
                break;
            case Plant.PlantState.OverHeating:
                stateText.text = "OverHeating";
                break;
            case Plant.PlantState.Freezing:
                stateText.text = "Freezing";
                break;
            default:
                break;
        }

    }
}
