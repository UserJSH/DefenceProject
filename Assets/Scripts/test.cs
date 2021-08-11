using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private Animator anim;

    float ct = 0;
    float et = 0.3f;
    private void OnGUI()
    {
        if (GUILayout.Button("test"))
        {
            StartCoroutine(EnemyTakeDamage());
        }
    }
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
        state = State.Move;
    }

    // Update is called once per frame
    void Update()
    {
        ct += Time.deltaTime;
        switch (state)
        {
            case State.Move:
                EnemyMove();
                break;
            case State.TakeDamage:
                break;


        }
    }

    private void EnemyMove()
    {
        anim.SetBool("Walk Forward", true);

    }
    public IEnumerator EnemyTakeDamage()
    {
        state = State.TakeDamage;
        anim.SetBool("Walk Forward", false);
        anim.SetTrigger("Take Damage");


        yield return new WaitForSeconds(et);

        state = State.Move;
    }

}
