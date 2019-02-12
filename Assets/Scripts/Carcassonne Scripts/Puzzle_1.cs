using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Puzzle_1 : Puzzle
{
    private PuzzleMap puzzle_1_Map;

    // Start is called before the first frame update
    protected override void Start()
    {
        ORIGIN_ID = "0575";
        puzzle_1_Map = new PuzzleMap(new List<ImageTarget>()
        { new ImageTarget(ORIGIN_ID, Direction.North, 0, 0, 1),
          new ImageTarget("0591", Direction.North, 0, 1, 2),
          new ImageTarget("0607", Direction.North, 0, 2, 3),
          new ImageTarget("0630", Direction.North, 0, 3, 4),
          new ImageTarget("0633", Direction.North, 1, 3, 5),
          new ImageTarget("0636", Direction.North, 2, 3, 6),
          new ImageTarget("0642", Direction.North, 3, 3, 7),
          new ImageTarget("0621", Direction.North, 3, 2, 8),
          new ImageTarget("0698", Direction.North, 2, 1, 9),
          new ImageTarget("0611", Direction.North, 1, 2, 10),
          new ImageTarget("0595", Direction.North, 1, 1, 11),
          new ImageTarget("0580", Direction.North, 1, 0, 12),
          new ImageTarget("0584", Direction.North, 2, 0, 13),
          new ImageTarget("0588", Direction.North, 3, 0, 14),
          new ImageTarget("0600", Direction.North, 3, 1, 15),
          new ImageTarget("0617", Direction.North, 2, 2, 0),
        });

        puzzleMap = puzzle_1_Map;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
