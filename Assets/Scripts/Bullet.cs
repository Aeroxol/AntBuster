using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed;
    private Vector2 targetPosition;

    void Start()
    {
        // 임시로 5초 뒤 삭제. 
        Destroy(gameObject, 5f);
    }

    // 총알 충돌 테스트를 위한 움직임 구현
    void FixedUpdate()
    {
        TrackingTarget(targetPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            print("공격에 맞음");
            Destroy(gameObject);
        }
    }

    public void GetInfo(Vector2 target, float speed)
    {
        targetPosition = target;
        bulletSpeed = speed;
    }

    private void TrackingTarget(Vector2 targetPosition)
    {
        transform.Translate(targetPosition.normalized * bulletSpeed * Time.deltaTime);
    }
}
