using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private int price;
    [SerializeField] private float atkRate;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject bullet;

    private bool isAttack = false;
    private float currentFireRate = 0;
    private Transform target;

    private void FixedUpdate()
    {
        SearchEnemy();
    }

    private void SearchEnemy()
    {
        Collider2D[] _target = Physics2D.OverlapCircleAll(transform.position, range, layerMask);
        
        if(_target.Length > 0)
        {
            print("타겟 찾음");
            target = _target[0].transform;  // TODO: 타겟팅 시스템이 추가되면 변경
            Attack(target);
        }
        else
        {
            print("타겟 잃음");
        }
    }

    private void Attack(Transform target)
    {
        currentFireRate += Time.deltaTime;
        if (currentFireRate >= atkRate)
        {
            currentFireRate = 0;
            GameObject projectile = PoolManager.Instance.Get(1);
            projectile.transform.position = this.transform.position;
            projectile.GetComponent<Bullet>().GetInfo(target.position - transform.position, bulletSpeed);
        }
    }
}
