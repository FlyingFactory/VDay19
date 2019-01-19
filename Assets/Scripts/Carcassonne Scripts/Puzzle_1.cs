using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Puzzle_1 : Puzzle
{
    private PuzzleMap puzzle_1_Map;

    // Start is called before the first frame update
    protected override void Start()
    {
        ORIGIN_ID = "0552";
        puzzle_1_Map = new PuzzleMap(new List<ImageTarget>()
        { new ImageTarget(ORIGIN_ID, Direction.North, 0, 0),
          new ImageTarget("0557", Direction.North, 1, 0)
        });

        puzzleMap = puzzle_1_Map;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
