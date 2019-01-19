using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Puzzle : MonoBehaviour
{
    public static PuzzleMap puzzleMap;
    public static ImageTarget origin;
    public static string ORIGIN_ID;

    protected virtual void Start()
    {
        origin = new ImageTarget(ORIGIN_ID);
        origin.currentTargetPosition = new TargetPosition(0, 0);
    }
}
