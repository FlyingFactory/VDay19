using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Puzzle_2 : Puzzle
{
    private PuzzleMap puzzle_2_Map;

    // Start is called before the first frame update
    protected override void Start()
    {
        ORIGIN_ID = "0575";
        puzzle_2_Map = new PuzzleMap(new List<ImageTarget>()
        { new ImageTarget(ORIGIN_ID, Direction.North, 0, 0, 1),
          new ImageTarget("0595", Direction.North, 0, -1, 2),
          new ImageTarget("0580", Direction.North, 0, -2, 3),
          new ImageTarget("0591", Direction.North, 1, -2, 4),
          new ImageTarget("0588", Direction.North, 2, -2, 5),
          new ImageTarget("0598", Direction.East, 1, -1, 6),
          new ImageTarget("0630", Direction.North, 2, 0, 7),
          new ImageTarget("0600", Direction.North, 2, -1, 8),
          new ImageTarget("0617", Direction.North, 1, 0, 0),
        });

        puzzleMap = puzzle_2_Map;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

}
