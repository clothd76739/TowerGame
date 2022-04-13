using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldierBehaviour : MonoBehaviour
{
    GameObject tower;
    [SerializeField]
    GameObject gungun;
    [SerializeField]
    GameObject bulletPrefab;

    GameObject bullet;

    GameObject target;

    Vector3 rotateToTarget;
    bool moving;
    int counter = 0;
    int step = 60;
    bool targetFound = false;
    bool rotateCompleted = false;

    public int soldierKill;
    public Text KillText;

    void Start()
    {
        tower = GameObject.FindGameObjectWithTag("Tower");
        moving = true;
        target = null;
    }

    //void Move()
    //{
    //    Vector3 direction = tower.transform.position - Cube.transform.position;
    //    float distance = direction.magnitude;

    //    //if (moveCounter < 3 * duration)
    //    //{
    //    //    Cube.transform.position += direction * Time.deltaTime;
    //    //}
    //    //else
    //    //{
    //    //}
    //    //moveCounter++;
    //}

    void Update()
    {
        //Cube.transform.position = Vector3.Lerp(Cube.transform.position, tower.transform.position, speed);
        //speed = calculateNewSpeed();



        if (moving)
        {
            UpdateMove();
        }
        else
        {
            Shoot();
        }


        #region OLD CODE

        //Vector3 direction = tower.transform.position - Cube.transform.position;
        //float distance = direction.magnitude;
        //if (moveCounter < 3 * duration)
        //{
        //    Cube.transform.position += direction * Time.deltaTime;
        //}
        //else
        //{
        //}
        //moveCounter++;
        //if (distance < 0)
        //{

        //}
        #endregion
    }

    void UpdateMove()
    {
        if (tower != null)
        {
            transform.LookAt(tower.transform.position);//找到塔的方向

            transform.position += transform.forward * 0.2f * Time.deltaTime;//移動小兵

            Vector3 direction = tower.transform.position - transform.position;

            if (direction.magnitude < 0.25f)
            {
                moving = false;
            }
        }
    }
    void FindTarget()
    {
        GameObject[] tower = GameObject.FindGameObjectsWithTag("Soldier");

        Debug.Log(tower.Length);

        if (tower.Length == 0) return;

        targetFound = true;

        // 0 1 2 3
        int targetIndex = Random.Range(0, tower.Length); // 0 <= x < length

        target = tower[targetIndex];

        //計算轉向到目標的旋量
        //rotateToTarget = Quaternion.LookRotation(target.transform.position - transform.position);
        rotateToTarget = Quaternion.LookRotation(target.transform.position - transform.position).eulerAngles;

        rotateToTarget.x = 0;
        rotateToTarget.z = 0;

        if (rotateToTarget.y > 180f)
        {
            rotateToTarget.y -= 360;
        }

        Debug.Log(rotateToTarget);

    }
    void RotateTower()
    {
        counter++;

        var nextRotate = counter * rotateToTarget / step;

        transform.rotation = Quaternion.Euler(nextRotate);

        if (counter == step)
        {
            rotateCompleted = true;
        }
    }


    void Shoot()
    {
        float speed = 0.5f;

        if (bullet == null)
        {
            bullet = Instantiate(bulletPrefab, gungun.transform.position, Quaternion.identity);
        }
        //子彈移動速度
        bullet.transform.position += transform.forward * speed * Time.deltaTime;

        //子彈跟塔的距離
        Vector3 bulletdirection = tower.transform.position - bullet.transform.position;

        if (bulletdirection.magnitude < 0.05f)
        {
            //Instantiate(bullet, transform.position, Quaternion.identity);
            tower.GetComponent<TowerBehaviour>().ReduceBlood();
            Destroy(bullet);
        }
    }
    public void KillSoldier()
    {
        soldierKill += 1;
        KillText.text = soldierKill.ToString();
    }
    void OnDestroy()
    {
        if (bullet != null)
        {
            Destroy(bullet);
        }
    }
}
