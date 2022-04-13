using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    //GameObject soldier;
    //-
    //[SerializeField]
    //GameObject bulletPrefab;

    //GameObject target;
    //Vector3 direction;

    //float speed;

    //int counter;
    //bool isStart = false;

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Soldier"))
    //    {
    //        Debug.Log("Soldier");
    //        Destroy(other.gameObject);
    //        Destroy(gameObject);
    //    }
    //    //else if (other.gameObject.CompareTag("Soldier"))
    //    //{
    //    //    Debug.Log("Soldier");
    //    //    Destroy(gameObject);
    //    //}
    //    //Destroy(gameObject); //self
    //}

    //public void SetDirection(Vector3 direction)
    //{
    //    this.direction = direction;
    //}

    void Start()
    {
        //soldier = GameObject.FindWithTag("Soldier");
        //-
        //speed = 8f;
        //counter = 0;
        //isStart = true;
    }

    void Update()
    {
        //if (this.gameObject.activeSelf)
        //    this.transform.position = Vector3.MoveTowards(this.transform.position, soldier.transform.position, 5f);
        //if (transform.position == soldier.transform.position)
        //{
        //    this.gameObject.SetActive(false);
        //}
        //--
        //if (isStart)
        //{
        //    GameObject bullet = Instantiate(bulletPrefab);
        //    bullet.transform.position += direction * speed * Time.deltaTime;
        //}
        //counter++;

        //if (counter > 300)
        //{
        //    Destroy(gameObject);
        //}
    }
}