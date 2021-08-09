using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : WeaphonesController
{
    private Vector2 InputPosPivot;

    //초기화
    private void init()
    {

    }
    //놓기
    protected override void OnPut(Vector3 pos)
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
    protected override void OnHold()
    {
        //base.OnHold();
        InputPosPivot = InputPos;
    }
}
