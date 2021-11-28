using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using LitJson;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D Rb;
    private float Horizontal;
    Transform Shoot ;
    Transform thisG;
    public Transform Gameobj;
    [SerializeField]
    private float moveSpeed = 50;
    [SerializeField]
    private float jumpForce = 60;
    [SerializeField]
    private bool isGround = false;
    [SerializeField]
    private float gravityScale = 2;
    public Transform checkPoint;
    public LayerMask laymask;
    [SerializeField]
    private Vector2 checkBoxSize;
    private int jumpTimes = 0;

    private int Player = 0;
    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        thisG = GetComponent<Transform>();
        Shoot = transform.Find("shoot").GetComponent<Transform>();
        Physics2D.queriesStartInColliders= false;
        if (this.gameObject.CompareTag("1")){
            Player = 1;
        }else if(this.gameObject.CompareTag("2"))
        {
            Player = 2;
        }
    }
    private void Update()
    {
        if(Player == 1)
        {
            Horizontal = Input.GetAxis("Horizontal") * moveSpeed;
        }else if(Player == 2)
        {
            Horizontal = Input.GetAxis("Horizontal2") * moveSpeed;
        }
        
        Flip();
        CheckGround();
        Move();
        GunsConrtoller();
        
    }

    private void Move()
    {
        bool isPlayer1Jump ;
        if(Player == 1)
        {
            isPlayer1Jump = Input.GetKeyDown(KeyCode.K);

        }else 
        {
            isPlayer1Jump = Input.GetKeyDown(KeyCode.Keypad2);
        }
        if(isPlayer1Jump && isGround )
        {
            jumpTimes += 1;
            Rb.velocity = Vector2.up * jumpForce;        
        }
        Rb.velocity = new Vector2(Horizontal,Rb.velocity.y);
        if (Rb.velocity.y <= 0)
        {
            if (jumpTimes > 1)
                Rb.gravityScale = jumpTimes * 1.2f;
            else
                Rb.gravityScale = 1;
        }
        else
        {
            Rb.gravityScale = 1;
        }
    }
    private void CheckGround()
    {
        Collider2D collider = Physics2D.OverlapBox(checkPoint.position,checkBoxSize,0,laymask);
        if (collider!=null ) 
        {
            isGround=true;
            jumpTimes = 0;
        }
        else if(collider==null && jumpTimes>=2)
        {
            isGround=false;  
        }
            
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(checkPoint.position,checkBoxSize);
        Gizmos.color = Color.red;
    }
    private void Flip()
    {
        if(Horizontal > 0)
        {
            transform.eulerAngles = new Vector3(0,0,0);
        }
        else if(Horizontal < 0)
        {
            transform.eulerAngles = new Vector3(0,180,0);
        }
    }
    private void GunsConrtoller()
    {
        bool isPlayer1UP = true;
        bool isPlayer1Down = true;
        bool isPlayer1left = true;
        bool isPlayer1Right = true;

        if (Player == 1)
        {
            isPlayer1UP = Input.GetKey(KeyCode.W);
            isPlayer1Down = Input.GetKey(KeyCode.S);
            isPlayer1left = Input.GetKey(KeyCode.A);
            isPlayer1Right = Input.GetKey(KeyCode.D);
        }
        else if (Player == 2)
        {
            isPlayer1UP = Input.GetKey(KeyCode.UpArrow);
            isPlayer1Down = Input.GetKey(KeyCode.DownArrow);
            isPlayer1left = Input.GetKey(KeyCode.LeftArrow);
            isPlayer1Right = Input.GetKey(KeyCode.RightArrow);
        }
        if (isPlayer1UP)
        {
            if (isPlayer1left)
            {
                Shoot.transform.eulerAngles = new Vector3(0, 0, 135);
            }
            else if (isPlayer1Right)
            {
                Shoot.transform.eulerAngles = new Vector3(0, 0, 45);
            }
            else
            {
                Shoot.transform.eulerAngles = new Vector3(0, 0, 90);
            }
        }
        else if (isPlayer1Down)
        {
            if (isPlayer1left)
            {
                Shoot.transform.eulerAngles = new Vector3(0, 0, -135);
            }
            else if (isPlayer1Right)
            {
                Shoot.transform.eulerAngles = new Vector3(0, 0, -45);
            }
            else
            {
                Shoot.transform.eulerAngles = new Vector3(0, 0, -90);
            }
        }
        else
        {
            Shoot.transform.eulerAngles = transform.eulerAngles;
        }
    }

}
