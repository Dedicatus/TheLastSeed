using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [Header("Stage")]
    [SerializeField] private Sprite[] plantSprites;

    [Header("Time")]
    [SerializeField] private int stageDayGap = 3;

    [Header("Health")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int HealthRecover = 20;

    [Header("Debug")]
    [SerializeField] private int maxStage;
    [SerializeField] private int curStage;
    [SerializeField] private int dayUntilNextStage = 0;
    [SerializeField] private int curHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        curStage = 0;
        maxStage = plantSprites.Length;
        gameObject.GetComponent<SpriteRenderer>().sprite = plantSprites[curStage];
        curHealth = maxHealth;
        dayUntilNextStage = stageDayGap;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void nextStage()
    {
        if (curStage + 1 <= maxStage)
        {
            ++curStage;
            gameObject.GetComponent<SpriteRenderer>().sprite = plantSprites[curStage];
        }
        else
        {
            Debug.LogError("Plant max stage reached!");
        }
    }

    public void dayPassed()
    {
        dayUntilNextStage--;
        if (dayUntilNextStage == 0)
        {
            dayUntilNextStage = stageDayGap;
            nextStage();
            addHealth(HealthRecover);
        }
    }

    public void addHealth(int n)
    {
        if (curHealth + n > maxHealth)
        {
            curHealth = maxHealth;
        }
        else
        {
            curHealth += n;
            if (curHealth <= 0)
            {
                //GameOver
            }
        }
    }
    
    public int getDayUntilNextStage()
    {
        return dayUntilNextStage;
    }

    public int getCurHealth()
    {
        return curHealth;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }
}
