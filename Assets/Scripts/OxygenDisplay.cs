using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenDisplay : MonoBehaviour
{
    private GameController myGameController;
    private OxygenController myOxygenController;
    private Image fillImage;

    // Start is called before the first frame update
    void Start()
    {
        myGameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        myOxygenController = GameObject.FindWithTag("GameController").transform.parent.Find("OxygenController").GetComponent<OxygenController>();
        fillImage = transform.GetChild(0).GetComponent<Image>();
        fillImage.fillAmount = myOxygenController.getCurOxygen() / myOxygenController.getMaxOxygen();
    }

    // Update is called once per frame
    void Update()
    {
        if (myGameController.getCurScene() == GameController.GameScene.PlantLand)
        {
            fillImage.fillAmount = myOxygenController.getCurOxygen() / myOxygenController.getMaxOxygen();
        }
    }
}
