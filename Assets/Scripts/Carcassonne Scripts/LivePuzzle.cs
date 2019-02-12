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
    public static int currentPlayer;

    public static bool puzzleSolved;
    public GameObject spark;

    private static List<GameObject> imageTargetGOs;
    private static GameObject answerGO;

    [SerializeField] private GameObject player1GO;
    [SerializeField] private GameObject player2GO;

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
        GetImageTargets();
    }

    private bool playerChosen = false;
    private float timeSinceLastChecked = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (!playerChosen)
        {
            if ((int)player1GO.GetComponent<TrackableBehaviour>().CurrentStatus > 2)
            {
                InitialisePlayer1();
                currentPlayer = 1;
                playerChosen = true;
            }
            else if ((int)player2GO.GetComponent<TrackableBehaviour>().CurrentStatus > 2)
            {
                InitialisePlayer2();
                currentPlayer = 2;
                playerChosen = true;
            }
        }
        else if ((button1 || DetectCondition()) && !puzzleSolved && (timeSinceLastChecked >= 6.0f)) // Replace with proper condition (e.g. button press)
        {
            livePuzzleMap = new PuzzleMap().GenerateLivePuzzleMap();
            CheckPuzzle();

            // temp
            //TrackerManager.Instance.GetTracker<ObjectTracker>().Start();
            button1 = false;
            timeSinceLastChecked = 0.0f;
        }
        timeSinceLastChecked += Time.deltaTime;
    }

    private void GetImageTargets()
    {
        imageTargetGOs = new List<GameObject>();
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).GetComponent<ImgTargetBehaviour>() != null)
            {
                if (this.transform.GetChild(i).GetComponent<ImgTargetBehaviour>().targetId.Equals("0647"))
                {
                    answerGO = this.transform.GetChild(i).gameObject;
                }
                else
                {
                    imageTargetGOs.Add(this.transform.GetChild(i).gameObject);
                }

                for (int j = 0; j < this.transform.GetChild(i).childCount; j++)
                {
                    this.transform.GetChild(i).GetChild(j).gameObject.SetActive(false);
                }
            }
        }
    }

    bool alreadyChecked = false;

    private bool DetectCondition()
    {
        if (conditionObject == null) return false;
        else if ((int)conditionObject.GetComponent<TrackableBehaviour>().CurrentStatus > 2
            && !alreadyChecked)
        {
            alreadyChecked = true;
            return true;
        }
        else
        {
            alreadyChecked = false;
            return false;
        }
    }

    private static void InitialisePuzzleMap()
    {
        origin = Puzzle.origin;

    }

    private static void InitialisePlayer1()
    {
        Debug.Log("initialising player 1");
        foreach (GameObject gobject in imageTargetGOs)
        {
            for (int j = 0; j < gobject.transform.childCount; j++)
            {
                gobject.transform.GetChild(j).gameObject.SetActive(true);
            }
        }
    }

    private static void InitialisePlayer2()
    {
        Debug.Log("initialising player 2");
        for (int j = 0; j < answerGO.transform.childCount; j++)
        {
            answerGO.transform.GetChild(j).gameObject.SetActive(true);
        }
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
            if (currentPlayer == 1)
            {
                Debug.Log("Puzzle is finished");
                music.clip = victoryClip;
                music.Play();
                puzzleSolved = true;
                LightRope(spark);
            } else if (currentPlayer == 2)
            {
                Debug.Log("Puzzle is finished");
                music.clip = victoryClip;
                music.Play();
                puzzleSolved = true;
                foreach (GameObject gobject in imageTargetGOs)
                {
                    gobject.SetActive(true);
                    for (int j = 0; j < gobject.transform.childCount; j++)
                    {
                        gobject.transform.GetChild(j).gameObject.SetActive(true);
                    }
                }

                 LightRope(spark);
            }
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
            if (currentGO.GetComponent<ImgTargetBehaviour>().thisImageTarget.ropeOrder > 0)
            {
                ropeGO.Add(currentGO);
            }
        }

        spark.SetActive(true);
    }

}
