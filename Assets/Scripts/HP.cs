using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public static HP Hp = null;

    public int maxHP = 4;

    [SerializeField] private Slider sliderHP;

    private void Awake()
    {
        if (Hp == null)
        {
            Hp = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        sliderHP.maxValue = HP.Hp.maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        sliderHP.value = HP.Hp.maxHP;
    }
}
