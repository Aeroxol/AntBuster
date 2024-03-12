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


    private void SearchEnemy()
    {
        Collider[] _target = Physics.OverlapSphere(this.transform.position, range, layerMask);

        for (int i = 0; i < _target.Length; i++)
        {
            Transform _targetTf = _target[i].transform; 

            if (_targetTf.name == "Player")
            {
                Vector2 _direction = (_targetTf.position - this.transform.position).normalized;
            }
        }
    }

    private void CreateProjectile(Vector2 target)
    {
        var projectile = Instantiate(bullet, this.transform);
        projectile.GetComponent<Bullet>().targetPosition = target;
    }
}
