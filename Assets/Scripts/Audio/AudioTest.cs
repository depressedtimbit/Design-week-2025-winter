using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AudioTest : MonoBehaviour
{

    private float currentVolume;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.instance.PlaySound("metal_clang", 1, 0.2f, 0.5f, 1f);
        }   

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SubtitleManager.instance.DoDialogue("I am a robot... I have robot thoughts...");
        }
    }
}
