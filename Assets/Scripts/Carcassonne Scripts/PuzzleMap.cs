using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class PuzzleMap : IEquatable<PuzzleMap>, IEnumerable
{
    public List<ImageTarget> puzzleMap;
    public List<GameObject> imgTargetGO;

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
            else imgTargetGO[i].GetComponent<ImgTargetBehaviour>().thisImageTarget.ropeOrder
                = otherMap.puzzleMap[i].ropeOrder;
        }
        return true;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return (IEnumerator) GetEnumerator();
    }

    public PuzzleMapEnum GetEnumerator()
    {
        return new PuzzleMapEnum(imgTargetGO);
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
            }
        }
        ImgTargetBehaviour[] imageTargets = new ImgTargetBehaviour[imageTargetGO.Count];
        ImageTarget[] imageTargetProperties = new ImageTarget[imageTargetGO.Count];

        //TrackerManager.Instance.GetTracker<ObjectTracker>().Stop();

        bool originFound = false;

        for (int i = 0; i < imageTargetGO.Count; i++)
        {
            if (!originFound)
            {
                imageTargets[i] = imageTargetGO[i].GetComponent<ImgTargetBehaviour>();

                if (imageTargets[i].thisImageTarget.targetId == Puzzle.ORIGIN_ID)
                {
                    imageTargets[i].thisImageTarget.currentDirection = Direction.North;
                    imageTargets[i].thisImageTarget.targetId = Puzzle.ORIGIN_ID;
                    imageTargets[i].thisImageTarget.currentTargetPosition = new TargetPosition(0, 0);
                    LivePuzzle.origin = imageTargets[i].thisImageTarget;
                    LivePuzzle.originObject = imageTargetGO[i];
                    LivePuzzle.SetOriginCoords();
                    i = 0;
                    originFound = true;
                    imageTargetProperties[i] = imageTargets[i].thisImageTarget;
                }
                else
                {
                }
            }
            else if (originFound)
            {
                imageTargets[i] = imageTargetGO[i].GetComponent<ImgTargetBehaviour>();

                if ((imageTargets[i].thisImageTarget.targetId != Puzzle.ORIGIN_ID)||true)
                {
                    imageTargets[i].UpdateCurrentDirection();
                    imageTargets[i].SetTargetPosition();
                    imageTargetProperties[i] = imageTargets[i].thisImageTarget;

                }
            }
        }
        livePuzzleMap.puzzleMap = imageTargetProperties.ToList();
        livePuzzleMap.imgTargetGO = imageTargetGO;
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

public class PuzzleMapEnum : IEnumerator
{
    public List<GameObject> imgTargetGO;
    int position = -1;
    
    public PuzzleMapEnum(List<GameObject> imgTargetGO)
    {
        this.imgTargetGO = imgTargetGO;
    }

    public bool MoveNext()
    {
        position++;
        return (position < imgTargetGO.Count);
    }

    public void Reset() { position = -1; }

    object IEnumerator.Current
    {
        get { return Current; }
    }

    public GameObject Current
    {
        get
        {
            try
            {
                return imgTargetGO[position];
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException();
            }
        }
    }
}