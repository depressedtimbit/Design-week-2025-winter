using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private bool IsOpen;
    public int DoorID;
    public int RequiredTool;
    public Sprite lockedSprite;
    public Sprite UnlockedSprite;
    public View view;

    // Start is called before the first frame update
    void Start()
    {
        if (view == null){
            Debug.LogWarning(gameObject.name + "has no set view!");
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = lockedSprite;
        if (PlayerData.Instance.DoorStates[DoorID] == true)
        {
            UnlockDoor();
        }
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsOpen)
            {
                ViewManager.Instance.ChangeView(view); 
            }
            else if (PlayerData.Instance.ToolStates[RequiredTool] == true)
            {
                UnlockDoor();
            }
            else
            {
                Debug.Log("I can't Seem to open it yet...");
            }
        }
    }

    void UnlockDoor()
    {
        IsOpen = true;
        spriteRenderer.sprite = UnlockedSprite;
    }
}
