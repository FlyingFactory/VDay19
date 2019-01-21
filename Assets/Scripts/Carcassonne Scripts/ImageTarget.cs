﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { North, East, South, West };

public struct ImageTarget : IEquatable<ImageTarget>
{
    private static readonly float TARGET_SIZE = 5.0f;
    public string targetId;
    public Direction currentDirection;
    public TargetPosition currentTargetPosition;

    public ImageTarget(string targetId)
    {
        this.targetId = targetId;
        this.currentDirection = Direction.North;
        this.currentTargetPosition = new TargetPosition(0, 0);
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
        { Debug.Log("equals"); return true; }
        else return false;
    }

}
