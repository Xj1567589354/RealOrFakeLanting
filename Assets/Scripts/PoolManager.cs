using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }
    //通过传入类型找到对应的池子
    private Dictionary<Object, Queue<Object>> poolsDict;

    private void Awake()
    {
        Instance = this;
        poolsDict = new Dictionary<Object, Queue<Object>>();
    }
    /// <summary>
    /// 初始化某一个对象池
    /// </summary>
    /// <param name="prefab">指定预制体的类型</param>
    /// <param name="size">当前对应类型的对象池的长度</param>
    public void InitPool(Object prefab,int size)
    {
        if (poolsDict.ContainsKey(prefab))
        {
            return;
        }
        Queue<Object> queue = new Queue<Object>();
        for (int i = 0; i < size; i++)
        {
            Object go= Instantiate(prefab);
            CreateGameObjectAndSetActive(go,false);
            queue.Enqueue(go);
        }
        poolsDict[prefab] = queue;
    }

    private void CreateGameObjectAndSetActive(Object obj,bool active)
    {
        GameObject itemGo = null;
        if (obj is Component)
        {
            Component component = obj as Component;
            itemGo = component.gameObject;
        }
        else
        {
            itemGo = obj as GameObject;
        }
        itemGo.transform.SetParent(transform);
        itemGo.SetActive(active);
    }

    public T GetInstance<T>(Object prefab) where T : Object
    {
        Queue<Object> queue;
        if (poolsDict.TryGetValue(prefab,out queue))
        {
            Object obj;
            if (queue.Count>0)
            {
                obj= queue.Dequeue();
            }
            else
            {
                obj = Instantiate(prefab);
            }
            CreateGameObjectAndSetActive(obj,true);
            queue.Enqueue(obj);
            return obj as T;
        }
        Debug.LogError("还没有当前类型的资源池被实例化");
        return null;
    }
}
