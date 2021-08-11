using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    int hp;
    public int maxHP = 3;
    public Slider sliderHP;

    public int HP
    {
        get { return hp; }
        set
        {
            hp = value;
            sliderHP.value = value;
        }
    }

    public void PlayerTakeDamage(int index)
    {
        hp -= -1;
    }

    // Start is called before the first frame update
    void Start()
    {
        sliderHP.maxValue = maxHP;
        HP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
