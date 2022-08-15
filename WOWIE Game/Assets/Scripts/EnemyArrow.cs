using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindWithTag("Enemy") != null)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            transform.right = GameObject.FindWithTag("Enemy").transform.position - transform.position;
            //  transform.LookAt(GameObject.FindWithTag("Enemy").transform,new Vector3(0,1,0));
        }
        else
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        }
        
       // transform.rotation = 
    }
}
