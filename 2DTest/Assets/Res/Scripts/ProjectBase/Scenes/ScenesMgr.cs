using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// 场景切换模块
/// 异步加载一般都会和协程配合使用
/// </summary>
public  class ScenesMgr : SingletonBase<ScenesMgr>
{
   
   public   AsyncOperation ao;

    /// <summary>
    /// 切换场景 同步
    /// </summary>
    /// <param name="name"></param>
    public void LoadScene(int index,UnityAction fun) 
    {
        //场景同步加载
        SceneManager.LoadScene(index);
        //加载完成过后才会执行fun
        fun();
    }


    /// <summary>
    /// 提供给外部的异步加载的接口
    /// </summary>
    /// <param name="name"></param>
    /// <param name="fun"></param>
    public  void LoadSceneAsyn(int index ,UnityAction fun) 
    {
        //有协程就要开启，不能让他在外面开启，所以要在这个方法里开启。开启协程就要继承Monobehaviour 所以就要用到Mono模块
      MonoManager.GetInstacne(). StartCoroutine(ReallyLoadSceneAsyn(index, fun));
    }


    /// <summary>
    /// 协程异步加载场景
    /// </summary>
    /// <param name="name"></param>
    /// <param name="fun"></param>
    /// <returns></returns>
    private  IEnumerator ReallyLoadSceneAsyn(int index, UnityAction fun) 
    {
     
        ao = SceneManager.LoadSceneAsync(index);
       ao .allowSceneActivation = false;
        //可以得到场景加载的一个进度
        while (!ao .isDone)
        {
           EventCenter.GetInstacne().EventTrigger("Loading",ao.progress);
          
            if (ao .progress>=0.9f)
            {
                if (Input.anyKeyDown)
                {
                    ao.allowSceneActivation = true;
                }
               
              
            }
            yield return null;

        }
        
        //加载完成过后才会执行fun
        fun();
    }


}
