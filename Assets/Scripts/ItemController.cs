using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    public enum items {Sprinkler, Lamp, Cover, Artificialsun}
    [Header("GameObjects")]
    public GameObject sprinkler;
    public GameObject lamp;
    public GameObject cover;
    public GameObject artificialsun;
    [SerializeField] public Image curItemUI;
    [SerializeField] public Image itemInUse;
    
    [Header("Controller")]
    [SerializeField] private Plant plant;
    [SerializeField] private WeatherController myWeatherController;
    [SerializeField] private GameController myGameController;
    

    public items lastUsedItem;
    public items curItem;

    public bool slotIsEmpty;

    public Dictionary<string, ItemController.items> itemPairs;



    // Start is called before the first frame update
    void Awake()
    {
        itemPairs = new Dictionary<string, ItemController.items>();
        myGameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();

        itemPairs.Add("Cooling", items.Sprinkler);
        itemPairs.Add("Lamp", items.Lamp);
        itemPairs.Add("Warming", items.Artificialsun);
        itemPairs.Add("Cover", items.Cover);

        slotIsEmpty = true;
        myWeatherController = GameObject.FindWithTag("GameController").transform.parent.Find("WeatherController").GetComponent<WeatherController>();

    }

    // Update is called once per frame
    void Update()
    {
        //CHECK CLICKS 
        if (Input.GetMouseButtonDown(0))
        {

            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mHit;
            if (Physics.Raycast(mRay, out mHit))
            {
                //Debug.Log("1232132");

                switch (mHit.collider.gameObject.name)
                {
                    case "SprinklerinHome":
                        break;
                    case "PurifierinHome":
                     
                        break;
                    case "CoverinHome":
                      
                        break;
                    case "PipeinHome":
                       
                        break;
                    case "ArtificialsuninHome":
                      
                        break;

                }
            }

        }
    }

    public void ClickCurItem( ) {
        if (myGameController.getCurScene() == GameController.GameScene.PlantLand)
        {
            if (!slotIsEmpty)
            {
                itemInUse.sprite = curItemUI.sprite;
                plant.UseCurItems(curItem);
                curItemUI.sprite = null;
                slotIsEmpty = true;
                lastUsedItem = curItem;
                myWeatherController.usedItemLatsPhase = true;

            }
        }
       
    }

    public void changeItem(string name) {
        curItem = itemPairs[name];
    }



}
