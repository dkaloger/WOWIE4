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
            transform.LookAt(GameObject.FindWithTag("Enemy").transform);
        }
        else
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        }
        
       // transform.rotation = 
    }
}
