using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaphonesController : MonoBehaviour
{
    protected const float cameraDistance = 1.5f; //
    protected float positionY = 0.2f; // Y�� ������
    [SerializeField] protected GameObject[] obj; // ������ ������Ʈ
    [SerializeField] private Animator BowAnim; //���� �ִϸ�����

    protected Camera mainCam; // ����ī�޶�
    private Player player;



    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        obj[0].SetActive(true);
        player = GameObject.Find("HP").GetComponent<Player>();

    }

    //    //�����ġ�� ������Ʈ�� ������ų�� position�� �޾ƿ�
    //    var pos = mainCam.ViewportToWorldPoint(new Vector3(0.45f, positionY, mainCam.nearClipPlane * cameraDistance));

    // Update is called once per frame
    void Update()
    {
        player.ScoreUp();
        //�հ����� ��ġ���� �� ������Ʈ�� ����
        if (Input.touchCount == 0) return;

        if (TouchHelper.IsDown)
        {
            WeaphonesShoot();
        }

    }


        //���� ������Ʈ�� ī�޶��� �ڽ�����
        //HoldingObj.transform.SetParent(mainCam.transform); // �ڽĵ��


    public void WeaphonesShoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit))
        {
            BowAnim.SetTrigger("Shoot");
            //if (hit.transform.tag == "Enemy")
            //{

                // ���� hitInfo�� Enemy������Ʈ�� ������ �ִٸ�?
                Enemy enemy = hit.transform.GetComponent<Enemy>();
                if (enemy != null)
                {
                    // enemy�� AddDamage �Լ��� ȣ���ϰ� �ʹ�.
                    enemy.EnemyTakeDamage(1);                  
                }
                
                //en.IfDestroyed();
                //Instantiate(smoke, hit.point, Quaternion.LookRotation(hit.normal));
            //}
        }
    }

}

