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


    // PathRequestManager������ ���� ��ã�� ��û�� �����ϴ� �޼���
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
                // openList �߿��� F Cost�� ���� ū ��带 ã��.
                // F Cost�� ���ٸ� H Cost�� ���� ��带 ã��.
                for (int i = 1; i < openList.Count; i++)
                {
                    if (openList[i].FCost < currentNode.FCost || openList[i].FCost == currentNode.FCost && openList[i].hCost < currentNode.FCost)
                    {
                        currentNode = openList[i];
                    }
                }
                // Ž���� ���� OpenList���� �����ϰ� CloseList�� �߰��Ѵ�
                openList.Remove(currentNode);
                closeList.Add(currentNode);
                // Ž���� ��尡 ��ǥ ����� Ž�� ����
                if (currentNode == targetNode)
                {
                    pathSuccess = true;
                    break;
                }
                // ��� Ž��(�̿� ���)
                foreach (ANode node in aGrid.GetNeighbor(currentNode))
                {
                    // �̵��Ұ� ����̰ų� closeList�� ���ԵǾ� �ִ� ����� ��� ��ŵ
                    if (node.isWalkAble == false || closeList.Contains(node))
                        continue;
                    // �̿� ������ G�ڽ�Ʈ�� H�ڽ�Ʈ�� ����Ͽ� openList�� �߰�
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
        // ������ WorldPosition�� ���� WayPoints�� �������θ� �Ŵ����Լ����� �˷��ش�
        pathRequestManager.FinishedProcessingPath(wayPoints, pathSuccess);
    }
    // Ž�� ���� �� ���� ����� ParentNode�� �����Ͽ� ����Ʈ�� ��� �޼���
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
    // Path ����Ʈ�� �ִ� ������ WorldPosition�� Vector3[] �迭�� ��� ����
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
    // �� ��尣�� �Ÿ��� Cost�� ����ϴ� �޼���
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
