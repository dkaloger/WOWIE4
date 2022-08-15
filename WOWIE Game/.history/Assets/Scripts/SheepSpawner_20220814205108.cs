using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    [SerializeField] private GameObject sheep;

    public Transform player;
    private Vector2 currentPos;
    private float timer;
    private int localcount;
    private int[] zonesCount = new int[]{0,0,0};
    private float[][] zoneBounds = {new float[]{-12,15.4f,-7.8f,50}, new float[]{18.4f,60.3f,-7.8f, 6.2f},new float[]{60.3f, 75.5f, -7.8,16.9}};

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn",0f,5f);
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    private void Spawn(){
        currentPos = player.GetComponent<Rigidbody2D>().position;
        int i;
        if(currentPos.x < 15.4){
            i = 0;
        }
        else if(currentPos.x < 60.3){
            i = 1;
        }
        else if(currentPos.x < 75.5){
            i = 2;
        }
        if(zonesCount[i]<10){
            GameObject sh = Instantiate(sheep, 3,3);
            Vector2 screenPos = Camera.main.WorldToScreenPoint(sh.transform.position);
            while(sh.position )
            zonesCount++;
        }
    }
}
