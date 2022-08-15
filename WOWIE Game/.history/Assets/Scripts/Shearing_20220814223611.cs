using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shearing : MonoBehaviour
{
    [SerializeField] private Image shearedsheep;
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
            

        }
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
