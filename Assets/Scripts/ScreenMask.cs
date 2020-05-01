using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMask : MonoBehaviour
{
    private GameController myGameController;

    private void OnEnable()
    {
        myGameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    public void selfDisable()
    {
        myGameController.canChangeScene = true;
        gameObject.SetActive(false);
    }
}
