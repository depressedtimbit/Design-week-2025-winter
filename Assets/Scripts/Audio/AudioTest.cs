using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;

public class AudioTest : MonoBehaviour
{

    public TMP_FontAsset font;
    public TMPro_FontAssetCreatorWindow wow;

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
    }
}
