using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
   // public float turnrate;
    public int speed;
    public float maxspeed;
    public 
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
        //Hold object
        if (Input.GetKeyDown(KeyCode.Space)){
            foreach (var item in GameObject.FindGameObjectsWithTag("Holdable"))
            {
               // if (item) 
            }
            
        }

       
    }

}
