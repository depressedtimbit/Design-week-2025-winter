using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;


public class DotLogic : MonoBehaviour
{
    public TextMeshPro numberDisplay;
    public int number;

    void Update()
    {
       
    }

    private void OnMouseDown()
    {
        incrementNumber();
        updateDisplay();
        PuzzleManager.winCheck();
        PuzzleManager.loseCheck();
    }

    private void incrementNumber()
    {
        if (number >= PuzzleManager.maxNumber)
        {
            number = PuzzleManager.minNumber;
        } else
        {
            number++;
        }
    }
    private void updateDisplay()
    {
        numberDisplay.text = number.ToString();
    }
}
