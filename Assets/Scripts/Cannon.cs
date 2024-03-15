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
        Attack();
    }


    // 임시로 포탑을 이동시킬 수 있게 만들었습니다.
    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.position += new Vector3(h, v, 0) * 5.0f * Time.deltaTime;
    }

    private void SearchEnemy()
    {
        //(a-b).sqrMagnitude
        Collider2D[] _target = Physics2D.OverlapCircleAll(transform.position, range, layerMask);
        
        if(_target.Length > 0)
        {
            print("타겟 찾음");
            isAttack = true;
            target = _target[0].transform;  // TODO: 타겟팅 시스템이 추가되면 변경
            //StartCoroutine(AttackRoutine(_target[0].transform.position));
        }
        else
        {
            print("타겟 잃음");
            isAttack = false;
            //StopCoroutine(AttackRoutine(_target[0].transform.position));
        }
    }

    private void Attack()
    {
        if (isAttack)
        {
            currentRateOfFire += Time.deltaTime;
            if (currentRateOfFire >= atkRate)
            {
                currentRateOfFire = 0;
                CreateProjectile(target.transform.position - transform.position);
            }
        }
    }

    private void CreateProjectile(Vector2 target)
    {
        var projectile = Instantiate(bullet, transform.position, Quaternion.identity);
        projectile.GetComponent<Bullet>().GetInfo(target, bulletSpeed);
    }

    //IEnumerator AttackRoutine(Vector2 target)
    //{
    //    CreateProjectile(target);
    //    yield return new WaitForSeconds(1.0f / atkRate);
    //}
}
