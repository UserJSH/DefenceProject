using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 태어날 때 imageHit를 보이지 않게 하고싶다.
// 적이 플레이어를 공격했을 때 ImageHit를 깜빡이게 하고싶다.
public class Hit : MonoBehaviour
{
    public static Hit instance;

    private void Awake()
    {
        instance = this;

        //DontDestroyOnLoad(gameObject);
    }
    public GameObject imageHit;
    // Start is called before the first frame update
    void Start()
    {
        // 태어날 때 ImageHit를 보이지 않게 하고 싶다.
        imageHit.SetActive(false);
        
    }

    public void Hiting()
    {
        // 깜빡거리고 싶다.
        StartCoroutine("IeHit");
    }

    IEnumerator IeHit()
    {
        // 1. imageHit를 보이게 하고 싶다.
        imageHit.SetActive(true);
        // 2. 0.1초 후에
        yield return new WaitForSeconds(0.1f);
        // 3. imageHit를 안 보이게 하고 싶다.
        imageHit.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
