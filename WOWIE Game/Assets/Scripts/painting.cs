using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class painting : MonoBehaviour
{
    public Sprite[] images;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = images[GameObject.FindGameObjectWithTag("Score Manager").GetComponent<Score>().CompletedPaintings+1];
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
