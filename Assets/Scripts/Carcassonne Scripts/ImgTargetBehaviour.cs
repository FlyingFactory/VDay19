using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImgTargetBehaviour : MonoBehaviour
{
    private static readonly float TARGET_SIZE = 5.0f;
    public string targetId;
    public ImageTarget thisImageTarget;
    public int ropeOrder;

    private void Start()
    {
        thisImageTarget = new ImageTarget(targetId, ropeOrder);

        if (thisImageTarget.targetId == LivePuzzle.origin.targetId)
        { 
            LivePuzzle.originObject = this.gameObject;
        }
    }

    public void UpdateCurrentDirection()
    {
        float rotation = this.GetComponent<Transform>().eulerAngles.y;
        rotation %= 360.0f;

        if ((rotation <= 45.0f && rotation > -45.0f) || rotation <= -315.0f || rotation > 315.0f)
        {
            thisImageTarget.currentDirection = Direction.North;
        }
        else if (rotation <= 135.0f || rotation <= -225.0f)
        {
            thisImageTarget.currentDirection = Direction.East;
        }
        else if (rotation <= 225.0f || rotation <= -135.0f)
        {
            thisImageTarget.currentDirection = Direction.South;
        }
        else if (rotation <= 315.0f || rotation <= -45.0f)
        {
            thisImageTarget.currentDirection = Direction.West;
        }
    }

    public void SetTargetPosition()
    {
        float xdiff = this.transform.position.x;
        float zdiff = this.transform.position.z;


        thisImageTarget.currentTargetPosition.xCoord = Mathf.RoundToInt(xdiff/TARGET_SIZE);
        thisImageTarget.currentTargetPosition.zCoord = Mathf.RoundToInt(zdiff/TARGET_SIZE);
        Debug.Log("target id: " + thisImageTarget.targetId);
        Debug.Log("x: " + this.thisImageTarget.currentTargetPosition.xCoord);
        Debug.Log("z: " + this.thisImageTarget.currentTargetPosition.zCoord);
        Debug.Log("direction" + this.thisImageTarget.currentDirection);
    }
}