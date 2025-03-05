using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;


public class DotLogic : MonoBehaviour
{
    public TextMeshPro numberDisplay;
    public int number;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        incrementNumber();
        PuzzleManager.winCheck();
        PuzzleManager.loseCheck();
        updateDisplay();
    }

    private void incrementNumber()
    {
        number++;
    }
    private void updateDisplay()
    {
        numberDisplay.text = number.ToString();
    }
}
