using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateExample : MonoBehaviour
{
    [SerializeField]
    GameObject cubePrefab;

    [SerializeField]
    GameObject indicator;
    [SerializeField]
    Camera ARCam;
    [SerializeField]
    GameObject towerPrefab;

    GameObject tower;
    GameObject model;

    List<GameObject> cubes;

    bool tooClose;

    float minDistance;

    // Start is called before the first frame update
    void Awake()
    {
        cubes = new List<GameObject>();
        tooClose = false;
        minDistance = 0.2f;
    }

    public void Click()
    {
        RaycastHit hit = RayCastTest();

        Debug.Log(tooClose);

        if (tooClose) return;

        if(hit.transform == null)
        {
            Debug.Log("Hit nothing");
        }
        else 
        {
            cubes.Add(Instantiate(cubePrefab, hit.transform.position, hit.transform.rotation));

            //if (tower == null)
            //{
            //    tower = Instantiate(towerPrefab, indicator.transform.position, indicator.transform.rotation);
            //}
            //else 
            //{
            //    model = Instantiate(cubePrefab, hit.transform.position, hit.transform.rotation);
            //}
        }

    }
    
    public RaycastHit RayCastTest()
    {
        Camera cam = FindObjectOfType<Camera>();

        Vector3 hitScreen = Input.mousePosition;

        Vector3 worldPoint = cam.ScreenToWorldPoint(hitScreen);
        Vector3 viewPoint = cam.ScreenToViewportPoint(hitScreen);

        Debug.Log(worldPoint);
        Debug.Log(viewPoint);

        Physics.Raycast(worldPoint, (viewPoint - worldPoint), out RaycastHit hit);

        Debug.DrawLine(worldPoint, (viewPoint - worldPoint), Color.red, 20f);

        tooClose = false;

        foreach(GameObject cube in cubes)
        {
            Vector3 difference = cube.transform.position - hit.transform.position;

            float distance = difference.magnitude;

            Debug.Log("distance "+distance);

            if(distance < minDistance)
            {
                tooClose = true;

                break;
            }
        }

        return hit;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Click();
        }
    }
}
