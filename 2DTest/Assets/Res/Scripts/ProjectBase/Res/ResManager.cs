using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 资源加载模块
/// 异步加载
/// </summary>
public class ResManager : SingletonBase<ResManager>
{
    //同步加载资源
    public T  Load<T>(string name) where T:Object 
    {
        T res = Resources.Load<T>(name);
        //如果对象是一个GameObject类型的 我把它实例化以后 再返回出去 外部直接使用就行
        if (res is GameObject)
        {
          return  GameObject.Instantiate(res);
        }
        else
        {
            return res;
        }
        
    }

    //异步加载资源
    public void LoadAsync<T>(string name ,UnityAction<T> callBack,Transform transform = null) where T : Object
    {
        //开启异步加载的协程
        MonoManager.GetInstacne().StartCoroutine(ReallyLoadAsync(name,callBack,transform));
    }

    //真正的协同程序函数，用于开启异步加载对应的资源
    private IEnumerator ReallyLoadAsync<T>(string name, UnityAction<T> callBack,Transform transform = null) where T:Object
    {
        ResourceRequest o = Resources.LoadAsync<T>(name);
        yield return o;

        //Object.ResourceRequest.asset是正在加载的对象
        //判断加载的对象是不是GameObject
        if (o.asset is GameObject)
        {
            //委托
            callBack(GameObject.Instantiate(o.asset,transform.position,(o.asset as GameObject).transform.rotation) as T);
        }
        else
        {
            callBack(o.asset as T);
        }
    }
}
