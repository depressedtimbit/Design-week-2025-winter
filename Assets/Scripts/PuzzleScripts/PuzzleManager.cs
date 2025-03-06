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
                AdditiveSceneManager.Instance.unloadScene(true, unlockedToolID);
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
                    //AdditiveSceneManager.Instance.restartScene();
                    // turn screen white
                    AudioManager.instance.PlaySound("explosion", 1, 0.1f, 1f, 0.1f);
                    CurtainManager.instance.Fade(true, 0.05f, false).setOnComplete(() =>
                    {
                        AdditiveSceneManager.Instance.unloadScene(false);
                    });

                    // wait
                    LeanTween.value(0, 1, 0.75f).setOnComplete(() =>
                    {
                        // fade black screen in
                        CurtainManager.instance.Fade(true, 0.5f).setOnComplete(() =>
                        {
                            // fade white screen out
                            CurtainManager.instance.Fade(false, 0.05f, false);

                            // wait 0.5 sec
                            LeanTween.value(0, 1, 0.5f).setOnComplete(() =>
                            {
                                // fade black screen out
                                CurtainManager.instance.Fade(false, 0.5f).setOnComplete(() =>
                                {
  
                                });
                            });
                        });
                    });
                } else
                {
                    currentState[i].number = minNumber;
                }
                
            }
        }
    }
}
