using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.EventSystems;

public class PasswordCheck : MonoBehaviour
{
    public string correctPassword;
    public TMP_InputField passwordInput;
    public GameObject passwordUI;
    public GameObject leftChest;
    public GameObject rightChest;
    public Transform leftChestMove;
    public Transform rightChestMove;
    public GameObject dotPuzzle;

    // Start is called before the first frame update
    private void Start()
    {
        passwordInput.interactable = true;
        dotPuzzle.SetActive(false);
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnEndedInputField(string password)
    {
        if (password.ToLower() == correctPassword.ToLower())
        {
            passwordUI.SetActive(false);
            dotPuzzle.SetActive(true);
            ChestMove();

        } else
        {
            passwordInput.text = "";
        }
    }

    private void ChestMove()
    {
        leftChest.transform.position = leftChestMove.position;
        rightChest.transform.position = rightChestMove.position;
    }
}
