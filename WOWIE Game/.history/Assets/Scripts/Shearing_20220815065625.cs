using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shearing : MonoBehaviour
{
    [SerializeField] private Sprite shearedsheep;
    [SerializeField] private Sprite fullSheep;
    [SerializeField] private GameObject wool;
    
    private float t = 0;
    private float cooldown;
    private bool startTimer = false;
    public bool dead = false;
    private float t2 = 0;
    private Vector2 savedPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }
     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Ore") && gameObject.GetComponent<SpriteRenderer>().sprite != shearedsheep && !dead)
        {
            collision.transform.parent.parent.GetComponent<PlayerController>().Helditem = null;
            Destroy(collision.gameObject);
            gameObject.GetComponent<SpriteRenderer>().sprite = shearedsheep;
            Instantiate(wool, new Vector2(collision.transform.parent.parent.GetComponent<Transform>().position.x, collision.transform.parent.parent.GetComponent<Transform>().position.y),Quaternion.identity);
            startTimer = true;
        }
       
    }
    // Update is called once per frame
    void Update()
    {
        if(dead){
            if(t2 == 0){
                GetComponent<BoxCollider2D>().isTrigger = true;
                gameObject.GetComponent<SpriteRenderer>().sprite = shearedsheep;
                gameObject.tag = "Holdable";
                savedPos = new Vector2(transform.position.x, transform.position.y);
            }
            t2 += Time.deltaTime*1f;
            if(t >= 60){
                Instantiate()
            }
        }
        if(startTimer){
            t+=Time.deltaTime*1f;
            if(t >= 60){
                t = 0;
                gameObject.GetComponent<SpriteRenderer>().sprite = fullSheep;
                startTimer = false;
            }
        }
        
    }
}
