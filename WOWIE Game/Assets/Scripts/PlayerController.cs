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
    public float idlethreshold = 0.1f;
    public float sensitivity = 1000;
    public Vector2 movement;
    public Vector3 adjustedmovement;

    public bool Line17;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.y) < maxspeed)
        {
            rb.AddForce(transform.up * (Input.GetAxis("Vertical") * speed));
            rb.AddForce(transform.right * (Input.GetAxis("Horizontal") * speed));
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Helditem != null)
        {

            anim.SetBool("AI Picked", Helditem.name.Contains("The AI"));
            anim.SetBool("Carrying ore", Helditem.name.Contains("Ore"));
            anim.SetBool("carrying artwork", Helditem.name.Contains("Painting"));


        }
        else
        {
            anim.SetBool("AI Picked", false);
            anim.SetBool("Carrying ore", false);
            anim.SetBool("carrying artwork", false);

        }


        if (Mathf.Abs(Input.GetAxis("Horizontal")) - 0.2f > 0 || Mathf.Abs(Input.GetAxis("Vertical")) - 0.2f > 0)
        {
            movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical");
        }

        if (Mathf.Abs(Input.GetAxis("Horizontal")) - 0.2f > 0 ||
            Mathf.Abs(Input.GetAxis("Vertical")) - 0.2f > 0 && anim.GetBool("HoldingGun"))
        {
            movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical");
        }
        // adjustedmovement.x =Mathf.Clamp( movement.normalized  ;


        //   anim.SetBool("Imovementdle", true);


        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        if (Helditem != null && Helditem.name.Contains("Wool"))
        {
            anim.SetBool("Carrying artwork", true);
            if (GetComponent<SpriteRenderer>().sprite.name.Contains("back"))
            {
                Helditem.GetComponent<Transform>().position =
                    new Vector2(transform.position.x, transform.position.y - 0.2f);
                Helditem.GetComponent<SpriteRenderer>().sortingOrder = -1;
            }
            else
            {
                Helditem.GetComponent<SpriteRenderer>().sortingOrder = 1;
                if (GetComponent<SpriteRenderer>().sprite.name.Contains("forward"))
                    Helditem.GetComponent<Transform>().position =
                        new Vector2(transform.position.x, transform.position.y - 0.3f);
                if (GetComponent<SpriteRenderer>().sprite.name.Contains("right"))
                    Helditem.GetComponent<Transform>().position =
                        new Vector2(transform.position.x + 0.13f, transform.position.y - 0.2f);
                if (GetComponent<SpriteRenderer>().sprite.name.Contains("left"))
                    Helditem.GetComponent<Transform>().position =
                        new Vector2(transform.position.x - 0.13f, transform.position.y - 0.2f);
            }

        }




        // if (Mathf.Abs(Input.GetAxis("Vertical")) < idlethreshold && Mathf.Abs(Input.GetAxis("Horizontal"))<idlethreshold)
        // {
        //anim.SetBool("Idle", true);
        //}
        //  anim.SetBool("Idle", false);
        //movement

        SprintT += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            if (SprintCooldown < SprintT)
            {
                rb.AddForce(movement * speed * SprintMultiplier);

                SprintT = 0;
            }

        }

        //Hold object
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var prevHeldItem = Helditem;
            //drop
            if (Helditem != null && !Helditem.name.Contains("Painting"))
            {

                if (Helditem.name.Contains("The AI"))
                {
                    Helditem.GetComponent<BoxCollider2D>().enabled = true;

                }

                if (Helditem.GetComponent<SpriteRenderer>() != null)
                {
                    Helditem.GetComponent<SpriteRenderer>().enabled = true;
                }

                Helditem.transform.parent = null;
                Helditem.transform.position =
                    transform.position + (Vector3)movement.normalized + new Vector3(0.0f, -0.5f, 0f);
                Helditem.transform.rotation = Quaternion.Euler(Vector3.zero);


                Helditem.transform.parent = null;

                var itemObjects = Helditem.GetComponents<IHeldItem>();

                if (itemObjects is { Length: > 0 })
                    foreach (var itemObject in itemObjects)
                        itemObject.Drop();

                Helditem = null;
            }
            
            if (Helditem == null)
            {
                foreach (var item in GameObject.FindGameObjectsWithTag("Holdable"))
                {
                    if (item == prevHeldItem) continue;
                    if (Vector2.Distance(
                            transform.position + (Vector3)movement.normalized + new Vector3(0.0f, -0.5f, 0f),
                            item.transform.position) < pickupRange)
                    {

                        item.transform.parent = transform.GetChild(0);

                        Helditem = item;

                        if (Helditem.GetComponent<SpriteRenderer>() != null)
                        {
                            if (!Helditem.name.Contains("Wool"))
                                Helditem.GetComponent<SpriteRenderer>().enabled = false;
                            print("picked");
                        }

                        if (Helditem.name.Contains("The AI"))
                        {
                            if (GameObject.FindGameObjectWithTag("DialogManager").GetComponent<DialogManager>()
                                    .canmove == false && Line17 == false)
                            {
                                Line17 = true;
                                GameObject.FindGameObjectWithTag("DialogManager").GetComponent<DialogManager>()
                                    .canmove = true;
                            }

                            Helditem.GetComponent<BoxCollider2D>().enabled = false;
                        }


                        var itemObjects = Helditem.GetComponents<IHeldItem>();

                        if (itemObjects is { Length: > 0 })
                            foreach (var itemObject in itemObjects)
                                itemObject.Pickup();

                        break;
                    }
                }
            }

           

        }
    }
}
