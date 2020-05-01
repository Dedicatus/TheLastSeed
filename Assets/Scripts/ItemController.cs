using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    public enum items { Sprinkler, Lamp, Cover, Artificialsun }
    [Header("GameObjects")]
    public GameObject sprinkler;
    public GameObject lamp;
    public GameObject cover;
    public GameObject artificialsun;
    [SerializeField] public Image curItemUI;
    [SerializeField] public Image itemInUse;
    [SerializeField] Sprite[] usedItemSprites;
    [Header("Controller")]
    [SerializeField] private Plant plant;
    [SerializeField] private WeatherController myWeatherController;
    [SerializeField] private GameController myGameController;
    [SerializeField] private AudioController myAudioController;



    public items lastUsedItem;
    public items curItem;

    private bool slotIsEmpty;

    public Dictionary<string, ItemController.items> itemPairs;
    public Dictionary<items, Sprite> usedItemPairs;

    GameObject[] itemInHome;



    // Start is called before the first frame update
    void Awake()
    {
        itemInHome = GameObject.FindGameObjectsWithTag("Item");
        itemPairs = new Dictionary<string, ItemController.items>();
        myGameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        usedItemPairs = new Dictionary<items, Sprite>();

        itemPairs.Add("Cooling", items.Sprinkler);
        itemPairs.Add("Lamp", items.Lamp);
        itemPairs.Add("Warming", items.Artificialsun);
        itemPairs.Add("Cover", items.Cover);

        usedItemPairs.Add(items.Sprinkler, usedItemSprites[0]);
        usedItemPairs.Add(items.Lamp, usedItemSprites[1]);
        usedItemPairs.Add(items.Artificialsun, usedItemSprites[2]);
        usedItemPairs.Add(items.Cover, usedItemSprites[3]);


        curItemUI.enabled = false;
        slotIsEmpty = true;
        myWeatherController = GameObject.FindWithTag("GameController").transform.parent.Find("WeatherController").GetComponent<WeatherController>();
        myAudioController = GameObject.FindWithTag("GameController").transform.parent.Find("AudioController").GetComponent<AudioController>();


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

    public void ClickCurItem()
    {
        if (myGameController.getCurScene() == GameController.GameScene.PlantLand)
        {
            if (!slotIsEmpty)
            {
                myAudioController.PlayUseItemSound();
                itemInUse.enabled = true;
                itemInUse.sprite = usedItemPairs[curItem];
                plant.UseCurItems(curItem);
                curItemUI.sprite = null;
                curItemUI.enabled = false;
                slotIsEmpty = true;
                lastUsedItem = curItem;
                myWeatherController.usedItemLastPhase = true;

            }

        }

    }

    public void changeItem(string name)
    {
        curItem = itemPairs[name];
        slotIsEmpty = false;
        curItemUI.enabled = true;
    }

    public void Initialization()
    {
        curItemUI.sprite = null;
        curItemUI.enabled = false;
        slotIsEmpty = true;
        itemInUse.sprite = null;
        itemInUse.enabled = false;
        for (int i = 0; i < itemInHome.Length; i++)
        {
            itemInHome[i].GetComponent<ClickItem>().Initialization();
        }

    }

}
