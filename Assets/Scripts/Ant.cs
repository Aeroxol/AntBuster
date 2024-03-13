using AntBuster;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnsBuster
{
    public class Ant : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;

        public float moveSpeed;
        public float turnTime;

        private Vector2 m_direction;
        [SerializeField]
        private Cell m_targetCell;
        private float m_turnTimer;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            // 1안
            // Way1();
            // 2안
            Way2();

            Quaternion targetQ = Quaternion.FromToRotation(Vector3.up, m_direction);
            float blend = 1f - Mathf.Pow(1f - 0.9f, Time.deltaTime);
            spriteRenderer.transform.rotation = Quaternion.Lerp(spriteRenderer.transform.rotation, targetQ, blend);
        }

        private void Way1()
        {
            transform.Translate(moveSpeed * m_direction * Time.deltaTime);
            m_turnTimer -= Time.deltaTime;
            if (m_turnTimer < 0)
            {
                SetRandomDirection();
                m_turnTimer += turnTime;
            }
        }

        private void Way2()
        {
            if (m_targetCell == null) GetNextCell(GameManager.Instance.gameBoard);

            m_direction = (m_targetCell.position - transform.position).normalized;
            transform.Translate(moveSpeed * m_direction * Time.deltaTime);
            if((transform.position - m_targetCell.position).magnitude < 0.1f){
                m_targetCell = GetNextCell(GameManager.Instance.gameBoard);
            }
        }

        private void SetRandomDirection()
        {
            float f = 360f * Random.value;
            Vector3 v = Quaternion.Euler(0, 0, f) * Vector3.right;
            m_direction = v;
        }

        private Cell GetNextCell(GameBoard gameBoard)
        {
            return gameBoard.GetNextCell(transform.position);
        }
    }
}