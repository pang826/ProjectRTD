using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;
    [SerializeField] private List<GameObject> obj = new List<GameObject>();
    [SerializeField] private Dictionary<E_PoolType, GameObject> dict = new Dictionary<E_PoolType, GameObject>();
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (Instance != this)
                Destroy(gameObject);
        }

        for(int i = 1; i <= obj.Count; i++) 
        {
            dict.Add((E_PoolType)i, obj[i - 1]);
            Queue<GameObject> que = new Queue<GameObject>();
            que.Enqueue(obj[i - 1]);
        }
    }

    public GameObject GetObject(E_PoolType type, Transform transform)
    {
        GameObject obj = Instantiate(dict[type], transform);
        obj.transform.parent = this.transform;
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void ReturnObject(E_PoolType type, GameObject obj) 
    {

    }
}
