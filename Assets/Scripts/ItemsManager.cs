using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsManager : MonoBehaviour
{
    public enum items {Sprinkler, Purifier, Pipe, Cover, Artificialsun}
    [Header("GameObjects")]
    public GameObject sprinkler;
    public GameObject purifier;
    public GameObject pipe;
    public GameObject cover;
    public GameObject artificialsun;
    public GameObject[] Supplys;
    public Image[] itemImages;
    [Header("Controller")]
    [SerializeField] private Plant plant;


    // Start is called before the first frame update
    void Start()
    {
        
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
                Debug.Log("1232132");

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

    public void fooSprinkler() { 
    
    }

    public void fooPurifier()
    {

    }

    public void fooCover()
    {

    }

    public void fooPipe()
    {

    }

    public void fooLight()
    {

    }


}
