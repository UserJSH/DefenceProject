using System.Collections;
using UnityEngine;

public class EnemyMng : MonoBehaviour
{
    [SerializeField] private Transform[] MobSpawn;
    [SerializeField] private GameObject Portal;
    [SerializeField] private GameObject Mob;
    [SerializeField] private Animator anim;

    public float createTime = 5f;

    public int maxCount = 6;
    private int index;
    private bool trigger = false;


    // Start is called before the first frame update
    void Start()
    {

        //anim = Portal.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger) return;

        if (Mng.I.count >= maxCount)
        {
            return;
        }

        Mng.I.count++;
        trigger = true;
        index = Random.Range(0, MobSpawn.Length);
        StartCoroutine(PortalInstantiate(index));

    }

    private void EnemyInstantiate(int index)
    {
        // 1. 적 공장에서 적을 생성
        GameObject enemy = Instantiate(Mob);
        // 2. 내 위치에 가져다 놓고 싶다.
        enemy.transform.position = MobSpawn[index].transform.position;
        // 3. 내 방향과 일치 시키고 싶다.
        enemy.transform.rotation = MobSpawn[index].transform.rotation;

        Portal.SetActive(false);
        anim.SetTrigger("PortalOff");
        trigger = false;
    }

    IEnumerator PortalInstantiate(int index)
    {

        Portal.SetActive(true);
        //GameObject portal = Instantiate(Portal);
        // 2. 내 위치에 가져다 놓고 싶다.
        Portal.transform.position = MobSpawn[index].transform.position;
        // 3. 내 방향과 일치 시키고 싶다.
        Portal.transform.rotation = MobSpawn[index].transform.rotation;
        //Portal.transform.localScale = new Vector3(1f, 1f, 0.5f);

        anim.SetTrigger("PortalOn");

        yield return new WaitForSeconds(2f);
        EnemyInstantiate(index);

    }

    IEnumerator EnemyGenerator(int index)
    {
        yield return new WaitForSeconds(createTime);
        StartCoroutine(PortalInstantiate(index));

    }

}
