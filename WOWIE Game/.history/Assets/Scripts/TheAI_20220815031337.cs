using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheAI : MonoBehaviour
{
    public int StoredWool, RequiredWool;
    public GameObject Painting;
    public GameObject workedonpainting;
    public bool line21;

    private DialogManager dialogManager;
    // Start is called before the first frame update
    void Start()
    {
        
        dialogManager = GameObject.FindGameObjectWithTag("DialogManager").GetComponent<DialogManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Wool"))
        {
            StoredWool++;
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
        if (collision.name.Contains("Sheep") && collision.GetComponent<Shearing>().dead){
            currentHealth = GetComponent<Health>().GetHealth()+75;
            GetComponent<Health>().SetHealth(currentHealth+75);
            if(GetComponent<Health>()._currentHealth > 300){
                GetComponent<Health>()._currentHealth = 300;
            }
        }
       
    }
    // Update is called once per frame
    void Update()
    {
        if(StoredWool >= RequiredWool)
        {
            if (dialogManager.canmove == false && line21 == false&&GameObject.Find("Player").GetComponent<PlayerController>().Line17 == true)
            {
                line21 = true;
                dialogManager.canmove = true;
            }

            dialogManager.paintingcreated();
            StoredWool = 0;
        }
    }
}
