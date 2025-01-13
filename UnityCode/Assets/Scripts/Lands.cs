using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lands : MonoBehaviour
{
    public GameObject land;
    public float speed = 10f;
    public float startPosZ = -12f; 
    List<GameObject> lands;



    // Start is called before the first frame update
    void Start()
    {
        lands = new List<GameObject>();
        //Éú³É10¸ö
        for (int i = 0; i < 10; i++) { 
            GameObject newLand = Instantiate(land) as GameObject;
            newLand.transform.localPosition = new Vector3(0,0,startPosZ + i * 24); 
            newLand.transform.SetParent(this.transform);
            lands.Add(newLand);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
