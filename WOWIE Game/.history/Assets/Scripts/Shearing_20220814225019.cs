using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shearing : MonoBehaviour
{
    [SerializeField] private Sprite shearedsheep;
    [SerializeField] private Sprite fullSheep;
    [SerializeField] private GameObject wool;
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
            gameObject.GetComponent<SpriteRenderer>().sprite = shearedsheep;
            collision.transform.parent.parent.GetComponent<PlayerController>().Helditem = Initialize(Wool, new Vector2(collision.transform.parent.parent.GetComponent<Transform>().transform.x, ))

        }
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
