using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    GameObject[] target;
    private Animator anim;
    private Text t;

    public float speed = 0.05f; //이동속도
    private int index; //생성 위치
    public float attackDistance = 0.25f; //공격가능 범위
    float currentTime = 0; // 누적시간
    float attackDelay = 2f; // 공격 딜레이
    public int attackPower = 3; // 공격력
    public int hp = 15; // 체력

    public enum State
    {
        Move,
        Attack,
        Die,
    }

    public State state;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectsWithTag("Player");
        index = Random.Range(0, target.Length);
        state = State.Move;
        t = GameObject.Find("Text1").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        float di = Vector3.Distance(transform.position, target[index].transform.position);
        t.text = di.ToString();
        switch (state)
        {
            case State.Move:
                EnemyMove(target, index);
                break;
            case State.Attack:
                Attack();
                break;

        }
    }

    private void EnemyMove(GameObject[] target, int index)
    {
        

        // 공격범위 밖이라면
        if (Vector3.Distance(transform.position, target[index].transform.position) > attackDistance)
        {
            var direction = target[index].transform.position - transform.position; // 목적지 - 현재위치 = 방향
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);
            transform.position = Vector3.Lerp(transform.position, target[index].transform.position, Time.deltaTime * speed);

            //Vector3 dir = target[index].transform.position - transform.position;
            //dir.Normalize();
            //transform.position += dir * speed * Time.deltaTime;

            anim.SetBool("Walk Forward", true);
        }
        // 만일, 플레이어가 공격 범위 이내에 있다면 플레이어를 공격상태로 전환
        else
        {
            state = State.Attack;
            currentTime = attackDelay;
        }
    }

    private void Attack()
    {
        anim.SetBool("Walk Forward", false);
        // 일정한 시간마다 플레이어를 공격한다.
        currentTime += Time.deltaTime;
        if (currentTime > attackDelay)
        {
            var motion = Random.Range(1, 2);
            anim.SetTrigger("Attack 01");
            //target.GetComponent<PlayerMove>().DamageAction(attackPower);
            currentTime = 0;
        }

    }

}
