using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantDisplay : MonoBehaviour
{
    [SerializeField] private GameObject[] plantObjects;
    [SerializeField] private Plant myPlantContoller;

    // Start is called before the first frame update
    void Awake()
    {
        plantObjects[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextStage()
    {
        plantObjects[myPlantContoller.getCurStage() - 1].SetActive(false);
        plantObjects[myPlantContoller.getCurStage()].SetActive(true);
    }

    public int getMaxStage()
    {
        return plantObjects.Length;
    }
}
