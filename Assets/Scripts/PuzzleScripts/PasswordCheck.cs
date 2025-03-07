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

    private PuzzleManager p;

    // Start is called before the first frame update
    private void Start()
    {
        passwordInput.interactable = true;
        dotPuzzle.SetActive(false);
        EventSystem.current.SetSelectedGameObject(gameObject);

        p = FindObjectOfType<PuzzleManager>();

        // if the player has already solved this robot's password, instantly bypass the password field
        if (PlayerData.Instance != null && PlayerData.Instance.puzzlePasswordCracked[p.puzzleData.puzzleIndex])
        {
            OnPasswordSolved();
        }
    }

    public void OnEndedInputField(string password)
    {
        if (password.ToLower() == correctPassword.ToLower())
        {
            if (PlayerData.Instance != null) PlayerData.Instance.puzzlePasswordCracked[p.puzzleData.puzzleIndex] = true;
            OnPasswordSolved();

        } else
        {

            passwordInput.text = "";
        }
    }

    private void OnPasswordSolved()
    {
        passwordUI.SetActive(false);
        dotPuzzle.SetActive(true);
        ChestMove();
    }

    private void ChestMove()
    {
        leftChest.transform.position = leftChestMove.position;
        rightChest.transform.position = rightChestMove.position;
    }
}
