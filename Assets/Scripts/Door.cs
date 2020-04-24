using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameController.GameScene targetScene = GameController.GameScene.SpaceShip;
    private GameController myGameController;
    public GameObject inventoryMenu;

    // Start is called before the first frame update
    void Start()
    {
        myGameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeSceneFun(){
         myGameController.changeScene(targetScene);
    }
    private void OnMouseDown()
    {
        myGameController.changeScene(targetScene);
    }

    public void showInventory(){
        inventoryMenu.SetActive(true);
    }

    public void closeInventory(){
        inventoryMenu.SetActive(false);
    }
}
