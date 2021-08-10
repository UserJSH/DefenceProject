using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMng : MonoBehaviour
{
    [SerializeField] private GameObject[] MobSpawn;
    [SerializeField] private GameObject Mob;

    private float currTime = 0f;
    public float createTime = 3f;

/*    int count;
    public int COUNT
    {
        get { return count; }
        set
        {
            count = value;
            if (count < 0)
            {
                count = 0;
            }
            if (count > maxCount)
            {
                count = maxCount;
            }
        }
    }*/
    public int maxCount = 6;
    private int index;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Mng.I.count < maxCount)
        {
            currTime += Time.deltaTime;

            if (currTime >= createTime)
            {
                index = Random.Range(0, MobSpawn.Length);
                // 1. 적 공장에서 적을 생성
                GameObject enemy = Instantiate(Mob);
                // 2. 내 위치에 가져다 놓고 싶다.
                enemy.transform.position = MobSpawn[index].transform.position;
                // 3. 내 방향과 일치 시키고 싶다.
                enemy.transform.rotation = MobSpawn[index].transform.rotation;

                currTime = 0f;
                Mng.I.count++;
            }

        }

    }

    /*private void EnemyInstantiate()
    {
        index = Random.Range(0, MobSpawn.Length);
        // 1. 적 공장에서 적을 생성
        GameObject enemy = Instantiate(Mob);
        // 2. 내 위치에 가져다 놓고 싶다.
        enemy.transform.position = MobSpawn[index].transform.position;
        // 3. 내 방향과 일치 시키고 싶다.
        enemy.transform.rotation = MobSpawn[index].transform.rotation;
    }*/

    /*IEnumerator EnemyGenerator()
    {
        for (int i = Mng.I.count; i < maxCount; i++)
        {
            yield return new WaitForSeconds(3f);
            EnemyInstantiate();
        }
    }*/

}
