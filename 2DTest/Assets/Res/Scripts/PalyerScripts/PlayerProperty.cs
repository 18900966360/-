using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProperty : MonoBehaviour
{
    public int Hp = 100;
    private Transform Born1;
    private Transform Born2;
    public Text Hptxt;
    string Pname = "";
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.CompareTag("1"))
        {
            Hptxt = GameObject.Find("Canvas/player1Hp").GetComponent<Text>();
            Born1 = GameObject.Find("born1").GetComponent<Transform>();
            Pname = "Palyer1 HP";
        }
        else if (gameObject.CompareTag("2"))
        {
            Hptxt = GameObject.Find("Canvas/player2Hp").GetComponent<Text>();
            Born2 = GameObject.Find("born2").GetComponent<Transform>();
            Pname = "Palyer2 HP";
        }
        
    }
    
    // Update is called once per frame
    void Update()
    {
        Hptxt.text = Pname + "：" + Hp.ToString();
        if (Hp<=0)
        {
            Invoke("born",2);
            //Destroy(this.gameObject)
            this.gameObject.SetActive(false);
        }
    }
    void born() {
        this.gameObject.SetActive(true);
        Hp = 100;
        if (gameObject.CompareTag("1")) {
            gameObject.transform.position = Born1.position;
        }
        else if (gameObject.CompareTag("2"))
        {
            gameObject.transform.position = Born2.position;
        }
    }
}
