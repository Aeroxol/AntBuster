using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private int price;
    [SerializeField] private int damage;
    [SerializeField] private float range;
    [SerializeField] private float atkRate;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject bullet;

    Transform target;


    private void FixedUpdate()
    {
        SearchEnemy();
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.position += new Vector3(h, v, 0) * 5.0f * Time.deltaTime;
    }

    private void SearchEnemy()
    {
        //(a-b).sqrMagnitude
        Collider2D[] _target = Physics2D.OverlapCircleAll(transform.position, 10f, layerMask);
        
        if(_target.Length > 0)
        {
            for (int i = 0; i < _target.Length; i++)
            {
                if (_target[i].tag == "Ant")
                {
                    print("타겟 감지");
                    target = _target[i].gameObject.transform;
                    
                    // 코루틴으로 연사속도 만큼 변경해야 함
                    CreateProjectile(_target[i].transform.position);
                }
            }
        }
        else
        {
            Debug.Log("타겟 잃음");
            target = null;
        }
    }

    
    private void CreateProjectile(Vector2 target)
    {
        var projectile = Instantiate(bullet, this.transform);
        projectile.GetComponent<Bullet>().targetPosition = target;
    }
}
