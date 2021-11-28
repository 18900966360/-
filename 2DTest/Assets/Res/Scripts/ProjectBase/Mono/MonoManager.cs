using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 1.可以提供给外部添加帧更新事件的方法
/// 2.可以提供给外部添加协程的方法
/// </summary>
public class MonoManager : SingletonBase<MonoManager>
{
    public MonoController controller;
    public MonoManager()
    {
        //保证了MonoController对象的唯一性
        GameObject obj = new GameObject("MonoController");
        controller= obj.AddComponent<MonoController>();
    }
    public void DestoryONload(GameObject gameObject)
    {
        controller.DontDistoryOnload(gameObject);
    }
    /// <summary>
    /// 给外部提供的，添加帧更新事件的函数
    /// </summary>
    /// <param name="fun"></param>
    public void AddUpdataListenter(UnityAction fun)
    {
        controller.AddUpdataListentr(fun);
    }
    /// <summary>
    /// 给外部提供的，移除帧更新事件的函数
    /// </summary>
    /// <param name="fun"></param>
    public void RemoveUpdataListentr(UnityAction fun)
    {
        controller.RemoveUpdataListentr(fun);
    
    }

    public Coroutine StartCoroutine(string methodName) 
    {
      return  controller.StartCoroutine(methodName);
    }
    public Coroutine StartCoroutine(IEnumerator routine) 
    {
        return controller.StartCoroutine(routine);
    }
    public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value) 
    {
        return controller.StartCoroutine(methodName, value);
    }
    public Coroutine StartCoroutine_Auto(IEnumerator routine)
    {
        return controller.StartCoroutine(routine);
    }


}
