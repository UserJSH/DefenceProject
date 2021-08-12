using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    GameObject[] target;
    private Player player;
    private Animator anim;


    public float speed = 0.05f; //�̵��ӵ�
    private int index; //���� ��ġ
    private float attackDistance = 0.6f; //���ݰ��� ����
    float currentTime = 0; // �����ð�
    float attackDelay = 2f; // ���� ������
    float takeDamageDelay = 0.5f; //�� �ǰݽ� ������
    public int attackPower = 1; // ���ݷ�
    public int enemyHp = 2; // ü��

    private bool trigger = false; //�ڷ�ƾ Ʈ����

    public enum State
    {
        Move,
        Attack,
        TakeDamage,
        Die,
    }

    State state;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectsWithTag("Player");
        index = Random.Range(0, target.Length);
        player = GameObject.Find("HP").GetComponent<Player>();
        state = State.Move;

    }

    // Update is called once per frame
    void Update()
    {
        //�Ÿ������� �÷��̾������
        //float di = Vector3.Distance(transform.position, target[index].transform.position);

        switch (state)
        {
            case State.Move:
                EnemyMove(target, index);
                break;
            case State.Attack:
                Attack();
                break;
            case State.TakeDamage:               
                break;
                

        }
    }

    private void EnemyMove(GameObject[] target, int index)
    {
        

        // ���ݹ��� ���̶��
        if (Vector3.Distance(transform.position, target[index].transform.position) > attackDistance)
        {
            var direction = target[index].transform.position - transform.position; // ������ - ������ġ = ����
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);
            transform.position = Vector3.Lerp(transform.position, target[index].transform.position, Time.deltaTime * speed);

            //Vector3 dir = target[index].transform.position - transform.position;
            //dir.Normalize();
            //transform.position += dir * speed * Time.deltaTime;

            anim.SetBool("Walk Forward", true);
        }
        // ����, �÷��̾ ���� ���� �̳��� �ִٸ� �÷��̾ ���ݻ��·� ��ȯ
        else
        {
            state = State.Attack;
            currentTime = attackDelay;
        }
    }

    private void Attack()
    {
        anim.SetBool("Walk Forward", false);
        // ������ �ð����� �÷��̾ �����Ѵ�.
        currentTime += Time.deltaTime;
        if (currentTime > attackDelay)
        {
            var motion = Random.Range(1, 2);
            anim.SetTrigger("Attack 0" + motion);
            player.PlayerTakeDamage(attackPower);
            //target.GetComponent<PlayerMove>().DamageAction(attackPower);
            currentTime = 0;
        }

    }

    public void EnemyTakeDamage(int Damage)
    {
        
        while(anim.GetBool("Walk Forward") || state != State.TakeDamage)
        {
            anim.SetBool("Walk Forward", false);
            state = State.TakeDamage;
        }        
        enemyHp -= Damage;

        if (trigger) return;

        trigger = true;
        if (enemyHp <= 0)
        {
            StartCoroutine(Die());
        }
        else
        {
            StartCoroutine(Take());
        }
    }

    IEnumerator Take()
    {
        anim.SetTrigger("Take Damage");

        yield return new WaitForSeconds(takeDamageDelay);

        state = State.Move;
        trigger = false;
        
    }
    IEnumerator Die()
    {
        anim.SetTrigger("Die");
        state = State.Die;

        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
        Mng.I.count--;
    }



}
