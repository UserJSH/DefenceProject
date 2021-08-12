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
    private Player player;



    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        obj[0].SetActive(true);
        player = GameObject.Find("HP").GetComponent<Player>();

    }

    //    //어느위치에 오브젝트를 생성시킬지 position을 받아옴
    //    var pos = mainCam.ViewportToWorldPoint(new Vector3(0.45f, positionY, mainCam.nearClipPlane * cameraDistance));

    // Update is called once per frame
    void Update()
    {
        player.ScoreUp();
        //손가락을 터치했을 때 오브젝트를 생성
        if (Input.touchCount == 0) return;

        if (TouchHelper.IsDown)
        {
            WeaphonesShoot();
        }

    }


        //잡은 오브젝트를 카메라의 자식으로
        //HoldingObj.transform.SetParent(mainCam.transform); // 자식등록


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
                
                //en.IfDestroyed();
                //Instantiate(smoke, hit.point, Quaternion.LookRotation(hit.normal));
            //}
        }
    }

}

