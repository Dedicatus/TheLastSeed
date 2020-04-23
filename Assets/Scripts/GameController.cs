using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum GameScene {SpaceShip, PlantLand };
    private GameScene myScene;

    [Header("Scenes")]
    [SerializeField] private GameObject spaceShip;
    [SerializeField] private GameObject plantLand;

    [SerializeField] private OxygenContoller myOxygenContoller;

    // Start is called before the first frame update
    void Start()
    {
        changeScene(GameScene.SpaceShip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeScene(GameScene scene)
    {
        switch (scene)
        {
            case GameScene.SpaceShip:
                myOxygenContoller.oxygenConsuming = false;
                spaceShip.SetActive(true);
                plantLand.SetActive(false);
                break;
            case GameScene.PlantLand:
                myOxygenContoller.oxygenConsuming = true;
                spaceShip.SetActive(false);
                plantLand.SetActive(true);
                break;
            default:
                break;
        }
    }
}
