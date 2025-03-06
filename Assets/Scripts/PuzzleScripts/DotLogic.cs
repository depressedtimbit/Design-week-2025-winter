using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Linq;


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
            
            List<DotLogic> foundConnected = new List<DotLogic>();
            foreach(DotLogic dot in connectedDots)
            {
                foundConnected.Add(dot);
            }
            for(int i=0; i<foundConnected.Count; i++)
            {
                for (int j = 0; j < foundConnected[i].connectedDots.Count; j++)
                //foreach(DotLogic otherDot in dot.connectedDots)
                {
                    if (!foundConnected.Contains(foundConnected[i].connectedDots[j]) && foundConnected[i].connectedDots[j] != this)
                    {
                        foundConnected.Add(foundConnected[i].connectedDots[j]);
                        Debug.Log(foundConnected[i].connectedDots[j]);
                    }
                }
                foundConnected[i].incrementNumber();
                foundConnected[i].updateDisplay();
            }
            incrementNumber();
            PuzzleManager.winCheck();
            PuzzleManager.loseCheck();
            updateDisplay();
        }
        
    }

    private void incrementNumber()
    {
        if (positiveButton)
        {
            number += 2;
        }
        else if (negativeButton)
        {
            number -= 1;
        }
        else
        {
            number++;
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

        /*foreach(DotLogic dots in otherDot.connectedDots)
        {
            if (dots != this && !dots.connectedDots.Contains(this))
            {
                Debug.Log("connected");
                connectedDots.Add(dots);
                dots.connectedDots.Add(this);
            }
        }*/
    }

    public void DisconnectDots(DotLogic otherDot)
    {
        Debug.Log("cut");
        connectedDots.Remove(otherDot);
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
