using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenDisplay : MonoBehaviour
{
    [SerializeField] private OxygenContoller myOxygenContoller;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<UnityEngine.UI.Text>().text = myOxygenContoller.getCurOxygen().ToString();
    }
}
