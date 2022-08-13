using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DeliveryPoint : MonoBehaviour
{
    public int PaintingValue;
    public Sprite completedEasel;
    public bool completed;
    public GameObject paintingdisplay;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.name.Contains("Painting")&& !completed)
        {
            GetComponent<SpriteRenderer>().sprite = completedEasel;
            paintingdisplay.SetActive(true);
            paintingdisplay.GetComponent<SpriteRenderer>().sprite = collision.GetComponent<Image>().sprite;
            GameObject.FindGameObjectWithTag("Score Manager").GetComponent<Score>().score += PaintingValue;
            GameObject.FindGameObjectWithTag("Score Manager").GetComponent<Score>().Timer = 0;
            GameObject.FindGameObjectWithTag("Score Manager").GetComponent<Score>().CompletedPaintings ++;  
            completed = true;
            Destroy(collision.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
