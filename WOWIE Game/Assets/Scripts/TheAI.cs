using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheAI : MonoBehaviour
{
    public int StoredOre, RequiredOre;
    public GameObject Painting;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Ore"))
        {
            StoredOre++;
            collision.transform.parent.parent.GetComponent<PlayerController>().Helditem = null;
            Destroy(collision.gameObject);

        }
       
    }
    // Update is called once per frame
    void Update()
    {
        if(StoredOre >= RequiredOre)
        {
            Instantiate(Painting,transform.GetChild(0).transform.position, transform.GetChild(0).transform.rotation);
            StoredOre = 0;
        }
    }
}