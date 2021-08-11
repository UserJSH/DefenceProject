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
    protected GameObject HoldingObj; // ������ ���� ������Ʈ
    protected Vector3 InputPos; // input posision

    //public GameObject arCamera;


    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        obj[0].SetActive(true);
        GameObject.Find("Image").SetActive(true);
        //init();
    }

    //private void init()
    //{
    //    //�����ġ�� ������Ʈ�� ������ų�� position�� �޾ƿ�
    //    var pos = mainCam.ViewportToWorldPoint(new Vector3(0.45f, positionY, mainCam.nearClipPlane * cameraDistance));

    //    //����
    //    var index = UnityEngine.Random.Range(0, obj.Length);

    //    //������Ʈ ����
    //    var cube = Instantiate(obj[index], pos, Quaternion.identity, mainCam.transform);
    //    //RigidBody
    //    var rigid = cube.GetComponent<Rigidbody>();
    //    rigid.useGravity = false;
    //    rigid.velocity = Vector3.zero;
    //    rigid.angularVelocity = Vector3.zero;
    //}

    // Update is called once per frame
    void Update()
    {
        //�հ����� ��ġ���� �� ������Ʈ�� ����
        if (Input.touchCount == 0) return;

        InputPos = TouchHelper.TouchPos;

        if (TouchHelper.IsDown)
        {
            WeaphonesShoot();
        }

    }

/*    protected virtual void OnPut(Vector3 pos)
    {
        HoldingObj.GetComponent<Rigidbody>().useGravity = true;
        HoldingObj.transform.SetParent(null); // �ڽ�����
    }

    protected virtual void OnHold()
    {
        HoldingObj.GetComponent<Rigidbody>().useGravity = false;

        //���� ������Ʈ�� ī�޶��� �ڽ�����
        HoldingObj.transform.SetParent(mainCam.transform); // �ڽĵ��
        HoldingObj.transform.rotation = Quaternion.identity;
        HoldingObj.transform.position = mainCam.ViewportToWorldPoint(new Vector3(0.5f, positionY, mainCam.nearClipPlane * cameraDistance));
    }
*/
    void Move(Vector3 pos)
    {
        //������Ʈ �̵�
        //z���� ����
        pos.z = mainCam.nearClipPlane * cameraDistance;
        HoldingObj.transform.position = Vector3.Lerp(HoldingObj.transform.position, mainCam.ScreenToWorldPoint(pos), Time.deltaTime * 7f);
    }

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
                Mng.I.count--;
                //en.IfDestroyed();
                //Instantiate(smoke, hit.point, Quaternion.LookRotation(hit.normal));
            //}
        }
    }

}

