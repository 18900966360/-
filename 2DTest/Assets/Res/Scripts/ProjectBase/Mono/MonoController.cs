using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Monod的管理者
/// </summary>
public class MonoController : MonoBehaviour
{
    private event UnityAction upDataEvent;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    void Update()
    {
        if (upDataEvent!=null)
        {
            upDataEvent();
        }
    }
    public void DontDistoryOnload(GameObject gameObject) 
    {
        DontDestroyOnLoad(gameObject);
    }
    /// <summary>
    /// 给外部提供的，添加帧更新事件的函数
    /// </summary>
    /// <param name="fun"></param>
    public void AddUpdataListentr(UnityAction fun) 
    {
        upDataEvent += fun;
    }
    /// <summary>
    /// 给外部提供的，移除帧更新事件的函数
    /// </summary>
    /// <param name="fun"></param>
    public void RemoveUpdataListentr(UnityAction fun)
    {
        upDataEvent -= fun;
    }
}
