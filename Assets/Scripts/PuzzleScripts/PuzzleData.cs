using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PuzzleData", menuName = "ScriptableObjects/SpawnPuzzleData", order = 1)]
public class PuzzleData : ScriptableObject
{
    public int puzzleIndex;
    public bool canLose;
    public int[] puzzleSolution;
    public int unlockedToolID;
}
