using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    GameObject gun;
    GameObject target;
    bool targetFound = false;
    bool rotateCompleted = false;
    bool nextshoot = false;

    Vector3 rotateToTarget;
    int counter = 0;
    int step = 10;
    Vector3 lastVector;

    private float ftime;
    public bool start;

    GameObject bullet;

    //public float towerLife;
    public Text HPText;

    float speed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        target = null;
        HPText = GameObject.Find("UIManager").GetComponent<UIMain>().hpText;
        //DisplayHP();
        //rotateToTarget = Quaternion.LookRotation(Vector3.back).eulerAngles;
    }
    void RotateTower()
    {
        counter++;

        var nextRotate = (counter * (rotateToTarget - lastVector) / step) + lastVector;

        transform.rotation = Quaternion.Euler(nextRotate);

        if (counter == step)
        {
            rotateCompleted = true;
            counter = 0;
            lastVector = nextRotate;
        }
    }
     

    // Update is called once per frame
    void Update()
    {
        if (nextshoot == false)
        {
            UpdateTower();
        }
        else
        {
            if (nextshoot)
            {
                rotateCompleted = false;
                targetFound = false;
                nextshoot = false;

            }
        }
    }
    public void DestroyTower() 
    {
        Destroy(gameObject);
    }
    void UpdateTower()
    {
        if (targetFound)
        {
            Debug.Log("found");

            //rotate
            if (rotateCompleted)//旋轉完成
            {               
                Shoot();//射擊小兵
            }
            else
            {
                RotateTower();//旋轉塔
            }
        }
        else
        {
            Debug.Log("Not found");

            FindTarget();//尋找目標小兵
        }
    }
    void FindTarget()
    {
        GameObject[] soldier = GameObject.FindGameObjectsWithTag("Soldier");

        if (soldier.Length == 0) return;

        targetFound = true;

        // 0 1 2 3
        int targetIndex = Random.Range(0, soldier.Length); // 0 <= x < length

        target = soldier[targetIndex];

        //計算轉向到目標的旋量
        //rotateToTarget = Quaternion.LookRotation(target.transform.position - transform.position);
        rotateToTarget = Quaternion.LookRotation(target.transform.position - transform.position).eulerAngles;

        rotateToTarget.x = 0;
        rotateToTarget.z = 0;

        if(rotateToTarget.y > 180f)
        {
            rotateToTarget.y -= 360;
        }
    }
    void Shoot()
    {
        if (bullet == null)
        {
            bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);//在槍口創造子彈
        }
        bullet.transform.LookAt(target.transform.position);//子彈對準目標小兵

        //子彈移動速度
        bullet.transform.position += bullet.transform.forward * speed * Time.deltaTime;
        Vector3 bulletdirection = target.transform.position - bullet.transform.position;

        if (bulletdirection.magnitude < 0.05f)
        {
            Destroy(bullet);//摧毀子彈
            Destroy(target);//摧毀目標小兵
            GameObject.Find("UIManager").GetComponent<UIMain>().AddLoss();//取得UI程式的小兵擊殺數計算
            nextshoot = true;//執行下一個目標
        }

        //bullet.GetComponent<BulletBehaviour>().SetDirection(direction);
    }
    //add:GameObject.Find("UIManager").GetComponent<UIMain>().gotoTheEnd();
    //public float getTowerLife()//塔生命
    //{
    //    return towerLife;//回傳塔生命
    //}
    //塔掉血
    //public void ReduceBlood()//塔扣血
    //{
    //    //towerLife -= 10f;
    //    //DisplayHP();
    //    GameObject.Find("UIManager").GetComponent<UIMain>().SubHP();//取得UI程式的扣血
    //}
    //public void DisplayHP() 
    //{
    //    HPText.text = towerLife.ToString();
    //}
    void OnDestroy()//摧毀後執行
    {
        if (bullet != null) //子彈存在的話
        {
            Destroy(bullet);//摧毀子彈
        }
        //GetComponent<ARController>().SetAllPlanesActive(true);
    }
}
