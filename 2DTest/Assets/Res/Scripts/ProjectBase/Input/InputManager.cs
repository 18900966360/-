using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 输出控制模块
/// </summary>
public class InputManager : SingletonBase<InputManager>
{
    private bool isStart = false; //默认关闭输入检测
    /// <summary>
    /// 构造函数中添加Update监听
    /// </summary>
    public  InputManager() 
    {
        MonoManager.GetInstacne().AddUpdataListenter(MyUpdate);
    }

    /// <summary>
    /// 石头开启或者关闭我的输入检测 比如过剧情需要不让你动 按了不好使，变成不检测
    /// </summary>
    /// <param name="isOpen"></param>
    public   void StartOrEndCheck(bool isOpen ) 
    {
        isStart = isOpen;
    }


    private  void checkKeyCode(KeyCode key) 
    {
        if (Input.GetKeyDown(key))
        {
            //事件中心模块，分发按下抬起事件
            EventCenter.GetInstacne().EventTrigger<KeyCode>("某键按下", key);
        }
        if (Input.GetKeyUp(key))
        {
            //事件中心模块，分发按下抬起事件
            EventCenter.GetInstacne().EventTrigger<KeyCode>("某键抬起", key);
        }
    }
    private void MyUpdate()
    {
        //没有开启输入检测 就不去检测直接return;
        if (!isStart)
        {
            return;
        }
        checkKeyCode(KeyCode.W);
        checkKeyCode(KeyCode.A);
        checkKeyCode(KeyCode.S);
        checkKeyCode(KeyCode.D);
    }
}
