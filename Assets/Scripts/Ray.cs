using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Ray : MonoBehaviour
{
    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject pre;
    bool IndicatorState = false;
    private ARRaycastManager arMng;
    private GameObject placedObj;
    private PlaceController ind;
    private Ray ray;

    // Start is called before the first frame update
    void Start()
    {
        indicator.SetActive(false);
        arMng = GetComponent<ARRaycastManager>();
        ind = GetComponent<PlaceController>();
        ray = GetComponent<Ray>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IndicatorState)
        {
            DetectGround();
        }
        

        if(indicator.activeInHierarchy && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);


            if(touch.phase == TouchPhase.Began)
            {
                if(placedObj == null)
                {
                    GameObject obj = Instantiate(pre);
                    //obj.transform.position = indicator.transform.position + new Vector3(0.28f, 0);
                    obj.transform.position = indicator.transform.position;
                    obj.transform.rotation = indicator.transform.rotation;
                    obj.transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
                    //obj.transform.rotation = Quaternion.Euler(0,-90,0);

                    placedObj = obj;


                    GameObject del = del = GameObject.Find("Ground 1");
                    del.SetActive(false);
                    ray.enabled = false;
                    IndicatorState = true;
                    //ind.enabled = true;
                }
                else
                {
                    
                    placedObj.transform.SetPositionAndRotation(
                            indicator.transform.position,
                            indicator.transform.rotation
                        );
                }

            }
        }
    }
    void DetectGround()
    {
        Vector2 screenSize = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        
        List<ARRaycastHit> hitInfos = new List<ARRaycastHit>();

        if(arMng.Raycast(screenSize, hitInfos, TrackableType.Planes))
        {
            float dis = hitInfos[0].distance;
            indicator.SetActive(true);

            indicator.transform.position = hitInfos[0].pose.position;
            //indicator.transform.rotation = Quaternion.Euler(0, 180, 0);
            indicator.transform.rotation = hitInfos[0].pose.rotation;
            indicator.transform.position += indicator.transform.up * 0.1f;

            //indicator.transform.localScale = new Vector3(dis * 0.001f, dis * 0.001f, dis * 0.001f);
            indicator.transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);

        }
        else
        {
            indicator.SetActive(false);
        }
    }

}
