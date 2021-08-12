using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Text t;

    public void GetScore(float score)
    {
        t.text = (Math.Truncate(score * 10) / 10).ToString();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {     
        
    }
}
