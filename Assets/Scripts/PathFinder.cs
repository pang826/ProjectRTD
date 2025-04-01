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
            if(currentNode == targetNode)
            {
                return;
            }
            // ��� Ž��(�̿� ���)
            foreach(ANode node in aStarManager.GetNeighbor(currentNode))
            {
                // �̵��Ұ� ����̰ų� closeList�� ���ԵǾ� �ִ� ����� ��� ��ŵ
                if (node.isWalkAble == false || closeList.Contains(node))
                    continue;
                // �̿� ������ G�ڽ�Ʈ�� H�ڽ�Ʈ�� ����Ͽ� openList�� �߰�
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
    // Ž�� ���� �� ���� ����� ParentNode�� �����Ͽ� ����Ʈ�� ��� �޼���
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
