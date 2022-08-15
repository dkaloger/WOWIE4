using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shearing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Ore"))
        {
            collision.transform.parent.parent.GetComponent<PlayerController>().Helditem = null;
            Destroy(collision.gameObject);
            GetComponent<AudioSource>().Play();
            if (StoredWool == 1)
            {
                workedonpainting= Instantiate(Painting, transform.GetChild(0).transform.position, transform.GetChild(0).transform.rotation);
                workedonpainting.transform.parent = transform.GetChild(0);
               
            }
            workedonpainting.GetComponent<painting>().delivered++;

        }
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
