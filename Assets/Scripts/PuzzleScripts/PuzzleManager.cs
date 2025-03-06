using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    public DotLogic[] dots;
    public PuzzleData puzzleData;
    public static int[] solution;
    public static int unlockedToolID; 
    public static DotLogic[] currentState;
    public static int maxNumber = 5;
    public static int minNumber = 0;
    private static bool canLose;

    private void Start()
    {
        currentState = dots;
        solution = puzzleData.puzzleSolution;
        canLose = puzzleData.canLose;
        unlockedToolID = puzzleData.unlockedToolID;
    }
    public static void winCheck()
    {
        int correct = 0;
        for(int i=0;i<solution.Length; i++)
        {
            if (solution[i] == currentState[i].number)
            {
                correct++;
            }
        }
        if (correct == solution.Length)
        {
            if(AdditiveSceneManager.Instance != null)
            {
                AdditiveSceneManager.Instance.unloadScene(unlockedToolID);
            }
            else Debug.Log("PuzzleWon");
        }
    }
    public static void loseCheck()
    {
        for (int i = 0; i < solution.Length; i++)
        {
            
            if (currentState[i].number >maxNumber)
            {
                if (canLose)
                {
                    Debug.Log("game Lost");
                    if (AdditiveSceneManager.Instance != null ) AdditiveSceneManager.Instance.restartScene();
                } else
                {
                    currentState[i].number = minNumber;
                }
                
            }
        }
    }
}
