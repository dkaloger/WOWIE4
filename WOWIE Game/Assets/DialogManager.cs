using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public DialogueScriptableObject current;
    public DialogueScriptableObject[] Openingtutorial, AIHIt, PaintingCreated;
    public TextMeshProUGUI box;
    public string towrite;
    public float chardelay, endpagedelay;
    public int page;
    public bool canmove = true;
    public bool introcompleted = false;
    public AudioSource audiosrc;
    public bool coroutinerunning;
    
    // Start is called before the first frame update
    void Start()
    {
        towrite = current.Pages[page];
        StartCoroutine(WaitAndPrint(current.Pages[0]));

    }

    public void HIT()
    {
        if (box.text == towrite && coroutinerunning == false && introcompleted)
        {
            print(box.text + towrite);
            current = AIHIt[Random.Range(0, AIHIt.Length)];
            towrite = current.Pages[0];
            box.text = "";

            towrite = current.Pages[0];
            coroutinerunning = true;
            StartCoroutine(WaitAndPrint(towrite));
        }

    }

    public void paintingcreated()
    {
        if (box.text == towrite && introcompleted)
        {
            box.text = "";
            current = PaintingCreated[Random.Range(0, PaintingCreated.Length - 1)];
            towrite = current.Pages[0];
            StartCoroutine(WaitAndPrint(towrite));
        }
    }

    // Update is called once per frame
    void Update()
    {
        audiosrc.enabled = coroutinerunning && towrite.Contains("Box"); 
        if (page == 32 && !canmove)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length < 2)
            {
                canmove = true;
            }
        }

        if (Input.GetKey(KeyCode.Return))
        {
            chardelay = 0.00f;
            endpagedelay = 3f;
        }
        else
        {
            chardelay = 0.02f;
            endpagedelay = 1f;
        }

        if (34 == page)
        {
            introcompleted = true;
            box.text = "";
            towrite = "";
        }

        //intro only
        if (current.Pages.Count >= page && canmove && !introcompleted)
        {
            if (towrite != current.Pages[page])
            {
                box.text = "";
                towrite = current.Pages[page];
                StartCoroutine(WaitAndPrint(towrite));

            }
        }







    }

    private IEnumerator WaitAndPrint(string towriteIE)
    {

        foreach (char c in towriteIE)
        {
            // rn += c;
            box.text += c;
            coroutinerunning = true;
            if (chardelay > 0.01)
            {
                yield return new WaitForSeconds(chardelay);
            }

            if (!introcompleted && Input.GetKey(KeyCode.Return))
                break;

        }

        box.text = towriteIE;
        
        if (page == 30 && !introcompleted)
        {
           // introcompleted = true;
            GameObject.Find("Player").GetComponent<EnemySpawnner>().SpawnFirstWave();
        }
        coroutinerunning = false;
        yield return new WaitForSeconds(Input.GetKey(KeyCode.Return) && !introcompleted ? endpagedelay / 5f : endpagedelay);
        
        if (page == 17 || page == 21 || page == 31)
        {
            canmove = false;
        }

        page++;
    }
}