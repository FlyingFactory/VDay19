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
    private AudioSource music;
    public AudioClip victoryClip;

    private void Awake()
    {
        current = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
        InitialisePuzzleMap();
    }

    // Update is called once per frame
    void Update()
    {
        if (button1) // Replace with proper condition (e.g. button press)
        {
            livePuzzleMap = new PuzzleMap().GenerateLivePuzzleMap();
            CheckPuzzle();

            // temp
            TrackerManager.Instance.GetTracker<ObjectTracker>().Start();
            button1 = false;
        }
    }

    private void InitialisePuzzleMap()
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
        }
        else
        {
            // Insert failure
            Debug.Log("Puzzle does not match");
        }
    }

}
