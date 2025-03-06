using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RobotScript : MonoBehaviour
{
    public string sceneToLoad;
    public View view;

    private bool IsComplete;

    public Sprite completeSprite;

    void Start()
    {    
        if (view == null){
                Debug.LogWarning(gameObject.name + "has no set view!");
        }
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && !IsComplete)
        {
            AdditiveSceneManager.Instance.LoadScene(sceneToLoad, view.gameObject);
            //IsComplete = true;
        }
    }

    public void OnPuzzleComplete()
    {
        if (completeSprite != null) GetComponent<SpriteRenderer>().sprite = completeSprite;
        IsComplete = true;
    }
}

