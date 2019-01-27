using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class LivePuzzle : MonoBehaviour
{
    public static PuzzleMap livePuzzleMap;
    public static float originXCoord;
    public static float originYCoord;
    public static float originZCoord;
    public static ImageTarget origin;
    public static GameObject originObject;
    public bool button1 = false;
    public static LivePuzzle current;
    private static AudioSource music;
    [SerializeField] private AudioClip victoryClip;
    [SerializeField] private GameObject conditionObject;

    public static bool puzzleSolved;
    public GameObject spark;

    public static List<GameObject> ropeGO;

    private void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
        InitialisePuzzleMap();
        puzzleSolved = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((button1 || DetectCondition()) && !puzzleSolved) // Replace with proper condition (e.g. button press)
        {
            livePuzzleMap = new PuzzleMap().GenerateLivePuzzleMap();
            CheckPuzzle();

            // temp
            //TrackerManager.Instance.GetTracker<ObjectTracker>().Start();
            button1 = false;
        }
    }

    private bool DetectCondition()
    {
        if (conditionObject == null) return false;
        else if ((int)conditionObject.GetComponent<TrackableBehaviour>().CurrentStatus > 2) return true;
        else return false;
    }

    private static void InitialisePuzzleMap()
    {
        origin = Puzzle.origin;

    }

    public static void SetOriginCoords()
    {
        originXCoord = originObject.transform.position.x;
        originYCoord = originObject.transform.position.y;
        originZCoord = originObject.transform.position.z;

    }

    public void CheckPuzzle()
    {

        if (livePuzzleMap.SortedPuzzle().Equals(Puzzle.puzzleMap))
        {
            // Insert victory and exit
            Debug.Log("Puzzle is finished");
            music.clip = victoryClip;
            music.Play();
            puzzleSolved = true;
            LightRope(spark);
        }
        else
        {
            // Insert failure
            Debug.Log("Puzzle does not match");
        }
    }

    private static void LightRope(GameObject spark)
    {
        ropeGO = new List<GameObject>();

        foreach (GameObject currentGO in livePuzzleMap)
        {
            Debug.Log(currentGO.GetComponent<ImgTargetBehaviour>().thisImageTarget.ropeOrder);
            if (currentGO.GetComponent<ImgTargetBehaviour>().thisImageTarget.ropeOrder > 0)
            {
                ropeGO.Add(currentGO);
            }
        }

        spark.SetActive(true);
    }

}
