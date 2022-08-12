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
    float progress;
    public float RockHardness;
   public GameObject Ore;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
       // if (Input.GetKeyDown(KeyCode.Space))
       // {
       if(playerController.Helditem != null) { 
            if (playerController.Helditem.name == "Pickaxe"&& TLMain.GetTile(TLMain.layoutGrid.WorldToCell(playerController.Helditem.transform.position)) == Rock)
            {
                progress += Time.deltaTime;
                if(progress > RockHardness)
                {
                    progress = 0;
                    TLMain.SetTile(TLMain.layoutGrid.WorldToCell(playerController.Helditem.transform.position), tileNull);
                    Instantiate(Ore, playerController.Helditem.transform.position, playerController.Helditem.transform.rotation);
                }
               


           }
        }
    }
}
