using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : WeaphonesController
{
    private Vector2 InputPosPivot;

    //�ʱ�ȭ
    private void init()
    {

    }
    //����
    protected override void OnPut(Vector3 pos)
    {
        var rigid = HoldingObj.GetComponent<Rigidbody>();
        rigid.useGravity = true;
        var direction = mainCam.transform.TransformDirection(Vector3.forward.normalized);

        var delta = (pos.y - InputPosPivot.y) * 100 / Screen.height; // ���� ������ ���� ������ ���̰�, �̸� �̿��� ���� ������ ��
        rigid.AddForce((direction + Vector3.up) * 4.5f * delta); // (��������)������ ����
        HoldingObj.transform.SetParent(null);
        InputPosPivot.y = pos.y; // ���������� y���� �־���
    }

    //���
    protected override void OnHold()
    {
        //base.OnHold();
        InputPosPivot = InputPos;
    }
}
