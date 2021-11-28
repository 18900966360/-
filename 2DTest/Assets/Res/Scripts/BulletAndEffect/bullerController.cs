using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CapsuleCollider2D))]
public class bullerController : MonoBehaviour 
{
    float timer = 0;
    public int Type ;
    public int Atk;
    private CapsuleCollider2D CC;
    // Start is called before the first frame update
    void Awake ()
    {
        CC = GetComponent<CapsuleCollider2D>();
    }
    void Start()
    {
        Physics2D.queriesStartInColliders= false;
        CC.direction = 0 ;
        
        // CC.size = this.GetComponent<RectTransform>().sizeDelta;
        // CC.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 3)
        {
            PoolManager.GetInstacne().PushObj(this.gameObject.name,this.gameObject);
            timer = 0;
        }
    }
    // void OnTriggerEnter(Collider other){
    //     Debug.Log("OnTriggerEnter");
    // }
    void OnCollisionEnter2D( Collision2D collisionInfo ){
        Debug.Log(Type+"Type-------------------");
        if (collisionInfo.transform.gameObject.layer==8 || (collisionInfo.transform.gameObject.layer==9 && Type.ToString()!=collisionInfo.transform.gameObject.tag)){
            PoolManager.GetInstacne().PushObj(this.gameObject.name,this.gameObject);
            timer = 0;
            if(collisionInfo.transform.gameObject.layer == 9){
                //DOTO:根据TYPE扣血
                collisionInfo.gameObject.GetComponent<PlayerProperty>().Hp -= Atk;
            }
            Debug.Log(collisionInfo.gameObject.name);
        }
    }
}
