using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    GameObject[] target;
    private Animator anim;
    private Text t;

    public float speed = 0.05f; //�̵��ӵ�
    private int index; //���� ��ġ
    private float attackDistance = 0.6f; //���ݰ��� ����
    float currentTime = 0; // �����ð�
    float attackDelay = 2f; // ���� ������
    float takeDamageDelay = 0.2f; //�� �ǰݽ� ������
    public int attackPower = 1; // ���ݷ�
    public int enemyHp = 2; // ü��

    public enum State
    {
        Move,
        Attack,
        TakeDamage,
        Die,
    }

    State state;
    Player player;
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

    public IEnumerator EnemyTakeDamage(int Damage)
    {
        state = State.TakeDamage;
        anim.SetBool("Walk Forward", false);

        enemyHp -= Damage;
        
        if(enemyHp <= 0)
        {
            anim.SetTrigger("Die");
            Destroy(gameObject, 2f);
        }
        else
        {
            anim.SetTrigger("Take Damage");
        }     

        yield return new WaitForSeconds(takeDamageDelay);

        state = State.Move;
    }


}
