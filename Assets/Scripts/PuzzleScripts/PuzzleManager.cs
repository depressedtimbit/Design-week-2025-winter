using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public DotLogic[] dots;
    public PuzzleData puzzleData;
    public static int[] solution;
    public static DotLogic[] currentState;
    public static int maxNumber = 5;
    public static int minNumber = 0;
    public static int loseValue;

    private void Start()
    {
        currentState = dots;
        solution = puzzleData.puzzleSolution;
        loseValue = puzzleData.loseValue;
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
            Debug.Log("YOU WIN");
        }
    }
    public static void loseCheck()
    {
        for (int i = 0; i < solution.Length; i++)
        {
            if (currentState[i].number == loseValue)
            {
                Debug.Log("YOU LOSE");
            }
        }
    }
}
