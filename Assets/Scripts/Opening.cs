using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opening : MonoBehaviour
{
    private UnityEngine.Video.VideoPlayer opening;
    private GameController myGameController;
    private bool started;
    private float timer;
    private float waitTime = 3.0f;
    // Start is called before the first frame update
    void Awake()
    {
        myGameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        opening = gameObject.GetComponent<UnityEngine.Video.VideoPlayer>();
        gameObject.GetComponent<RawImage>().enabled = true;
        opening.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (!opening.isPlaying && timer > waitTime)
        {
            gameObject.SetActive(false);
            myGameController.timePassing = true;
        }
        
    }
}
