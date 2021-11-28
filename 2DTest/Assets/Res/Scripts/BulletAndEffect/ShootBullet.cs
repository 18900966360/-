using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
public class ShootBullet : MonoBehaviour
{
    private JsonData GunsData;
    private Transform Qk;
    private Transform MB;
    private Transform ThisT;
    private int Player = 0;
    private int WeaponsNum = 1001;

    void Awake()
    {
        Qk = transform.Find("QK").GetComponent<Transform>();
        MB = transform.Find("MB").GetComponent<Transform>();
        ThisT = GetComponent<Transform>();
        GunsData = GetJsonData.JsonFileToPerson("GunsConfig"); 
    }
    // Start is called before the first frame update
    void Start()
    {
         
         if(transform.root.CompareTag("1"))
         {
             Player = 1;
         }else if (transform.root.CompareTag("2"))
         {
             Player = 2;
         }

        ResManager.GetInstacne().LoadAsync<Sprite>((string)GunsData[WeaponsNum.ToString()]["GunPath"], (o) =>
        {
            transform.GetComponent<SpriteRenderer>().sprite = o;
        });

    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }
     private void Attack()
    {
        bool isPlayer1Shoot ;
        bool isChangeWeapons;
        if (Player == 1)
        {
            isPlayer1Shoot = Input.GetKeyDown(KeyCode.J);
            isChangeWeapons = Input.GetKeyDown(KeyCode.I);

        }
        else 
        {
            isPlayer1Shoot = Input.GetKeyDown(KeyCode.Keypad1);
            isChangeWeapons = Input.GetKeyDown(KeyCode.Keypad4);
        }
        if(isPlayer1Shoot)
        {
            PoolManager.GetInstacne().GetObj((string)GunsData[WeaponsNum.ToString()]["Bulletpath"],(o)=>{
                Rigidbody2D rb =o.transform.GetComponent<Rigidbody2D>();
                o.transform.position = Qk.position;
                o.name = (string)GunsData[WeaponsNum.ToString()]["BulletName"];
                int speed = (int)GunsData[WeaponsNum.ToString()]["Speed"];
                // if( ThisT.rotation.y == 0){
                //     rb.velocity = Vector2.right * 20;
                //     o.transform.eulerAngles = new Vector3(0,0,0);
                // }else {
                //     rb.velocity = -Vector2.right * 20;
                //     o.transform.eulerAngles = new Vector3(0,180,0);
                // }
                if (o.GetComponent<bullerController>()!=null)
                {
                    o.GetComponent<bullerController>().Type = Player;
                    o.GetComponent<bullerController>().Atk = (int)GunsData["1001"]["Atk"];
                }
                Vector2 direction = MB.position - Qk.position;
                o.transform.eulerAngles = ThisT.eulerAngles;
                rb.velocity =  direction * speed;
            },Qk);
        }
        if (isChangeWeapons) {
            WeaponsNum += 1;
            ResManager.GetInstacne().LoadAsync<Sprite>((string)GunsData[WeaponsNum.ToString()]["GunPath"], (o) =>
            {
                transform.GetComponent<SpriteRenderer>().sprite = o;
            });
        }
    }
}
