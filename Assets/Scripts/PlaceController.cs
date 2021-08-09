using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceController : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private ARRaycastManager raycastMng;
    [SerializeField] private GameObject placeIndicator; // 화살표
    [SerializeField] private GameObject[] prefab; // 가구 오브제

    private Dictionary<int, GameObject> dic = new Dictionary<int, GameObject>(); // 생성된 가구를 담아둘 컬렉션

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //충돌 정보 저장
        var hit = new List<ARRaycastHit>();

        //현재 스크린의 중앙값(viewport를 스크린 좌표로 변환)
        var center = mainCam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));

        //바닥과 충돌 되는지
        raycastMng.Raycast(center, hit, TrackableType.PlaneWithinBounds);

        //화살표를 보여줄지 안보여줄지 결정
        placeIndicator.SetActive(hit.Count > 0);

        if (hit.Count == 0) return;

        //카메라의 정면방향
        var camForward = mainCam.transform.TransformDirection(Vector3.forward);

        //회전축의 y축값을 고정해서 회전시키기 위해
        var camBearing = new Vector3(camForward.x, 0, camForward.z).normalized;

        //충돌된 트랜스폼 정보
        var pose = hit[0].pose;

        //원하는 각도로 화살표가 방향을 설정(항상 카메라 앞쪽 방향)
        pose.rotation = Quaternion.LookRotation(camBearing);

        // 충돌된 위치에 화살표 생성
        placeIndicator.transform.SetPositionAndRotation(pose.position, pose.rotation);

        if (TouchHelper.Touch3)
        {
            var index = Random.Range(0, prefab.Length);

            var obj = Instantiate(prefab[index], pose.position, pose.rotation, transform);
            obj.transform.position = new Vector3(pose.position.x, pose.position.y + 0.05f, pose.position.z);
            

            obj.SetActive(true);

            dic.Add(obj.GetInstanceID(), obj); //오브젝트가 생성될때 컬렉션에 추가

            RefreshSelection(obj);
        }
        //가구 선택
        if (TouchHelper.IsDown)
        {
            
            if (Physics.Raycast(mainCam.ScreenPointToRay(TouchHelper.TouchPos), out var hits, mainCam.farClipPlane))
            {
                if (hits.transform.tag.Equals("Player"))
                {
                    RefreshSelection(hits.transform.gameObject);
                }
            }
        }
    }

    private void RefreshSelection(GameObject select)
    {
        foreach (var item in dic)
        {
            var furniture = item.Value.GetComponent<Furniture>();

            //가구가 있을경우
            if (furniture)
            {
                //선택된 가구만 true
                furniture.IsSelected = (select.GetInstanceID() == item.Key);
            }
        }
    }
}
