using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class painting : MonoBehaviour
{
    public Sprite[] images;
    public int delivered;
    public float fillamount;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().sprite = images[GameObject.FindGameObjectWithTag("Score Manager").GetComponent<Score>().CompletedPaintings+1];
        

    }

    // Update is called once per frame
    void Update()
    {
        if(fillamount< (float)delivered / 4.0f)
        {
            fillamount +=Time.deltaTime;
        }
        GetComponent<Image>().fillAmount = fillamount;

        if (delivered == 4)
        {
            
            tag = "Holdable";
           
        }
    }
}
