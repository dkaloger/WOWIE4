using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Mining : MonoBehaviour
{
    PlayerController playerController;
    public Tilemap TLMain;

    Tile tileNull;
    public float progress;
    public float RockHardness;
   public GameObject Ore;

    bool Active;
    public Vector3 offset;
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
            
            print(transform.position + (Vector3)playerController.movement.normalized + new Vector3(0.0f, -0.5f, 0f));
            if (playerController.Helditem.name == "Pickaxe"&& TLMain.GetTile(TLMain.layoutGrid.WorldToCell(transform.position+(Vector3)playerController.movement.normalized+ offset))!= null)
            {
                if (TLMain.GetTile(TLMain.layoutGrid.WorldToCell(transform.position + (Vector3)playerController.movement.normalized + offset)).name.Contains("ore"))
                {


                    Active = true;


                    progress += Time.deltaTime;
                    if (progress > RockHardness)
                    {
                        progress = 0;
                        TLMain.SetTile(TLMain.layoutGrid.WorldToCell(transform.position + (Vector3)playerController.movement.normalized + offset), tileNull);
                        Instantiate(Ore, new Vector3(0.5f, 0.5f, 0) + TLMain.layoutGrid.CellToWorld(TLMain.layoutGrid.WorldToCell(transform.position + (Vector3)playerController.movement.normalized + offset)), Quaternion.Euler(Vector3.zero));
                    }


                }
           }
        
            if (Active ==false)
            {
                progress = 0;
            }
            if (playerController.Helditem.GetComponent<AudioSource>() != null)
            {
                playerController.Helditem.GetComponent<AudioSource>().enabled = Active;
            }
        }
        playerController.anim.SetBool("mining", Active);


    }
}
