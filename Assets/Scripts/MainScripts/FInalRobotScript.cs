using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FInalRobotScript : MonoBehaviour
{
    public string sceneToLoad;

    public int RequiredTool;
    private SpriteRenderer spriteRenderer;
    public Sprite lockedSprite;
    public Sprite UnlockedSprite;
    public View view;
    private bool IsComplete = false;
    // Start is called before the first frame update
    void Start()
    {
        if (view == null){
            Debug.LogWarning(gameObject.name + "has no set view!");
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = lockedSprite;
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsComplete)
            {
                ViewManager.Instance.ChangeView(view); 
            }
            else if (PlayerData.Instance.ToolStates[RequiredTool] == true)
            {
                UnlockDoor();
            }
            else
            {
                //Debug.Log("I can't Seem to open it yet...");
                SubtitleManager.instance.DoDialogue("I can't seem to open it yet...");
            }
        }
    }

    void UnlockDoor()
    {
        AdditiveSceneManager.Instance.LoadScene(sceneToLoad, view.gameObject);
        IsComplete = true;
        spriteRenderer.sprite = UnlockedSprite;
    }
}
