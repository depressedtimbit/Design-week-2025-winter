using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class DotLogic : MonoBehaviour
{
    public TextMeshPro numberDisplay;
    private int number;
    public int maxNumber;
    public int minNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        incrementNumber();
        updateDisplay();
    }

    private void incrementNumber()
    {
        if (number >= maxNumber)
        {
            number = minNumber;
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
