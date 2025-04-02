using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// 오브젝트가 경로를 요청하고 경로상의 노드들의 위치를 받아서 오브젝트에게 이동을 시작하라는 콜백함수를 던져주는 클래스
public class PathRequestManager : MonoBehaviour
{
    private Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();     // 여러 오브젝트들의 길찾기 요청을 순서에 따라 큐에 담음
    private PathRequest curPathRequest;        // 현재 처리할 길찾기 요청

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
    // 오브젝트들이 요청하는 메서드
    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, UnityAction<Vector3[], bool> callback)
    {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
        Instance.pathRequestQueue.Enqueue(newRequest);
        Instance.TryProcessNext();
    }
    // 큐 순서대로 길찾기 요청을 꺼내어 PathFinding 알고리즘 시작 메서드
    private void TryProcessNext()
    {
        if (isProcessingPath == false && pathRequestQueue.Count > 0)
        {
            curPathRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            pathFinder.StartFindPath(curPathRequest.pathStart, curPathRequest.pathEnd);
        }
    }
    // 길찾기가 완료된 요청을 처리하고 오브젝트에게 이동시작명령 콜백함수를 실행하는 메서드
    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        curPathRequest.callback(path, success);
        isProcessingPath = false;
        TryProcessNext();
    }
    // 오브젝트들의 길찾기 요청정보를 담을 구조체
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
