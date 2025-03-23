using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;
    [SerializeField] private List<GameObject> obj = new List<GameObject>();
    [SerializeField] private Dictionary<E_PoolType, Queue<GameObject>> dict = new Dictionary<E_PoolType, Queue<GameObject>>();
    
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
            Queue<GameObject> que = new Queue<GameObject>();
            dict.Add((E_PoolType)i, que);
            for(int j = 0; j < 10; j++)
            {
                GameObject newObj = Instantiate(obj[i - 1], transform);
                newObj.SetActive(false);
                dict[(E_PoolType)i].Enqueue(newObj);
            }
        }
    }

    public GameObject GetObject(E_PoolType type, Transform transform)
    {
        GameObject getObj;
        if (dict[type].Count > 0)
        {
            getObj = dict[type].Dequeue();
        }
        else
        {
            getObj = Instantiate(this.obj[(int)type - 1]);
        }
        getObj.transform.parent = null;
        getObj.transform.position = transform.position;
        getObj.gameObject.SetActive(true);
        return getObj;
    }

    public void ReturnObject(E_PoolType type, GameObject obj) 
    {
        obj.transform.parent = transform;
        obj.gameObject.SetActive(false);
        dict[type].Enqueue(obj);
    }
}
