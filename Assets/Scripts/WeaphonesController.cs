using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaphonesController : MonoBehaviour
{
    protected const float cameraDistance = 7.5f; //
    protected float positionY = 0.4f; // Y�� ������
    [SerializeField] protected GameObject[] obj; // ������ ������Ʈ

    protected Camera mainCam; // ����ī�޶�
    protected GameObject HoldingObj; // ������ ���� ������Ʈ
    protected Vector3 InputPos; // input posision
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        init();
    }

    private void init()
    {
        //�����ġ�� ������Ʈ�� ������ų�� position�� �޾ƿ�
        var pos = mainCam.ViewportToWorldPoint(new Vector3(0.5f, positionY, mainCam.nearClipPlane * cameraDistance));

        //����
        var index = UnityEngine.Random.Range(0, obj.Length);

        //������Ʈ ����
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
        //�� �հ����� ��ġ���� �� ������Ʈ�� ����
        if (Input.touchCount == 0) return;

        InputPos = TouchHelper.TouchPos;

        if (TouchHelper.Touch2)
        {
            init();
        }

        //������Ʈ �̵�
        if (HoldingObj)
        {
            //������Ʈ ����
            if (TouchHelper.IsUp)
            {
                OnPut(InputPos);
                HoldingObj = null;
                return;
            }

            Move(InputPos);
        }

        //������Ʈ ���
        if (!TouchHelper.IsDown) return;

        RaycastHit hit;
        if (Physics.Raycast(mainCam.ScreenPointToRay(InputPos), out hit, mainCam.farClipPlane))
        {
            if (hit.transform.tag.Equals("Player"))
            {
                HoldingObj = hit.transform.gameObject;
                OnHold();
            }
        }
    }

    protected virtual void OnPut(Vector3 pos)
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

    void Move(Vector3 pos)
    {
        //������Ʈ �̵�
        //z���� ����
        pos.z = mainCam.nearClipPlane * cameraDistance;
        HoldingObj.transform.position = Vector3.Lerp(HoldingObj.transform.position, mainCam.ScreenToWorldPoint(pos), Time.deltaTime * 7f);
    }

    /*    private Vector2 InputPosPivot;

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
        }*/

}

