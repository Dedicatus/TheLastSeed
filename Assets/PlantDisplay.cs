using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantDisplay : MonoBehaviour
{
    [SerializeField] private Sprite[] plantSprites;
    [SerializeField] private Plant myPlantContoller;

    // Start is called before the first frame update
    void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = plantSprites[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextStage()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = plantSprites[myPlantContoller.getCurStage()];
    }

    public int getMaxStage()
    {
        return plantSprites.Length;
    }
}
