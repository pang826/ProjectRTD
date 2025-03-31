using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANode
{
    public bool isWalkAble;
    public Vector3 worldPos;
    public int gridX;
    public int gridY;
    public ANode(bool isWalkAble, Vector3 worldPos, int gridX, int gridY)
    {
        this.isWalkAble = isWalkAble;
        this.worldPos = worldPos;
        this.gridX = gridX;
        this.gridY = gridY;
    }
}
