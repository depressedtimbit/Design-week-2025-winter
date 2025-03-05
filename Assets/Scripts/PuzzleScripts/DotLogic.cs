using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;


public class DotLogic : MonoBehaviour
{
    public TextMeshPro numberDisplay;
    public int number;
    public List<DotLogic> connectedDots = new List<DotLogic>();
    public bool clickToIncrement = true;
    private bool positiveButton = false;
    private bool negativeButton = false;


    private void OnMouseDown()
    {
        if (clickToIncrement && !ConnectWires.probingActive)
        {
            if (positiveButton)
            {
                number += 1;
            }

            if (negativeButton)
            {
                if (number > 0)
                    number -= 2;
            }

            incrementNumber();

            PuzzleManager.winCheck();
            PuzzleManager.loseCheck();
            updateDisplay();
        }
        
    }

    private void incrementNumber()
    {

        number++;

        foreach (DotLogic dot in connectedDots)
        {
            if (dot != this)
            {
                if (dot.positiveButton)
                {
                    dot.number += 2;
                    dot.updateDisplay();
                }
                else if (dot.negativeButton)
                {
                    dot.number -= 1;
                    dot.updateDisplay();
                }
                else
                {
                    dot.number++;
                    dot.updateDisplay();
                }
                
            }
        }
    }
    private void updateDisplay()
    {
        numberDisplay.text = number.ToString();
    }

    public void ConnectDots(DotLogic otherDot)
    {
        if (!connectedDots.Contains(otherDot))
        {
            connectedDots.Add(otherDot);
        }
        if (!otherDot.connectedDots.Contains(this))
        {
            otherDot.connectedDots.Add(this);
        }

        foreach(DotLogic dots in otherDot.connectedDots)
        {
            if (dots != this && !dots.connectedDots.Contains(this))
            {
                Debug.Log("connected");
                connectedDots.Add(dots);
                dots.connectedDots.Add(this);
            }
        }
    }

    public void DisconnectDots(DotLogic otherDot)
    {
        connectedDots.Remove(otherDot);
        otherDot.connectedDots.Remove(this);
    }

    public void SetPositive()
    {
        positiveButton = true;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = Color.red;
    }

    public void SetNegative()
    {
        negativeButton = true;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = Color.blue;
    }
}
