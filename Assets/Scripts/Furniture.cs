using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    private Camera mainCam;
    [SerializeField] private Transform selectedIcon; // Ÿ�� ȭ��ǥ

    //������ ������Ʈ�� Ȱ�� ��Ȱ�� �ϱ�����
    private LeanDragTranslate translate;
    private LeanPinchScale scale;
    private LeanTwistRotateAxis axis;

    public bool IsSelected //������Ƽ
    {
        get => selectedIcon.gameObject.activeSelf;
        set {
            translate.enabled = scale.enabled = axis.enabled = value;
            selectedIcon.gameObject.SetActive(value); }
    }
    // Start is called before the first frame update
    void Start()
    {
        //������ ������ �����ϴ� ������Ʈ
        translate = gameObject.AddComponent<LeanDragTranslate>();

        //����,�ܾƿ��� �̿��� ������ ���� ����
        scale = gameObject.AddComponent<LeanPinchScale>();

        //ȸ���� �Ҽ� �ִ� ������Ʈ, y�� ȸ����
        axis = gameObject.AddComponent<LeanTwistRotateAxis>();

        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //�� �����Ӹ��� ī�޶� �ٶ󺸵���(������)
        selectedIcon.transform.LookAt(mainCam.transform);
    }
}
