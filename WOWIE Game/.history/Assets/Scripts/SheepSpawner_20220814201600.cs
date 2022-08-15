using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    [SerializeField] private GameObject sheep;

    public Transform player;
    private float timer;
    private int localcount;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn",0f,5f);
    }

    // Update is called once per frame
    void Update()
    {
        if()
    }
    private void Spawn(){

    }
}
