using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUI : MonoBehaviour
{
    [SerializeField] private GameObject hitText;
    [SerializeField] private GameObject exText;
    // Start is called before the first frame update
    void Start()
    {
        hitText.SetActive(false);
        exText.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ExText()
    {
        exText.SetActive(false);
    }

    public void HitText()
    {
        StartCoroutine("StartText");
    }
    IEnumerator StartText()
    {
        hitText.SetActive(true);
        
        yield return new WaitForSeconds(1.5f);

        hitText.SetActive(false);
    }
}
