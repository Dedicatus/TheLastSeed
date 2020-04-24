using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameController.GameScene targetScene = GameController.GameScene.SpaceShip;
    //[SerializeField] private GameObject plantInfoMenu;
    private GameController myGameController;

    // Start is called before the first frame update
    void Start()
    {
        myGameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        myGameController.changeScene(targetScene);
        /*
        if(targetScene == GameController.GameScene.PlantLand){
            plantInfoMenu.SetActive(true);
        } else {
            plantInfoMenu.SetActive(false);
        }
        */
    }

}
