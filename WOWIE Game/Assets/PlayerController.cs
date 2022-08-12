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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
   
    // Update is called once per frame
    void Update()
    {
        //movement
        if(Mathf.Abs(rb.velocity.x)+Mathf.Abs( rb.velocity.y) < maxspeed)
        {
            rb.AddForce(Input.GetAxis("Vertical") * transform.up * speed *Time.deltaTime);
            rb.AddForce(Input.GetAxis("Horizontal") * transform.right * speed * Time.deltaTime);
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
                        item.transform.localPosition = HoldPosition;
                        Helditem = item;
                        break;
                    }
                }
            }
            else if(Helditem != null)
            {
                Helditem.transform.parent = null;               
                Helditem = null;                
            }
            
        }

       
    }

}
