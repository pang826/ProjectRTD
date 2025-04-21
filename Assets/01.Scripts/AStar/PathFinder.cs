using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    private AGrid aGrid;
    private PathRequestManager pathRequestManager;

    private void Awake()
    {
        aGrid = GetComponent<AGrid>();
        pathRequestManager = GetComponent<PathRequestManager>();
    }


    // PathRequestManager에서의 현재 길찾기 요청을 시작하는 메서드
    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
    }

    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Vector3[] wayPoints = new Vector3[0];
        bool pathSuccess = false;

        ANode startNode = aGrid.GetNodeFromWorldPoint(startPos);
        ANode targetNode = aGrid.GetNodeFromWorldPoint(targetPos);

        if (startNode.isWalkAble && targetNode.isWalkAble)
        {
            List<ANode> openList = new List<ANode>();
            HashSet<ANode> closeList = new HashSet<ANode>();
            openList.Add(startNode);

            while (openList.Count > 0)
            {
                ANode currentNode = openList[0];
                // openList 중에서 F Cost가 가장 큰 노드를 찾음.
                // F Cost가 같다면 H Cost가 작은 노드를 찾음.
                for (int i = 1; i < openList.Count; i++)
                {
                    if (openList[i].FCost < currentNode.FCost || openList[i].FCost == currentNode.FCost && openList[i].hCost < currentNode.FCost)
                    {
                        currentNode = openList[i];
                    }
                }
                // 탐색된 노드는 OpenList에서 제거하고 CloseList에 추가한다
                openList.Remove(currentNode);
                closeList.Add(currentNode);
                // 탐색된 노드가 목표 노드라면 탐색 종료
                if (currentNode == targetNode)
                {
                    pathSuccess = true;
                    break;
                }
                // 계속 탐색(이웃 노드)
                foreach (ANode node in aGrid.GetNeighbor(currentNode))
                {
                    // 이동불가 노드이거나 closeList에 포함되어 있는 노드일 경우 스킵
                    if (node.isWalkAble == false || closeList.Contains(node))
                        continue;
                    // 이웃 노드들의 G코스트와 H코스트를 계산하여 openList에 추가
                    int neighborCost = currentNode.gCost + GetDistanceCost(currentNode, node);
                    if (neighborCost < node.gCost || openList.Contains(node) == false)
                    {
                        node.gCost = neighborCost;
                        node.hCost = GetDistanceCost(node, targetNode);
                        node.parentNode = currentNode;

                        if (openList.Contains(node) == false)
                        {
                            openList.Add(node);
                        }
                    }
                }
            }
        }
        yield return null;
        if (pathSuccess)
        {
            wayPoints = RetracePath(startNode, targetNode);
        }
        // 노드들의 WorldPosition을 담은 WayPoints와 성공여부를 매니저함수에게 알려준다
        pathRequestManager.FinishedProcessingPath(wayPoints, pathSuccess);
    }
    // 탐색 종료 후 최종 노드의 ParentNode를 추적하여 리스트에 담는 메서드
    private Vector3[] RetracePath(ANode startNode, ANode endNode)
    {
        List<ANode> path = new List<ANode>();
        ANode currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parentNode;
        }
        Vector3[] wayPoints = SimPlifyPath(path);
        Array.Reverse(wayPoints);
        return wayPoints;
    }
    // Path 리스트에 있는 노드들의 WorldPosition을 Vector3[] 배열에 담아 리턴
    Vector3[] SimPlifyPath(List<ANode> path)
    {
        List<Vector3> wayPoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if(directionNew != directionOld)
            {
                wayPoints.Add(path[i].worldPos);
            }
            directionOld = directionNew;
        }
        return wayPoints.ToArray();
    }
    // 두 노드간의 거리로 Cost를 계산하는 메서드
    private int GetDistanceCost(ANode nodeA, ANode nodeB)
    {
        int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distX > distY)
        {
            return 14 * distY + 10 * (distX - distY);
        }
        return 14 * distX + 10 * (distY - distX);
    }
}
