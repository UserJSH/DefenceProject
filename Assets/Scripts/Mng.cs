using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mng : MonoBehaviour
{
    public static Mng I = null;

    public int count;

    [SerializeField] private Text t;

    private void Awake()
    {
        if(I == null)
        {
            I = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        count = 0;
    }

    private void Update()
    {
        t.text = count.ToString();
    }
}
