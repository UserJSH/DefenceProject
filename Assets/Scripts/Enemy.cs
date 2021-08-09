using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    GameObject[] target;

    public float speed = 0.05f;
    private int index;

    public enum State
    {
        Idle,
        Move,
        Attack,
        Die,
    }

    public State state;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectsWithTag("Player");
        index = Random.Range(0, target.Length);
        state = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove(target, index);
    }

    private void EnemyMove(GameObject[] target, int index)
    {
        //Vector3 dir = target[index].transform.position - transform.position;
        //dir.Normalize();
        //transform.position += dir * speed * Time.deltaTime;
        var direction = target[index].transform.position - transform.position; // 목적지 - 현재위치 = 방향
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
        transform.position = Vector3.Lerp(transform.position, target[index].transform.position, Time.deltaTime * speed);
        state = State.Move;
    }
}
