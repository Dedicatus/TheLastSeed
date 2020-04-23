using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenDisplay : MonoBehaviour
{
    //A separate OxygenDisplayCrontoller is probably needed for more complex OxygenDisplay
    [SerializeField] private OxygenController myOxygenController;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<UnityEngine.UI.Text>().text = myOxygenController.getCurOxygen().ToString();
    }
}
