using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryPoint : MonoBehaviour
{
    public int PaintingValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.name.Contains("Painting"))
        {
            GameObject.FindGameObjectWithTag("Score Manager").GetComponent<Score>().score += PaintingValue;
            GameObject.FindGameObjectWithTag("Score Manager").GetComponent<Score>().Timer = 0;
            GameObject.FindGameObjectWithTag("Score Manager").GetComponent<Score>().CompletedPaintings ++;           
            Destroy(collision.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
