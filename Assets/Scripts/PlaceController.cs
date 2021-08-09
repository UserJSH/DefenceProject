using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceController : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private ARRaycastManager raycastMng;
    [SerializeField] private GameObject placeIndicator; // ȭ��ǥ
    [SerializeField] private GameObject[] prefab; // ���� ������

    private Dictionary<int, GameObject> dic = new Dictionary<int, GameObject>(); // ������ ������ ��Ƶ� �÷���

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�浹 ���� ����
        var hit = new List<ARRaycastHit>();

        //���� ��ũ���� �߾Ӱ�(viewport�� ��ũ�� ��ǥ�� ��ȯ)
        var center = mainCam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));

        //�ٴڰ� �浹 �Ǵ���
        raycastMng.Raycast(center, hit, TrackableType.PlaneWithinBounds);

        //ȭ��ǥ�� �������� �Ⱥ������� ����
        placeIndicator.SetActive(hit.Count > 0);

        if (hit.Count == 0) return;

        //ī�޶��� �������
        var camForward = mainCam.transform.TransformDirection(Vector3.forward);

        //ȸ������ y�ప�� �����ؼ� ȸ����Ű�� ����
        var camBearing = new Vector3(camForward.x, 0, camForward.z).normalized;

        //�浹�� Ʈ������ ����
        var pose = hit[0].pose;

        //���ϴ� ������ ȭ��ǥ�� ������ ����(�׻� ī�޶� ���� ����)
        pose.rotation = Quaternion.LookRotation(camBearing);

        // �浹�� ��ġ�� ȭ��ǥ ����
        placeIndicator.transform.SetPositionAndRotation(pose.position, pose.rotation);

        if (TouchHelper.Touch3)
        {
            var index = Random.Range(0, prefab.Length);

            var obj = Instantiate(prefab[index], pose.position, pose.rotation, transform);
            obj.transform.position = new Vector3(pose.position.x, pose.position.y + 0.05f, pose.position.z);
            

            obj.SetActive(true);

            dic.Add(obj.GetInstanceID(), obj); //������Ʈ�� �����ɶ� �÷��ǿ� �߰�

            RefreshSelection(obj);
        }
        //���� ����
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

            //������ �������
            if (furniture)
            {
                //���õ� ������ true
                furniture.IsSelected = (select.GetInstanceID() == item.Key);
            }
        }
    }
}
