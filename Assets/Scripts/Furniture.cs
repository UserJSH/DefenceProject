using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    private Camera mainCam;
    [SerializeField] private Transform selectedIcon; // 타겟 화살표

    //가구의 컴포넌트를 활성 비활성 하기위해
    private LeanDragTranslate translate;
    private LeanPinchScale scale;
    private LeanTwistRotateAxis axis;

    public bool IsSelected //프로퍼티
    {
        get => selectedIcon.gameObject.activeSelf;
        set {
            translate.enabled = scale.enabled = axis.enabled = value;
            selectedIcon.gameObject.SetActive(value); }
    }
    // Start is called before the first frame update
    void Start()
    {
        //포지션 정보를 변경하는 컴포넌트
        translate = gameObject.AddComponent<LeanDragTranslate>();

        //줌인,줌아웃을 이용해 스케일 정보 변경
        scale = gameObject.AddComponent<LeanPinchScale>();

        //회전을 할수 있는 컴포넌트, y축 회전만
        axis = gameObject.AddComponent<LeanTwistRotateAxis>();

        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //매 프레임마다 카메라를 바라보도록(빌보드)
        selectedIcon.transform.LookAt(mainCam.transform);
    }
}
