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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
   
    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Idle", true);

        anim.SetBool("MoveLeft", false);
        anim.SetBool("MoveRight", false);
        anim.SetBool("MoveUp", false);
        anim.SetBool("MoveDown", false);
        if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("MoveLeft", true);
            anim.SetBool("Idle", false);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("MoveRight", true);
            anim.SetBool("Idle", false);
        }
        else if(Input.GetKey(KeyCode.W)){
            anim.SetBool("MoveUp", true);
            anim.SetBool("Idle", false);

        }
        else if(Input.GetKey(KeyCode.S))
        {
            anim.SetBool("MoveDown", true);
            anim.SetBool("Idle", false);
        }
      
       


        
         if (Mathf.Abs(Input.GetAxis("Vertical")) < idlethreshold && Mathf.Abs(Input.GetAxis("Horizontal"))<idlethreshold)
          {
            anim.SetBool("Idle", true);
        }
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
                Helditem.transform.parent = null;               
                Helditem = null;                
            }
            
        }

       
    }

}
