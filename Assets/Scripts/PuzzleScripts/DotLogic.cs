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

    public GameObject button3DGraphic, square3DGraphic;
    public GameObject sceneNumberText;

    // reference to this button's 3D graphic's script, so we can set the number
    private TestButton3D graphicScript3D;

    public GameObject positiveParticles, negativeParticles;

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
        graphicScript3D.nextNumber = number;
        graphicScript3D.SetNumber(number);

    }


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

        if (clickToIncrement)
        {
            graphicScript3D.nextNumber = number;
            graphicScript3D.MouseDown();
        }
    }

    private void OnMouseUp()
    {
        //AudioManager.instance.PlaySound("buttonUp", 1, 0.1f, 1f, 0.1f);
        graphicScript3D.MouseUp();
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


        graphicScript3D.nextNumber = number;
        graphicScript3D.SetNumber(number);

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

        positiveParticles.SetActive(true);
        negativeParticles.SetActive(false);

    }

    public void SetNegative()
    {
        negativeButton = true;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = Color.blue;

        negativeParticles.SetActive(true);
        positiveParticles.SetActive(false);

    }
}
