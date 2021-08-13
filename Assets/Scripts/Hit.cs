using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �¾ �� imageHit�� ������ �ʰ� �ϰ�ʹ�.
// ���� �÷��̾ �������� �� ImageHit�� �����̰� �ϰ�ʹ�.
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
        // �¾ �� ImageHit�� ������ �ʰ� �ϰ� �ʹ�.
        imageHit.SetActive(false);
        
    }

    public void Hiting()
    {
        // �����Ÿ��� �ʹ�.
        StartCoroutine("IeHit");
    }

    IEnumerator IeHit()
    {
        // 1. imageHit�� ���̰� �ϰ� �ʹ�.
        imageHit.SetActive(true);
        // 2. 0.1�� �Ŀ�
        yield return new WaitForSeconds(0.1f);
        // 3. imageHit�� �� ���̰� �ϰ� �ʹ�.
        imageHit.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
