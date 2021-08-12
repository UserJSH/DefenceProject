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
    [SerializeField] private GameObject canvas;
    bool IndicatorState = false;
    private ARRaycastManager arMng;
    private WeaphonesController Weaphone;
    //private GameObject placedObj;

    // Start is called before the first frame update
    void Start()
    {
        indicator.SetActive(false);
        arMng = GetComponent<ARRaycastManager>();
        Weaphone = GetComponent<WeaphonesController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IndicatorState)
        {
            DetectGround();
        }
        

        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);


            if(touch.phase == TouchPhase.Began)
            {
                pre.SetActive(true);
                pre.transform.position = indicator.transform.position;
                pre.transform.rotation = indicator.transform.rotation;
                pre.transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);

                //GameObject del = del = GameObject.Find("Ground 1");
                //del.SetActive(false);
                IndicatorState = true;
                canvas.GetComponent<Canvas>().enabled = true;
                indicator.SetActive(false);
                Weaphone.enabled = true;
                
                //if (placedObj == null)
                //{
                    
                //}
                //else
                //{
                    
                //    placedObj.transform.SetPositionAndRotation(
                //            indicator.transform.position,
                //            indicator.transform.rotation
                //        );
                //}

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
            indicator.transform.rotation = hitInfos[0].pose.rotation;
            indicator.transform.position += indicator.transform.up * 0.1f;
            indicator.transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);

        }
        else
        {
            //indicator.SetActive(false);
        }
    }

}
