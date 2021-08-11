using System.Collections;
using UnityEngine;

public class EnemyMng : MonoBehaviour
{
    [SerializeField] private Transform[] MobSpawn;
    [SerializeField] private GameObject[] Portal;
    [SerializeField] private GameObject Mob;
    private Animator anim;

    private float currTime = 0f;
    public float createTime = 3f;

    public int maxCount = 6;
    private int index;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        index = Random.Range(0, MobSpawn.Length);
    }

    // Update is called once per frame
    void Update()
    {

        if (Mng.I.count < maxCount)
        {
            StartCoroutine(EnemyGenerator(index));
        }

    }

    

    private void EnemyInstantiate(int index)
    {
        // 1. 적 공장에서 적을 생성
        GameObject enemy = Instantiate(Mob);
        // 2. 내 위치에 가져다 놓고 싶다.
        enemy.transform.position = MobSpawn[index].transform.position;
        // 3. 내 방향과 일치 시키고 싶다.
        enemy.transform.rotation = MobSpawn[index].transform.rotation;

        Mng.I.count++;
    }

    IEnumerator PortalInstantiate(int index)
    {
        Portal[index].SetActive(true);
        anim.SetBool("Portal", true);

        EnemyInstantiate(index);

        yield return new WaitForSeconds(1.5f);

        anim.SetBool("Portal", false);
    }

    IEnumerator EnemyGenerator(int index)
    {
        yield return new WaitForSeconds(createTime);
        PortalInstantiate(index);
    }

}
