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
    int step = 60;
    Vector3 lastVector;

    private float ftime;
    public bool start;

    GameObject bullet;

    public float towerLife = 100;
    public Text HPText;
    bool gameover = false;

    float speed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        target = null;
        DisplayHP();
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
        if (getTowerLife() <= 0)
        {
            gameover = true;
            Destroy(gameObject);
        }
    }

    void UpdateTower()
    {
        if (targetFound)
        {
            Debug.Log("found");

            //rotate
            if (rotateCompleted)
            {               
                Shoot();
            }
            else
            {
                RotateTower();
            }
        }
        else
        {
            Debug.Log("Not found");

            FindTarget();
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
            bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);
        }
        bullet.transform.LookAt(target.transform.position);

        //子彈移動速度
        bullet.transform.position += bullet.transform.forward * speed * Time.deltaTime;
        Vector3 bulletdirection = target.transform.position - bullet.transform.position;

        if (bulletdirection.magnitude < 0.05f)
        {
            Destroy(bullet);
            Destroy(target);
            target.GetComponent<SoldierBehaviour>().KillSoldier();
            nextshoot = true;
        }

        //bullet.GetComponent<BulletBehaviour>().SetDirection(direction);
    }
    public float getTowerLife()
    {
        return towerLife;
    }
    //塔掉血
    public void ReduceBlood()
    {
        towerLife -= 5f;
        DisplayHP();
    }
    public void DisplayHP() 
    {
        HPText.text = towerLife.ToString();
    }
    void OnDestroy()
    {
        if (bullet != null) 
        {
            Destroy(bullet);
        }
    }
}
