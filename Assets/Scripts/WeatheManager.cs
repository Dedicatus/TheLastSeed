using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatheManager : MonoBehaviour
{
    public enum weatherList { AcidRain, SandStorm, HighTemp, Cold, Normal }
    //DEFINE VARIABLES
    [SerializeField] float lastTime;
    private float lastTimer;
    [SerializeField] float normalTime;
    private float normalTimer;
    public weatherList nowWeather;
    [SerializeField] bool inAccident;
    // Start is called before the first frame update
    void Start()
    {
        //for (int i = 0; i < 10; i++)
        //{
        //    nowWeather = (weatherList)Random.Range(0, 4);   
        //    Debug.Log(nowWeather.ToString());
        //}


        inAccident = false;
        lastTime = 15f;
        lastTimer = 0f;
        normalTime = 40f;
        normalTimer = 0f;


    }

    // Update is called once per frame
    void Update()
    {
        if (!inAccident)
        {
            normalTimer += Time.deltaTime;
            if (normalTime <= normalTimer) {
                ChangeWeather();
                normalTimer = 0;
            }
        }
        else 
        {


            lastTimer += Time.deltaTime;
            if (lastTime <= lastTimer) {
                BackNormal();
                lastTimer = 0;
            }
        }
    }

 


    void ChangeWeather() {
        nowWeather = (weatherList)Random.Range(0, 5);
        Debug.Log(nowWeather.ToString());
        switch (nowWeather)
        {
        
        }
    }

    // FUNCTIONS HANDLING DIFFERENT WEATHER CHANGE
    void BackNormal() { 
    
    }

    void fooAcidRain() { 
    
    
    }

    void fooSandStorm() { 
    
    }

    void fooHighTemp() { 
    
    }

    void fooCold() { 
    
    }
}

