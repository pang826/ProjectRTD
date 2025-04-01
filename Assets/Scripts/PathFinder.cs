using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    private AStarManager aStarManager;

    public Transform StartObj;
    public Transform TargetObj;

    private void Awake()
    {
        aStarManager = GetComponent<AStarManager>();
    }

    private void Update()
    {
        FindPath(StartObj.position, TargetObj.position);
    }

    private void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        ANode startNode = aStarManager.GetNodeFromWorldPoint(startPos);
        ANode targetNode = aStarManager.GetNodeFromWorldPoint(targetPos);

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
            if(currentNode == targetNode)
            {
                return;
            }
            // 계속 탐색(이웃 노드)
            foreach(ANode node in aStarManager.GetNeighbor(currentNode))
            {
                // 이동불가 노드이거나 closeList에 포함되어 있는 노드일 경우 스킵
                if (node.isWalkAble == false || closeList.Contains(node))
                    continue;
                // 이웃 노드들의 G코스트와 H코스트를 계산하여 openList에 추가
                int neighborCost = currentNode.gCost + GetDistanceCost(currentNode, node);
                if(neighborCost < node.gCost || openList.Contains(node) == false)
                {
                    node.gCost = neighborCost;
                    node.hCost = GetDistanceCost(node, targetNode);
                    node.parentNode = currentNode;

                    if(openList.Contains(node) == false)
                    {
                        openList.Add(node);
                    }
                }

            }
        }
    }
    // 탐색 종료 후 최종 노드의 ParentNode를 추적하여 리스트에 담는 메서드
    private void RetracePath(ANode startNode, ANode endNode)
    {
        List<ANode> path = new List<ANode>();
        ANode currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parentNode;
        }
        path.Reverse();
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
