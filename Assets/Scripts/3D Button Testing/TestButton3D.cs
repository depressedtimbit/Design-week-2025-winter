using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TestButton3D : MonoBehaviour
{
    public Transform button;

    // x = unpressed Y, y = pressed Y
    public Vector2 buttonPressY;

    public float pressDownTime = 0.15f, pressUpTime = 0.2f;

    private bool mousePressed = false, buttonPressed = false;
    private bool pressFinished = true;

    private int currentNumber;
    public TextMeshPro numberText;

    // 0 = no squish, 1 = horizontal squish, 2 = vertical squish
    public int textSquishMode = 0;

    public float maxEmissionSaturation = 0.8f;

    private Renderer buttonRenderer;
    private MaterialPropertyBlock m;

    public bool isSquare = false;

    // stores the number to switch to after the press graphic is complete
    // this is set in DotLogic
    public int nextNumber;

    // Start is called before the first frame update
    void Start()
    {
        InitMaterials();
       
    }
    
    public void InitMaterials()
    {
        if (!isSquare)
        {
            buttonRenderer = button.GetComponent<Renderer>();
            m = new MaterialPropertyBlock();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isSquare) return;
     if (!mousePressed && buttonPressed && pressFinished)
        {
            pressFinished = false;
            LeanTween.value(gameObject, 0, 1, pressUpTime).setOnUpdate((float val) =>
            {
                Vector3 pos = button.localPosition;
                pos.y = Mathf.Lerp(buttonPressY.y, buttonPressY.x, val);
                button.localPosition = pos;

                // unsquish text
                if (textSquishMode != 0)
                {
                    Vector3 scale = numberText.gameObject.transform.localScale;
                    if (textSquishMode == 1) scale.x = val;
                    else if (textSquishMode == 2) scale.y = val;
                    numberText.gameObject.transform.localScale = scale;
                }
               
            }).setOnComplete(() =>
            {
                buttonPressed = false;
            });
        }   
    }


    private void OnMouseDown()
    {
        nextNumber = currentNumber + 1;
        nextNumber %= 6;

        MouseDown();
    }

    private void OnMouseUp()
    {
        MouseUp();
    }

    public void MouseDown()
    {
        if (mousePressed) return;

        print("mouse down");

        if (isSquare)
        {
            OnPressComplete();
            return;
        }

        buttonPressed = true;
        mousePressed = true;
        pressFinished = false;

        LeanTween.value(gameObject, 0, 1, pressDownTime).setOnUpdate((float val) =>
        {
            Vector3 pos = button.localPosition;
            pos.y = Mathf.Lerp(buttonPressY.x, buttonPressY.y, val);
            button.localPosition = pos;

            // squish text
            if (textSquishMode != 0)
            {
                Vector3 scale = numberText.gameObject.transform.localScale;
                if (textSquishMode == 1) scale.x = 1 - val;
                else if (textSquishMode == 2) scale.y = 1 - val;
                numberText.gameObject.transform.localScale = scale;
            }
        }).setOnComplete(() =>
        {
            pressFinished = true;
            OnPressComplete();
        });
    }

    public void MouseUp()
    {
        if (!mousePressed) return;

        mousePressed = false;

    }

    private void OnMouseEnter()
    {
        
    }

    private void OnMouseExit()
    {
        
    }

    public void SetNumber(int n)
    {
        currentNumber = n;

        numberText.text = currentNumber.ToString();

        if (!isSquare)
        {
            m.SetColor("_EmissionColor", Color.HSVToRGB(0, 1, (currentNumber / 5f) * maxEmissionSaturation));
            buttonRenderer.SetPropertyBlock(m);
        }
    }

    private void OnPressComplete()
    {
        //currentNumber++;
        //currentNumber %= 6;

        SetNumber(nextNumber);

       
    }
}
