using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.gameObject.layer == 9)
        {
                //DOTO:根据TYPE扣血
            other.gameObject.GetComponent<PlayerProperty>().Hp = 0;
            Debug.Log(other.gameObject.name);
        }
    }
}
