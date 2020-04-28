using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenBarScript : MonoBehaviour
{
    private Transform myTransform;
    [SerializeField] private OxygenController oxygenController;
    private Color outsideColor = Color.red;
    private Color insideColor = Color.green;
    [SerializeField] private GameObject topColor;
    // Start is called before the first frame update
    void Start()
    {
        myTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        setOxygenLevelinUI();
        changeColor();
    }

    public void setOxygenLevelinUI(){
        if(oxygenController.getMaxOxygen() > 0){
            float level = oxygenController.getCurOxygen() / oxygenController.getMaxOxygen();
            myTransform.localScale = new Vector3(1f, level);
        }
    }

    public void changeColor(){
        if(!oxygenController.oxygenConsuming){
            topColor.GetComponent<UnityEngine.UI.Image>().color = insideColor;
        } else {
            topColor.GetComponent<UnityEngine.UI.Image>().color = outsideColor;
        }
    }
}
