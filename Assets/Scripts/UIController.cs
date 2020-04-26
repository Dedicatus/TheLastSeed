using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private GameController myGameController;

    [SerializeField] private UnityEngine.UI.Text dayText;
    [SerializeField] private UnityEngine.UI.Text timeText;
    [SerializeField] private GameObject plantInfoMenu;

    // Start is called before the first frame update
    void Start()
    {
        myGameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateDayText()
    {
        dayText.text = myGameController.getCurDay().ToString();
    }

    public void updateTimeText()
    {
        timeText.text = myGameController.getCurHour().ToString() + ":" + myGameController.getCurMin().ToString("00");
    }

    public void updateUI(int targetScene){
        if(targetScene == (int) GameController.GameScene.PlantLand){
            plantInfoMenu.SetActive(true);
        } else {
            plantInfoMenu.SetActive(false);
        }
    }

}
