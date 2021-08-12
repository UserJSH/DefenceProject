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
        // 1. �� ���忡�� ���� ����
        GameObject enemy = Instantiate(Mob);
        // 2. �� ��ġ�� ������ ���� �ʹ�.
        enemy.transform.position = MobSpawn[index].transform.position;
        // 3. �� ����� ��ġ ��Ű�� �ʹ�.
        enemy.transform.rotation = MobSpawn[index].transform.rotation;

        Portal.SetActive(false);
        anim.SetTrigger("PortalOff");
        trigger = false;
    }

    IEnumerator PortalInstantiate(int index)
    {

        Portal.SetActive(true);
        //GameObject portal = Instantiate(Portal);
        // 2. �� ��ġ�� ������ ���� �ʹ�.
        Portal.transform.position = MobSpawn[index].transform.position;
        // 3. �� ����� ��ġ ��Ű�� �ʹ�.
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
