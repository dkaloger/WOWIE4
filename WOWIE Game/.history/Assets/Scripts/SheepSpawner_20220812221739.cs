using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    [SerializeField] private GameObject sheep;
    private float timer;
    private int max = 3;
    private int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(count < max) timer += Time.deltaTime;
        if(timer > 10 && count < max){
            count++;
            timer = 0;
            Vector3 pos = new Vector3()
            GameObject sh = 

        }
    }
}
