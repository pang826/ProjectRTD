using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AGrid : MonoBehaviour
{
    [SerializeField] private LayerMask unWalkableMask;
    [SerializeField] private Vector2 worldSize;
    [SerializeField] private float radius;
    private ANode[,] grid;

    private float diameter;
    private int gridSizeX;
    private int gridSizeY;

    private void Awake()
    {
        diameter = radius * 2;
        gridSizeX = Mathf.RoundToInt(worldSize.x / diameter);
        gridSizeY = Mathf.RoundToInt(worldSize.y / diameter);
        CreateGrid();
    }

    private void CreateGrid()
    {
        grid = new ANode[gridSizeX, gridSizeY];
        Vector3 leftBottom = transform.position - Vector3.right * worldSize.x / 2 - Vector3.forward * worldSize.y / 2;
        Vector3 point;
        for(int i = 0; i < gridSizeX; i++) 
        {
            for(int j = 0; j < gridSizeY; j++)
            {
                point = leftBottom + Vector3.right * (i * diameter + radius) + Vector3.forward * (j *  diameter + radius);
                bool isWalkable = !(Physics.CheckSphere(point, radius, unWalkableMask));
                grid[i, j] = new ANode(isWalkable, point, i, j);
            }
        }
    }

    // 현재 노드의 주변 8방면 노트를 탐색하는 메서드
    public List<ANode> GetNeighbor(ANode node)
    {
        List<ANode> neighbors = new List<ANode>();
        for(int i = -1; i <= 1; i++)
        {
            for(int j = -1; j <= 1; j++)
            {
                if(i == 0 && j == 0)        // 자기 자신인 경우 스킵
                {
                    continue;
                }

                int checkX = node.gridX + i;
                int checkY = node.gridY + j;

                // X, Y 값이 Grid 범위 안에 있을 경우
                if(checkX >= 0 && checkY >= 0 && checkX < gridSizeX && checkY < gridSizeY)
                {
                    neighbors.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbors;
    }

    // 유니티의 WorldPosition으로부터 그리드 상의 노드를 찾는 함수
    public ANode GetNodeFromWorldPoint(Vector3 worldPos)
    {
        float percentX = (worldPos.x + worldSize.x / 2) / worldSize.x;
        float percentY = (worldPos.z + worldSize.y / 2) / worldSize.y;

        //Mathf.Clamp01 => 0과 1사이에 없다면 0과 1로 반환
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);   
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        
        return grid[x, y];

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(worldSize.x, 1, worldSize.y));
        if(grid != null)
        {
            foreach(ANode node in grid)
            {
                Gizmos.color = (node.isWalkAble) ? Color.white : Color.red;
                Gizmos.DrawCube(node.worldPos, Vector3.one * (diameter - 0.1f));
            }
        }
    }
}
