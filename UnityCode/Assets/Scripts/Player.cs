using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //设置轨道位置
    public float posLeftX = -0.8F;
    public float posRightX = 0.8F;
    public float changeColorTime = 3.0F;//小球颜色渐变时间
    public float jumpGravity = 50F;//小球重量
    public AudioClip getCoin;//吃金币音效
    public Text coinNumbersText; //分数
    int coinNumbers = 0;
    public Text GameOver;
    //计时
    public float gameTime = 30f;
    public Text timerText;
    //跳跃
    Rigidbody rg;                 
    bool canJump = true;     
    
    bool canControl = true;
    MeshRenderer render;          // change colorfloat
    float deltaTime = 0.0f;       // change color



    //Use this for initialization
    void Start()
    {
        render = this.transform.GetComponentInChildren<MeshRenderer>();
        rg = this.GetComponent<Rigidbody>();
    }
    //Update is called once per frame
    void Update()
    {
        //计时
        if(int.Parse(timerText.text) > 0)
        {
            timerText.text = ((int)(gameTime - Time.time)).ToString();
        }
        if (Time.time >= gameTime)
        {
            GameOver.text = "GAME OVER";
            StopGame();
        }

        //左右移动
        if(canControl)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                moveLeft();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                moveRight();
            }
        }

        //颜色渐变
        if (canControl)
        {
            if (deltaTime <= changeColorTime)
            {
                deltaTime += Time.deltaTime;
                render.material.color = Color.Lerp(Color.red, Color.yellow, deltaTime / changeColorTime);
            }
            else if (deltaTime <= 2 * changeColorTime)
            {
                deltaTime += Time.deltaTime;
                render.material.color = 
                    Color.Lerp(Color.yellow, Color.red, (deltaTime - changeColorTime) / changeColorTime);
            }
            else
            {
                deltaTime = 0.0f;
            }
        }
    }

    void moveLeft()
    {
        if (transform.position.x > 0)
        {
            transform.position = 
                new Vector3(0, transform.position.y, transform.position.z);
        }
        else if (transform.position.x == 0)
        {
            transform.position = 
                new Vector3(posLeftX, transform.position.y, transform.position.z);

        }
    }
    void moveRight()
    {
        if (transform.position.x < 0)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }
        else if (transform.position.x == 0)
        {
            transform.position = new Vector3(posRightX, transform.position.y, transform.position.z);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        switch (col.gameObject.tag)
        {
            case "Land":
                canJump = true;
                break;
            case "coin":
                Debug.Log("On_C_Coin");
                Destroy(col.gameObject);
                break;
          
        }

    }

    void OnTriggerEnter(Collider col)
    {
        switch (col.gameObject.tag)
        {
            case "coin":
                Debug.Log("ON_TCoin");
                AudioSource.PlayClipAtPoint(getCoin,this.transform.position);//音效
                Destroy(col.gameObject);
                coinNumbers= coinNumbers+1;
                coinNumbersText.text = coinNumbers.ToString();
                break;
        }
    }


    void FixedUpdate()//Unity自动检测触发，控制跳跃高度
    {
        //跳跃
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetButtonDown("Jump")) && canJump) {
            Debug.Log("jump");
            rg.velocity = Vector3.up * jumpGravity * Time.deltaTime;
           
            canJump = false;
        }
    }

    //游戏停止
    void StopGame()
    {
        GameObject.Find("Land1").SendMessage("StopMove");
        GameObject.Find("Land2").SendMessage("StopMove");
        GameObject.Find("Land3").SendMessage("StopMove");
        GameObject.Find("Land4").SendMessage("StopMove");
        GameObject.Find("Land5").SendMessage("StopMove");
        GameObject.Find("Land6").SendMessage("StopMove");
        GameObject.Find("Land7").SendMessage("StopMove");
        GameObject.Find("Land8").SendMessage("StopMove");
        GameObject.Find("Land9").SendMessage("StopMove");
        canControl = false;
        canJump = false; 
    }
}
