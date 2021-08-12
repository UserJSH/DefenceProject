using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private Text t;
    [SerializeField] private GameObject ui;
    
    public static float Score;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ScoreUp()
    {
        Score += Time.deltaTime;
    }
    public void ScoreReturn()
    {
        ui.GetComponent<Score>().GetScore(Score);
    }
    public void PlayerTakeDamage(int index)
    {
        HP.Hp.maxHP--;

        if (HP.Hp.maxHP <= 0)
        {
            //SceneManager.LoadScene("GameOver");
            ui.SetActive(true);
            ScoreReturn();
        }
        else
        {          
            Hit.instance.Hiting();          
        }        
    }

    private void Start()
    {
        
    }
    private void Update()
    {
        t.text = (Math.Truncate(Score * 10) / 10).ToString();
    }
}
