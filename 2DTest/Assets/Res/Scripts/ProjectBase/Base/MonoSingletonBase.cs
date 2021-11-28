using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingletonBase<T> : MonoBehaviour  where T:MonoBehaviour
{
    public  static T instance;
    
    public  void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
             
        }
        else
          {
              if (instance != this)
              {
                  Destroy(gameObject);
              }
          }
        DontDestroyOnLoad(gameObject);

    }
}
