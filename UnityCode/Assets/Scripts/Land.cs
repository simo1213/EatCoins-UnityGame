//Land
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Land : MonoBehaviour
{
    public float speed = 10F;
    public GameObject coin;
    List<GameObject> objects;

    // Use this for initialization
    void Start()
    {
        objects = new List<GameObject>();
    }
    //Update is called once per frame
    void Update()
    {
        if(speed!=0)
        {
             transform.position -= new Vector3(0,0,speed * Time.deltaTime);
            if (transform.position.z <= -12F)
            {
                clearObjects();//清除物体
                transform.position = new Vector3(0,0,24.0F);//更新位置  
                SetObjects();//生成物体
            } 
        }
    }

    void clearObjects(){
        foreach (GameObject g in objects) {
        Destroy(g);
        }
        objects.Clear();
    }

    
    void SetObjects()
    {
        float r = Random.Range(0f, 1f);

            for (int i = 0; i < 8; i++)
            {
                float r1 = Random.Range(0f, 1f);
                GameObject obj = null; // 初始化为 null

                float posX = 0f;
               if((r1 <= 0.7f)&&(r1>0.3f))// 40%是金币
                {
                    obj = Instantiate(coin) as GameObject;
                    int r2 = Random.Range(-1, 2); //-1 0 1随机数
                    posX = 0.8f * r2; // 设置金币位置
                    int n = Random.Range(-1, 1); //-1 0 随机数
                    if (obj != null)//设位置
                    {
                        if(n==-1)//金币滞空
                        {
                            obj.transform.localPosition = new Vector3(posX, 2f, 12.0f - i);
                        }
                        
                        else
                        {
                            obj.transform.localPosition = new Vector3(posX, 0.37f, 12.0f - i);
                        }
                        obj.transform.SetParent(this.transform);
                    }
                }
                
                objects.Add(obj);
            }
    }

    void StopMove(){
        //Debug.Log("stop");
        speed = 0f;
        Application.Quit(); // 结束游戏
    }
}
