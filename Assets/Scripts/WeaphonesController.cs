using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaphonesController : MonoBehaviour
{
    protected const float cameraDistance = 1.5f; //
    protected float positionY = 0.2f; // Y축 포지션
    [SerializeField] protected GameObject[] obj; // 생성할 오브젝트

    protected Camera mainCam; // 메인카메라
    protected GameObject HoldingObj; // 손으로 잡은 오브젝트
    protected Vector3 InputPos; // input posision

    public GameObject arCamera;


    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        init();

    }

    private void init()
    {
        //어느위치에 오브젝트를 생성시킬지 position을 받아옴
        var pos = mainCam.ViewportToWorldPoint(new Vector3(0.45f, positionY, mainCam.nearClipPlane * cameraDistance));

        //랜덤
        var index = UnityEngine.Random.Range(0, obj.Length);

        //오브젝트 생성
        var cube = Instantiate(obj[index], pos, Quaternion.identity, mainCam.transform);
        //RigidBody
        var rigid = cube.GetComponent<Rigidbody>();
        rigid.useGravity = false;
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }

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

    private Vector2 InputPosPivot;

    //초기화

    //놓기
    protected virtual void OnPut(Vector3 pos)
    {
        var rigid = HoldingObj.GetComponent<Rigidbody>();
        rigid.useGravity = true;
        var direction = mainCam.transform.TransformDirection(Vector3.forward.normalized);

        var delta = (pos.y - InputPosPivot.y) * 100 / Screen.height; // 잡은 지점과 놓은 지점의 차이값, 이를 이용해 공을 던지는 힘
        rigid.AddForce((direction + Vector3.up) * 4.5f * delta); // (물리적용)앞으로 힘줌
        HoldingObj.transform.SetParent(null);
        InputPosPivot.y = pos.y; // 놓은시점의 y값을 넣어줌
    }

    //잡기
    protected virtual void OnHold()
    {
        //base.OnHold();
        InputPosPivot = InputPos;
    }
    public void WeaphonesShoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
        {
            if (hit.transform.tag == "Enemy")
            {
                
                Destroy(hit.transform.gameObject);
                Mng.I.count--;
                //en.IfDestroyed();
                //Instantiate(smoke, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
}

