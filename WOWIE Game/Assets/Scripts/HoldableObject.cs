using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldableObject : MonoBehaviour
{
    public int stackSize;
    public bool move;
    public float movespeed;
    public Vector3 holdposition = new Vector3(0.6f,0.0f,0.0f);
    // Start is called before the first frame update
    void Start()
    {
        
    }
  
   
    // Update is called once per frame
    void Update()
    {
        move= false;
        if (move)
        {
            transform.position = (Vector3)Vector2.MoveTowards(transform.position, GameObject.Find("Pivot").transform.position+ holdposition, movespeed*Time.deltaTime);
        }
        if(Mathf.Equals(transform.position, GameObject.Find("Pivot").transform.position + holdposition))
        {
            move = false;
        }
    }
}
