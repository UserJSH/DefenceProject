using System.Collections;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] private Transform[] tm;
    [SerializeField] private GameObject[] tp;
    [SerializeField] private GameObject t;
    [SerializeField] private Animator a;

    //private float currTime = 0f;
    public float cre = 3f;

    public int max = 6;
    private int i;

    private void OnGUI()
    {
        if (GUILayout.Button("test"))
        {
            if (Mng.I.count < max)
            {
                StartCoroutine(por(0));
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<Animator>();
        i = Random.Range(0, tm.Length);
    }

    // Update is called once per frame
    void Update()
    {

        /*if (Mng.I.count < max)
        {
            StartCoroutine(gene(i));
        }*/

    }



    private void ene(int index)
    {
        // 1. 적 공장에서 적을 생성
        GameObject enemy = Instantiate(t);
        // 2. 내 위치에 가져다 놓고 싶다.
        enemy.transform.position = tm[index].transform.position;
        // 3. 내 방향과 일치 시키고 싶다.
        enemy.transform.rotation = tm[index].transform.rotation;

        Mng.I.count++;
    }

    IEnumerator por(int index)
    {
        tp[index].SetActive(true);


        yield return new WaitForSeconds(2f);
        //ene(index);
        a.SetBool("Portal", false);
    }

    IEnumerator gene(int index)
    {
        yield return new WaitForSeconds(cre);
        por(index);
    }

}
