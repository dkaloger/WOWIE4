using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shearing : MonoBehaviour
{
    [SerializeField] private Sprite shearedsheep;
    [SerializeField] private Sprite fullSheep;
    [SerializeField] private GameObject wool;
    private float t;
    private float cooldown;
    private bool startTimer = false;
    public bool dead = false;
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
            GetComponent<BoxCollider2D>().isTrigger = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = shearedsheep;
            gameObject.tag = "Holdable";
        }
        if(startTimer){
            t+=Time.deltaTime*1f;
            if(t >= 60){
                t = 0;
                gameObject.GetComponent<SpriteRenderer>().sprite = fullSheep;
                startTimer = false;
            }
        }

        int i = -1;
        if (transform.position.x < 15.4)
        {
            i = 0;
        }
        else if (transform.position.x < 60.3)
        {
            i = 1;
        }
        else if (transform.position.x < 85)
        {
            i = 2;
        }
        Vector2 screenPosition = GetComponent<Camera>().WorldToScreenPoint(transform.position);
        if (!(screenPosition.y > Screen.height || screenPosition.y < 0 || screenPosition.x > Screen.width || screenPosition.x < 0))
        {
            seen = true;
            Debug.Log("Seen!");
        }
        else
        {
            if (seen)
            {
                Destroy(gameObject);
                SheepSpawner.indivCount[i]++;
                Sheepseen = false;
            }
        }
        if(SheepSpawner.indivCount[i] == 4){
            SheepSpawner.indivCount[i] = 0;
            SheepSpawner.zonesCount[i]--;
        }
        
    }
}
