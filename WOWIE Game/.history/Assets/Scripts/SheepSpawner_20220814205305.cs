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
    private float[][] zoneBounds = {new float[]{-12,15.4f,-7.8f,50}, new float[]{18.4f,60.3f,-7.8f, 6.2f},new float[]{60.3f, 75.5f, -7.8f,16.9f}};

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
        int i = -1;
        if(currentPos.x < 15.4){
            i = 0;
        }
        else if(currentPos.x < 60.3){
            i = 1;
        }
        else if(currentPos.x < 75.5){
            i = 2;
        }
        if(zonesCount[i]<10 && i > -1){
            GameObject sh = Instantiate(sheep, new Vector2(3,3), Quaternion.identity);
            Vector2 screenPos = Camera.main.WorldToScreenPoint(sh.transform.position);
            //while(sh.position )
            zonesCount[i]++;
        }
    }
}
