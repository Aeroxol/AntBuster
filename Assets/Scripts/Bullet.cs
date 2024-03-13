using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{    
    public float moveSpeed = 5.0f;

    public Vector2 targetPosition;

    void Start()
    {
        // 임시로 5초 뒤 삭제. 
        Destroy(gameObject, 5f);
    }

    // 총알 충돌 테스트를 위한 움직임 구현
    void FixedUpdate()
    {
        TrackingTarget(targetPosition, moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            print("공격에 맞음");
            Destroy(gameObject);
        }
    }

    private void TrackingTarget(Vector2 targetPosition, float speed)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed);
    }
}
