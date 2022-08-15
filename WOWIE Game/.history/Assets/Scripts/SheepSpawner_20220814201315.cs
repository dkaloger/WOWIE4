using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    [SerializeField] private GameObject sheep;
    private float timer;
    private int zone;
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

    }
}
