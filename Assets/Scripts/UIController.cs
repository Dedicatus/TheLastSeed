using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private GameController myGameController;

    [SerializeField] private UnityEngine.UI.Text dayText;
    [SerializeField] private UnityEngine.UI.Text timeText;
    public UnityEngine.UI.Button btnSpaceShip;
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

}
