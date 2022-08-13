using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
   // public float turnrate;
    public int speed;
    public float maxspeed;
    public float pickupRange;
    public GameObject Helditem;
    public Vector3 HoldPosition;
    public int SprintCooldown, SprintMultiplier;
    public float SprintT;
    public Animator anim;
    public float idlethreshold=0.1f;
    public float sensitivity=1000;
    public Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
   
    // Update is called once per frame
    void Update()
    {
        
        if (Helditem != null )
        {
          if(  Helditem.name.Contains("Ore") || Helditem.name.Contains("The AI"))
            {
                Helditem.GetComponent<SpriteRenderer>().enabled = false;
                print("picked");
            }
            if(Helditem.name.Contains("The AI"))
            {
                Helditem.GetComponent <BoxCollider2D>().enabled = false;
            }
            anim.SetBool("AI Picked", Helditem.name.Contains("The AI"));
            anim.SetBool("Carrying ore", Helditem.name.Contains("Ore")); 
        }
        else
        {
            anim.SetBool("Carrying ore",false);

        }

            if (Mathf.Abs( Input.GetAxis("Horizontal")) -0.2f > 0 || Mathf.Abs(Input.GetAxis("Vertical")) - 0.2f > 0)
        {
            movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical");
        }
        //   anim.SetBool("Imovementdle", true);
       
       
        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);





        // if (Mathf.Abs(Input.GetAxis("Vertical")) < idlethreshold && Mathf.Abs(Input.GetAxis("Horizontal"))<idlethreshold)
        // {
        //anim.SetBool("Idle", true);
        //}
        //  anim.SetBool("Idle", false);
        //movement
        if (Mathf.Abs(rb.velocity.x)+Mathf.Abs( rb.velocity.y) < maxspeed)
        {
            rb.AddForce(transform.up * (Input.GetAxis("Vertical") * speed * Time.deltaTime));
            rb.AddForce(transform.right * (Input.GetAxis("Horizontal") * speed * Time.deltaTime));
        }
        SprintT += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift)|| Input.GetKeyDown(KeyCode.RightShift) )
        {
            if(SprintCooldown< SprintT)
            {
                rb.AddForce(transform.up * (Input.GetAxis("Vertical") * speed * Time.deltaTime*SprintMultiplier));
                rb.AddForce(transform.right * (Input.GetAxis("Horizontal") * speed * Time.deltaTime * SprintMultiplier));
                SprintT = 0;
            }
            
        }
        //Pivot

        var target = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.GetChild(0).position);
        target.z = 0;
        var dir = target;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.GetChild(0).rotation = Quaternion.Euler(0, 0, Quaternion.AngleAxis(angle, Vector3.forward).eulerAngles.z);
        //Hold object
        if (Input.GetKeyDown(KeyCode.Space)){
            if (Helditem == null)
            {
                foreach (var item in GameObject.FindGameObjectsWithTag("Holdable"))
                {
                    if (Vector2.Distance(transform.position, item.transform.position) < pickupRange)
                    {
                       
                        item.transform.parent = transform.GetChild(0);
                       // item.transform.localPosition = HoldPosition;
                        Helditem = item;
                        item.GetComponent<HoldableObject>().move = true;
                        Helditem.transform.localRotation =Quaternion.Euler( Vector3.zero);
                       // Helditem.transform.localPosition = HoldPosition;
                        var itemObject = Helditem.GetComponent<IHeldItem>();
                        
                        if (itemObject != null)
                            itemObject.Pickup();

                        break;
                    }
                }
            }
            else if(Helditem != null&& !Helditem.name.Contains("Painting"))
            {
                var itemObject = Helditem.GetComponent<IHeldItem>();
                if (itemObject != null)
                    itemObject.Drop();
                if (Helditem.name.Contains("The AI"))
                {
                    Helditem.GetComponent<BoxCollider2D>().enabled = true;
                    Helditem.GetComponent<SpriteRenderer>().enabled = true;
                    Helditem.transform.parent = null;
                    Helditem.transform.position = transform.position;
                    Helditem.transform.rotation = Quaternion.Euler(Vector3.zero);
                    anim.SetBool("AI Picked", false);
                }
                
                Helditem.transform.parent = null;
             
              
                Helditem = null;
              
            }
            
        }

       
    }

}
