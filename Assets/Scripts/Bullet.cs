using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed;
    private Vector2 targetPosition;
    private float timer;

    private void OnEnable()
    {
        timer = 0;
    }

    // 총알 충돌 테스트를 위한 움직임 구현
    void FixedUpdate()
    {
        TrackingTarget(targetPosition);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 3.0f) 
        {
            timer = 0;
            PoolManager.Instance.Release(gameObject, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            print("공격에 맞음");
            PoolManager.Instance.Release(gameObject, 1);
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
