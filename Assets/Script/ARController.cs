using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARPlaneManager))]
public class ARController : MonoBehaviour
{
    [SerializeField]
    GameObject modelPrefab;
    [SerializeField]
    GameObject indicator;
    [SerializeField]
    Camera ARCam;
    [SerializeField]
    GameObject towerPrefab;

    GameObject tower;
    GameObject model;

    bool indicatorValid;
    Pose indicatorPose;
    ARRaycastManager rayManager;
    Vector3 center;
    List<ARRaycastHit> hits;

    List<float> heights;

    float averageHeight;

    bool Towerone;

    // Start is called before the first frame update
    void Start()
    {
        rayManager = GetComponent<ARRaycastManager>();
        indicator.SetActive(false);
        center = new Vector3(0.5f, 0.5f);
        hits = new List<ARRaycastHit>();
        Towerone = true;
        heights = new List<float>();
    }

    //void SetAllPlanesActive(bool value)
    //{
    //    foreach (var plane in rayManager.trackables)
    //        plane.gameObject.SetActive(value);
    //}


    // Update is called once per frame
    void Update()
    {
        UpdateIndicator();
        UpdateModel();
    }

    void UpdateIndicator()
    {
        Vector3 screenCenter = ARCam.ViewportToScreenPoint(center);


        if (rayManager.Raycast(screenCenter, hits, TrackableType.Planes))
        {
            //pose = position + rotation
            indicator.transform.position = hits[0].pose.position;

            //set the rotation of the indicator
            Vector3 forward = Camera.current.transform.forward;
            forward.y = 0;
            indicator.transform.rotation = Quaternion.LookRotation(forward.normalized);
                        
            //set the condition memebers
            hits.Clear();//clear the buffer
            indicatorValid = true;//set the flag
            indicator.SetActive(true);//if the ray hit some planes, do something

            float sumHeight = 0;//

            for (int i = 0; i < heights.Count; i++)
            {
                sumHeight += heights[i];
            }

            averageHeight = sumHeight / heights.Count;

            foreach (GameObject soldiers in GameObject.FindGameObjectsWithTag("Soldier"))
            {
                Vector3 position = soldiers.transform.position;

                soldiers.transform.position = new Vector3(position.x, averageHeight, position.z);
            }//



            float currentHeight = indicator.transform.position.y;

            if (Mathf.Abs(currentHeight - averageHeight) >= 0.1f) //too far
            {
                //SetAllPlanesActive(false);
                indicatorValid = false;//set the flag
                indicator.SetActive(false);//hide the indicator
            }
            else
            {
                heights.Add(currentHeight);//add a new height data
            }
        }
        else
        {
            indicatorValid = false;//set the flag
            indicator.SetActive(false);//hide the indicator
        }
    }

    void UpdateModel()
    {
        if (indicatorValid == false) return;
        if (Input.touchCount == 0) return;
        if (Input.GetTouch(0).phase != TouchPhase.Began) return;
        


        if (Towerone)
        {
            tower = Instantiate(towerPrefab, indicator.transform.position, indicator.transform.rotation);
            Towerone = false;
        }
        else
        {
            model = Instantiate(modelPrefab, indicator.transform.position, indicator.transform.rotation);
        }
    }



}
