using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaphonesController : MonoBehaviour
{
    protected const float cameraDistance = 1.5f; //
    protected float positionY = 0.2f; // Y축 포지션
    [SerializeField] protected GameObject[] obj; // 생성할 오브젝트
    [SerializeField] private Animator BowAnim; //석궁 애니메이터

    protected Camera mainCam; // 메인카메라
    protected GameObject HoldingObj; // 손으로 잡은 오브젝트
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
    //    //어느위치에 오브젝트를 생성시킬지 position을 받아옴
    //    var pos = mainCam.ViewportToWorldPoint(new Vector3(0.45f, positionY, mainCam.nearClipPlane * cameraDistance));

    //    //랜덤
    //    var index = UnityEngine.Random.Range(0, obj.Length);

    //    //오브젝트 생성
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
        //손가락을 터치했을 때 오브젝트를 생성
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
        HoldingObj.transform.SetParent(null); // 자식해제
    }

    protected virtual void OnHold()
    {
        HoldingObj.GetComponent<Rigidbody>().useGravity = false;

        //잡은 오브젝트를 카메라의 자식으로
        HoldingObj.transform.SetParent(mainCam.transform); // 자식등록
        HoldingObj.transform.rotation = Quaternion.identity;
        HoldingObj.transform.position = mainCam.ViewportToWorldPoint(new Vector3(0.5f, positionY, mainCam.nearClipPlane * cameraDistance));
    }
*/
    void Move(Vector3 pos)
    {
        //오브젝트 이동
        //z축을 고정
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

                // 만약 hitInfo가 Enemy컴포넌트를 가지고 있다면?
                Enemy enemy = hit.transform.GetComponent<Enemy>();
                if (enemy != null)
                {
                    // enemy의 AddDamage 함수를 호출하고 싶다.
                    enemy.EnemyTakeDamage(1);
                }
                Mng.I.count--;
                //en.IfDestroyed();
                //Instantiate(smoke, hit.point, Quaternion.LookRotation(hit.normal));
            //}
        }
    }

}

