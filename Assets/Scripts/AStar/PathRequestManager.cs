using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// ������Ʈ�� ��θ� ��û�ϰ� ��λ��� ������ ��ġ�� �޾Ƽ� ������Ʈ���� �̵��� �����϶�� �ݹ��Լ��� �����ִ� Ŭ����
public class PathRequestManager : MonoBehaviour
{
    private Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();     // ���� ������Ʈ���� ��ã�� ��û�� ������ ���� ť�� ����
    private PathRequest curPathRequest;        // ���� ó���� ��ã�� ��û

    public static PathRequestManager Instance;
    private PathFinder pathFinder;

    private bool isProcessingPath;
    private void Awake()
    {
        pathFinder = GetComponent<PathFinder>();
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // ������Ʈ���� ��û�ϴ� �޼���
    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, UnityAction<Vector3[], bool> callback)
    {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
        Instance.pathRequestQueue.Enqueue(newRequest);
        Instance.TryProcessNext();
    }
    // ť ������� ��ã�� ��û�� ������ PathFinding �˰��� ���� �޼���
    private void TryProcessNext()
    {
        if (isProcessingPath == false && pathRequestQueue.Count > 0)
        {
            curPathRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            pathFinder.StartFindPath(curPathRequest.pathStart, curPathRequest.pathEnd);
        }
    }
    // ��ã�Ⱑ �Ϸ�� ��û�� ó���ϰ� ������Ʈ���� �̵����۸�� �ݹ��Լ��� �����ϴ� �޼���
    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        curPathRequest.callback(path, success);
        isProcessingPath = false;
        TryProcessNext();
    }
    // ������Ʈ���� ��ã�� ��û������ ���� ����ü
    struct PathRequest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public UnityAction<Vector3[], bool> callback;

        public PathRequest(Vector3 nStart, Vector3 nEnd, UnityAction<Vector3[], bool> nCallback)
        {
            pathStart = nStart;
            pathEnd = nEnd;
            callback = nCallback;
        }
    }
}
