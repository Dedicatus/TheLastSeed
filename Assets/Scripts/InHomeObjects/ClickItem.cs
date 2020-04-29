using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickItem : MonoBehaviour
{
    [SerializeField] float coolDownTime;
    [SerializeField] bool isCoolDown;
    [SerializeField] Image mask;
    

    private float coolDownTimer;
    [SerializeField] private ItemController myItemController;
    [SerializeField] private GameController myGameController;

    // Start is called before the first frame update
    void Awake()
    {
        isCoolDown = false;
        mask = this.transform.Find("Mask").GetComponent<Image>();
        myItemController = GameObject.FindWithTag("GameController").transform.parent.Find("ItemController").GetComponent<ItemController>();
        myGameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();

        coolDownTime = 192f;
        mask.fillAmount = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isCoolDown && myGameController.timePassing) {
            coolDownTimer += Time.deltaTime;
            mask.fillAmount = 1 - coolDownTimer / coolDownTime;

            if (coolDownTimer >= coolDownTime) {
                isCoolDown = false;
                coolDownTimer = 0;
                mask.fillAmount = 0;

            }
        }
        
    }

    public void Click()
    {
        if (isCoolDown != true) {
            isCoolDown = true;
            mask.fillAmount = 1;
            myItemController.curItemUI.sprite = this.GetComponent<Image>().sprite;
            myItemController.changeItem(this.name);
            
        }
    }
}
