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

    public GameObject button3DGraphic, square3DGraphic;
    public GameObject sceneNumberText;

    // reference to this button's 3D graphic's script, so we can set the number
    private TestButton3D graphicScript3D;

    private void Start()
    {
        // disable spritrenderer and text, enable 3d graphic
        GetComponent<SpriteRenderer>().enabled = false;

        sceneNumberText.SetActive(false);

        if (clickToIncrement)
        {
            button3DGraphic.SetActive(true);
            graphicScript3D = button3DGraphic.GetComponent<TestButton3D>();
        }
        else
        {
            square3DGraphic.SetActive(true);
            graphicScript3D = square3DGraphic.GetComponent<TestButton3D>();
        }

        graphicScript3D.InitMaterials();
        graphicScript3D.SetNumber(number);

    }

    private void OnMouseDown()
    {
        if (clickToIncrement)
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

        graphicScript3D.nextNumber = number;
        graphicScript3D.MouseDown();
    }

    private void OnMouseUp()
    {
        graphicScript3D.MouseUp();
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
            otherDot.connectedDots.Contains(this);
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
    }
    public void SetNegative()
    {
        negativeButton = true;
    }
}
