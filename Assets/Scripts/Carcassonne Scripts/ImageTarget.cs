using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { North, East, South, West };

public class ImageTarget : MonoBehaviour, IEquatable<ImageTarget>
{
    private static readonly float TARGET_SIZE = 5.0f;
    public string targetId;
    public Direction currentDirection;
    public TargetPosition currentTargetPosition;

    public ImageTarget(string targetId)
    {
        this.targetId = targetId;
    }

    public ImageTarget(string targetId, Direction currentDirection, int x_coord, int y_coord)
    {
        this.targetId = targetId;
        this.currentDirection = currentDirection;
        this.currentTargetPosition = new TargetPosition(x_coord, y_coord);
    }

    public bool Equals(ImageTarget otherTarget)
    {
        if (this.targetId == otherTarget.targetId
            && this.currentDirection == otherTarget.currentDirection
            && this.currentTargetPosition.xCoord == otherTarget.currentTargetPosition.xCoord
            && this.currentTargetPosition.zCoord == otherTarget.currentTargetPosition.zCoord)
            return true;
        else return false;
    }

    private void Start()
    {
        if (this.targetId == LivePuzzle.origin.targetId)
        {
            LivePuzzle.originObject = this.gameObject;
        }
    }

    private void Update()
    {
        float rotation_x = this.transform.eulerAngles.x;
        float rotation_y = this.transform.eulerAngles.y;
        float rotation_z = this.transform.eulerAngles.z;

        float rotation_xl = this.transform.localEulerAngles.x;
        float rotation_yl = this.transform.localEulerAngles.y;
        float rotation_zl = this.transform.localEulerAngles.z;

    }

    public void UpdateCurrentDirection()
    {
        float rotation = this.GetComponent<Transform>().eulerAngles.y;
        rotation %= 360.0f;

        if ((rotation <= 45.0f && rotation > -45.0f) || rotation <= -315.0f || rotation > 315.0f)
        {
            currentDirection = Direction.North;
        }
        else if (rotation <= 135.0f || rotation <= -225.0f)
        {
            currentDirection = Direction.East;
        }
        else if (rotation <= 225.0f || rotation <= -135.0f)
        {
            currentDirection = Direction.South;
        }
        else if (rotation <= 315.0f || rotation <= -45.0f)
        {
            currentDirection = Direction.West;
        }
    }

    public void SetTargetPosition()
    {
        float xdiff = this.transform.position.x;
        float zdiff = this.transform.position.z;
        Debug.Log("xdiff: " + xdiff);
        Debug.Log("zdiff: " + zdiff);

        currentTargetPosition.xCoord = Mathf.RoundToInt(xdiff/TARGET_SIZE);
        currentTargetPosition.zCoord = Mathf.RoundToInt(zdiff/TARGET_SIZE);

        Debug.Log("xcoord: " + currentTargetPosition.xCoord);
        Debug.Log("zcoord: " + currentTargetPosition.zCoord);

    }

}