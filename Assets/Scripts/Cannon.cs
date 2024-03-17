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
    private float currentRateOfFire = 0;
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
            Attack();
            target = _target[0].transform;  // TODO: 타겟팅 시스템이 추가되면 변경
        }
        else
        {
            print("타겟 잃음");
        }
    }

    private void Attack()
    {
        currentRateOfFire += Time.deltaTime;
        if (currentRateOfFire >= atkRate)
        {
            currentRateOfFire = 0;
            CreateProjectile(target.transform.position - transform.position);
        }
    }

    private void CreateProjectile(Vector2 target)
    {
        var projectile = Instantiate(bullet, transform.position, Quaternion.identity);
        projectile.GetComponent<Bullet>().GetInfo(target, bulletSpeed);
    }
}
