using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private Text t;
    [SerializeField] private GameObject overUi;
    
    public static float Score;
    private bool trigger = false;

    public void ScoreUp()
    {
        Score += Time.deltaTime;
    }
    public void ScoreReturn()
    {
        if (trigger) return;

        trigger = true;
        overUi.GetComponent<Score>().GetScore(Score);
    }
    public void PlayerTakeDamage(int index)
    {
        StartCoroutine(WaitHit());
        HP.Hp.maxHP--;

        if (HP.Hp.maxHP <= 0)
        {
            //SceneManager.LoadScene("GameOver");
            overUi.SetActive(true);
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

    IEnumerator WaitHit()
    {
        yield return new WaitForSeconds(2.5f);
    }
}
