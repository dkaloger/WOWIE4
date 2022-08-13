using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldableObject : MonoBehaviour
{
    public int stackSize;
    public bool move;
    public float movespeed = 0.01f;
    public Vector3 holdposition = new Vector3(0.6f,0.0f,0.0f);
    // Start is called before the first frame update
    void Start()
    {
        
    }
  
   
    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.localPosition, holdposition) > 0.01f&&transform.parent !=null)
        {
            if(transform.parent.name == "Pivot")
            {
                move = true;
            }
            
        }
        if (move)
        {
            //transform.localPosition = (Vector3)Vector2.MoveTowards(transform.position, GameObject.Find("Pivot").transform.position+ holdposition, movespeed*Time.deltaTime);
            transform.localPosition = (Vector3)Vector2.MoveTowards(transform.localPosition, holdposition, movespeed);
        }
       
        move = false;
    }
}
