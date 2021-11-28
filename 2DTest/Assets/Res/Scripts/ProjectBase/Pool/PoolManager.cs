using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 抽屉数据，池子中的一列容器
/// </summary>
public class PoolData
{
    //抽屉中对象挂载的父节点
    public GameObject fatherObj;
    //对象的容器 
    public List<GameObject> poolList;
    public PoolData(GameObject obj, GameObject poolObj)
    {
        //创建抽屉fatherObj，并且让Pool（衣柜）作为他的父物体
        fatherObj = new GameObject(obj.name);
        fatherObj.transform.parent = poolObj.transform;
        poolList = new List<GameObject>();
        PushObj(obj);

    }
    /// <summary>
    /// 往抽屉里面压东西
    /// </summary>
    /// <param name="obj"></param>
    public void PushObj(GameObject obj)
    {
        //存起来
        poolList.Add(obj);
        //设置父对象
        obj.transform.SetParent(fatherObj.transform);
        //失活
        obj.SetActive(false);
    }
    /// <summary>
    /// 从抽屉里面取东西
    /// </summary>
    /// <returns></returns>
    public GameObject GetObj()
    {
        
        GameObject obj = null;
        //取出第一个
        obj = poolList[0];
        poolList.RemoveAt(0);
        obj.SetActive(true);
        obj.transform.SetParent(null);
        return obj;
    }

}


/// <summary>
/// 对象池模块
/// </summary>
public class PoolManager : SingletonBase<PoolManager>
{
    //对象池容器（衣柜）
    public Dictionary<string, PoolData> PoolDic = new Dictionary<string, PoolData>();
    private GameObject poolObj;
   
    

    /// <summary>
    /// 往外拿东西
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public void GetObj(string name,UnityAction<GameObject> callBack,Transform transform=null) //Palyer1,2
    {
       
        //有抽屉，并且抽屉里有东西
        if (PoolDic.ContainsKey(name)&&PoolDic[name].poolList.Count>0)
        {
                callBack(PoolDic[name].GetObj());
                // if (PoolDic[name].GetObj().GetComponent<bullerController>()!=null)
                // {
                //     Debug.Log(Player+"Player-------------------");
                //     PoolDic[name].GetObj().GetComponent<bullerController>().Type = Player;
                // }
        }
        else
        {
            //通过异步加载资源，创建对象给外面用
            ResManager.GetInstacne().LoadAsync<GameObject>(name, (o) =>
            {
                o.name = name;
                
                
                callBack(o);
            },transform);
            //obj = GameObject.Instantiate(Resources.Load<GameObject>(name));
           // obj.name = name;
        }

    

    }

  

    
    /// <summary>
    /// 还东西
    /// </summary>
    /// <param name="name"></param>
    /// <param name="obj"></param>
    public void PushObj(string name, GameObject obj) 
    {

        if (poolObj==null)
        {
            poolObj = new GameObject("Pool");

        }
        else
        {
            MonoManager.GetInstacne().DestoryONload(poolObj);
        }
   
        
        //里面有抽屉
        if (PoolDic.ContainsKey(name))
        {
            PoolDic[name].PushObj(obj);
        }
        //里面没有抽屉
        else
        {
            PoolDic.Add(name, new PoolData(obj,poolObj));
            
        }
    }
    /// <summary>
    /// 清空对象池的方法
    /// 主要用在过场景的时候
    /// </summary>
    public void Clear() 
    {
        PoolDic.Clear();
        poolObj = null;
    }
}
