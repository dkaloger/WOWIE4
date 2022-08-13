using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Mining : MonoBehaviour
{
    PlayerController playerController;
    public Tilemap TLMain;
    public Tile Rock;
    Tile tileNull;
    public float progress;
    public float RockHardness;
   public GameObject Ore;
    public Animation anim;
    bool Active;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Active = false;
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        
        if (playerController.Helditem != null) {
            if (playerController.Helditem.GetComponent<AudioSource>() != null)
            {
                playerController.Helditem.GetComponent<AudioSource>().enabled = Active;
            }
            if (playerController.Helditem.name == "Pickaxe"&& TLMain.GetTile(TLMain.layoutGrid.WorldToCell(playerController.Helditem.transform.position)) == Rock)
            {
                Active=true;

               
                progress += Time.deltaTime;
                if(progress > RockHardness)
                {
                    progress = 0;
                    TLMain.SetTile(TLMain.layoutGrid.WorldToCell(playerController.Helditem.transform.position), tileNull);
                    Instantiate(Ore,new Vector3(0.5f,0.5f,0)+ TLMain.layoutGrid.CellToWorld( TLMain.layoutGrid.WorldToCell(playerController.Helditem.transform.position)), playerController.Helditem.transform.rotation);
                }
               


           }
           if(!anim.isPlaying && Active)
            {
                anim.Play();
            }
            if (Active ==false)
            {
                progress = 0;
            }
        }

        
        
    }
}
