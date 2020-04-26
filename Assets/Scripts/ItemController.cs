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
    [SerializeField] Image curItemUI;
    
    [Header("Controller")]
    [SerializeField] private Plant plant;
    [SerializeField] private WeatherController myWeatherController;
    

    public items lastUsedItem;
    public items curItem;

    private bool slotIsEmpty;

    // Start is called before the first frame update
    void Start()
    {
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
        if (!slotIsEmpty)
        {
            plant.UseCurItems(curItem);
            curItemUI.sprite = null;
            slotIsEmpty = true;
            lastUsedItem = curItem;
            myWeatherController.usedItemLatsPhase = true;
          
        }
       
    }

    //public void fooSprinkler() {
    //    sprinkler.SetActive(true);
    //}

    //public void fooSun()
    //{
    //    artificialsun.SetActive(true);
    //}

    //public void fooCover()
    //{
    //    cover.SetActive(true);
    //}

    //public void fooLight()
    //{

    //}


}
