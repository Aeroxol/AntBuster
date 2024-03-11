using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnsBuster
{
    public class Ant : MonoBehaviour
    {
        public float moveSpeed;
        public float turnTime;

        private Vector2 m_direction;
        private float m_turnTimer;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(moveSpeed * m_direction * Time.deltaTime);
            m_turnTimer -= Time.deltaTime;
            if(m_turnTimer < 0)
            {
                foo();
                m_turnTimer += turnTime;
            }
        }

        private void foo()
        {
            float f = 360f * Random.value;
            Vector3 v = Quaternion.Euler(0, 0, f) * Vector3.right;
            m_direction = v;
        }
    }
}