using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    private Ray ray;
    // Start is called before the first frame update
    void Start()
    {
        ray = GameObject.Find("AR Session Origin").GetComponent<Ray>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton()
    {
        //this.gameObject.SetActive(false);
        //ray.enabled = true;
        SceneManager.LoadScene("Game");
    }
}
