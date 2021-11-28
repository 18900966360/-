
using System.Collections.Generic;

using UnityEngine.Events;

public interface IEventInfo { }
public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> actions;
    public EventInfo(UnityAction<T> action) 
    {
        actions += action;
    }
}
public class EventInfo : IEventInfo
{
    public UnityAction actions;
    public EventInfo(UnityAction action)
    {
        actions += action;
    }
}
/// <summary>
/// 事件中心  （降低耦合度）
/// </summary>
public class EventCenter :SingletonBase<EventCenter>
{
    //key——事件的名字（比如：怪物死亡，玩家死亡，通关等等）
    //Value——监听这个事件，对应的委托函数们
    //object是所有对象的基类   （可能会有点装箱拆箱开销牺牲，但是只要传的不是值类型就不会有这个开销）
    private Dictionary<string, IEventInfo /*unity自带的委托*/> eventDic = new Dictionary<string, IEventInfo>();

    /// <summary>
    /// 添加事件监听  监听者调用
    /// </summary>
    /// <param name="name">事件的名字</param>
    /// <param name="action">用来处理事件的委托函数</param>
    public void AddEventListener<T>(string name, UnityAction<T> action)   
    {
        //有没有对应的事件监听
        //有的情况
        if (eventDic.ContainsKey(name))
        {
          ( eventDic[name] as EventInfo<T>).actions += action;
        }
        //没有的情况
        else
        {
            eventDic.Add(name,new EventInfo<T>(action));
        }
    }



    /// <summary>
    /// 监听不需要参数的事件
    /// </summary>
    /// <param name="name"></param>
    /// <param name="action"></param>
    public void AddEventListener(string name, UnityAction action) // 添加事件监听的重载
    {
        //有没有对应的事件监听
        //有的情况
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actions += action;
        }
        //没有的情况
        else
        {
            eventDic.Add(name, new EventInfo(action));
        }
    }


    /// <summary>
    /// 移除事件监听   监听者死亡或者消除时调用
    /// </summary>
    /// <param name="name">事件的名字</param>
    /// <param name="action">对应之前添加的委托函数</param>
    public void RemoveEventListener<T>(string name ,UnityAction<T> action) 
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo<T>).actions -= action;   
        }
    }


    /// <summary>
    /// 移除不需要参数的事件监听
    /// </summary>
    /// <param name="name"></param>
    /// <param name="action"></param>
    public void RemoveEventListener(string name, UnityAction action) //移除的重载
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actions -= action;
        }
    }


    /// <summary>
    /// 事件触发  
    /// </summary>
    /// <param name="name">哪一个名字的事件触发了</param>
    public void EventTrigger<T>(string name,T info)    
    {
        //有没有对应的事件监听
        //有的情况
        if (eventDic.ContainsKey(name))
        {
            //执行委托
            if ((eventDic[name] as EventInfo<T>).actions!=null)
            {
                (eventDic[name] as EventInfo<T>).actions.Invoke(info);
            }
        }
    }

    public void EventTrigger<T>(string name, T money,T exp)
    {
        //有没有对应的事件监听
        //有的情况
        if (eventDic.ContainsKey(name))
        {
            //执行委托
            if ((eventDic[name] as EventInfo<T>).actions != null)
            {
                (eventDic[name] as EventInfo<T>).actions.Invoke(money);
                (eventDic[name] as EventInfo<T>).actions.Invoke(exp);
                //(eventDic[name] as EventInfo<T>).actions.Invoke(equip);
            }



        }
    }
    /// <summary>
    /// 事件触发（不需要参数的）
    /// </summary>
    /// <param name="name"></param>
    public void EventTrigger(string name)  //触发的重载
    {
        //有没有对应的事件监听
        //有的情况
        if (eventDic.ContainsKey(name))
        {
            //执行委托
            if ((eventDic[name] as EventInfo).actions!=null)
            {
                (eventDic[name] as EventInfo).actions.Invoke();
            }
            


        }
    }



    /// <summary>
    /// 清空事件中心
    /// 主要用于场景切换时
    /// </summary>
    public void Clear() 
    {
        eventDic.Clear();
    }
}
