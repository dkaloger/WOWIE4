using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    public float score;
    public float Timer;
    public float penaltyTime; 
    public TextMeshProUGUI ScoreCounter;
    public TextMeshProUGUI TimeCounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if(Timer> penaltyTime)
        {
            score -= Time.deltaTime;
        }
        TimeCounter.text = Mathf.RoundToInt(Timer).ToString() + "s /"+ Mathf.RoundToInt(penaltyTime).ToString()+"s";
        ScoreCounter.text ="Score:"+Mathf.RoundToInt( score).ToString();

        
    }
}
