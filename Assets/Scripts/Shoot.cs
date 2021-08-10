using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject arCamera;
    public GameObject smoke;

    //private GameObject sp;
    //private EnemyMng ga;

    void Awake()
    {
        //sp = GameObject.Find("MobSpawner");
        //ga = sp.GetComponent<EnemyMng>();
        
    }

    public void WeaphonesShoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
        {
            if (hit.transform.tag == "Enemy")
            {

                Destroy(hit.transform.gameObject);
                //ga.COUNT--;

                //Instantiate(smoke, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
}
