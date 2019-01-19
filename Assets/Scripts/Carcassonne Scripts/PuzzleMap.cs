using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class PuzzleMap : IEquatable<PuzzleMap>
{
    public List<ImageTarget> puzzleMap;

    public PuzzleMap()
    {
        this.puzzleMap = new List<ImageTarget>();
    }

    public PuzzleMap(List<ImageTarget> targets)
    {
        this.puzzleMap = targets;
    }

    public bool Equals(PuzzleMap otherMap)
    {
        for (int i = 0; i < otherMap.puzzleMap.Count; i++)
        {
            if (!this.puzzleMap.Contains(otherMap.puzzleMap[i])) return false;
        }
        return true;
    }

    public PuzzleMap SortedPuzzle ()
    {
        puzzleMap.Sort(delegate(ImageTarget x, ImageTarget y)
        {
            if (x.currentTargetPosition.zCoord > y.currentTargetPosition.zCoord)
                return 1;
            else if (x.currentTargetPosition.zCoord < y.currentTargetPosition.zCoord)
                return -1;
            else if (x.currentTargetPosition.xCoord > y.currentTargetPosition.xCoord)
                return 1;
            else if (x.currentTargetPosition.xCoord < y.currentTargetPosition.xCoord)
                return -1;
            else return 0;
        });
        return this;
    }

    private PuzzleMap RotatedMap(int repititions = 1)
    {
        for (int i = 0; i < repititions; i++)
        {
            foreach (ImageTarget target in this.puzzleMap)
            {
                if (target.currentDirection == Direction.West)
                    target.currentDirection = Direction.North;
                else target.currentDirection++;
            }
        }
        return this;
    }

    public PuzzleMap GenerateLivePuzzleMap()
    {
        PuzzleMap livePuzzleMap = new PuzzleMap();
        List<GameObject> allImageTargetGO = GameObject.FindGameObjectsWithTag("image_target").ToList();
        List<GameObject> imageTargetGO = new List<GameObject>();
        foreach (GameObject currentImageTargetGO in allImageTargetGO)
        {
            if ((int)currentImageTargetGO.GetComponent<TrackableBehaviour>().CurrentStatus > 2)
            {
                imageTargetGO.Add(currentImageTargetGO);
                Debug.Log("Active GO");
            }
        }
        ImageTarget[] imageTargets = new ImageTarget[imageTargetGO.Count];

        TrackerManager.Instance.GetTracker<ObjectTracker>().Stop();

        bool originFound = false;

        for (int i = 0; i < imageTargetGO.Count; i++)
        {
            if (!originFound)
            {
                imageTargets[i] = imageTargetGO[i].GetComponent<ImageTarget>();

                if (imageTargets[i].targetId == Puzzle.ORIGIN_ID)
                {
                    imageTargets[i].currentDirection = Direction.North;
                    LivePuzzle.origin = imageTargets[i];
                    LivePuzzle.originObject = imageTargetGO[i];
                    LivePuzzle.SetOriginCoords();
                    i = 0;
                    originFound = true;
                    AlignToAxes(imageTargetGO);
                }
                else
                {
                }
            }
            else if (originFound)
            {
                imageTargets[i] = imageTargetGO[i].GetComponent<ImageTarget>();

                if (imageTargets[i].targetId != Puzzle.ORIGIN_ID)
                {
                    imageTargets[i].UpdateCurrentDirection();
                    imageTargets[i].SetTargetPosition();
                }
            }
        }
        livePuzzleMap.puzzleMap = imageTargets.ToList();
        return livePuzzleMap;
    }

    public void AlignToAxes(List<GameObject> imageTargetGO)
    {
        foreach (GameObject currentGO in imageTargetGO)
        {
            currentGO.transform.position -= new Vector3(LivePuzzle.originXCoord,
                LivePuzzle.originYCoord, LivePuzzle.originZCoord);
        }

        for (int i = 0; i < 360; i++)
        {
            LivePuzzle.current.transform.Rotate(new Vector3(1.0f, 0, 0), Space.World);
            if (Mathf.Abs(LivePuzzle.originObject.transform.eulerAngles.x) <= 1.0f) break;
        }

        for (int i = 0; i < 360; i++)
        {
            LivePuzzle.current.transform.Rotate(new Vector3(0, 1.0f, 0), Space.World);
            if (Mathf.Abs(LivePuzzle.originObject.transform.eulerAngles.y) <= 1.0f) break;
        }

        for (int i = 0; i < 360; i++)
        {
            LivePuzzle.current.transform.Rotate(new Vector3(0, 0, 1.0f), Space.World);
            if (Mathf.Abs(LivePuzzle.originObject.transform.eulerAngles.z) <= 1.0f) break;
        }


        Debug.Log(LivePuzzle.originObject.transform.eulerAngles.x);
        Debug.Log(LivePuzzle.originObject.transform.eulerAngles.y);
        Debug.Log(LivePuzzle.originObject.transform.eulerAngles.z);
    }

}
