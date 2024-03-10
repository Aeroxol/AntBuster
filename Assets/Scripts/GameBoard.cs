using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace AntBuster
{
    public class Node
    {
        private int _x, _y;
        public Node(int x, int y)
        {
            _x = x;
            _y = y;
        }
    }

    public class GameBoard : MonoBehaviour
    {
        private Node[,] m_gameBoard;
        private int m_width, m_height;

        // Start is called before the first frame update
        void Start()
        {
            Generate(20, 20);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Generate(int width, int height)
        {
            m_gameBoard = new Node[width, height];
            m_width = width;
            m_height = height;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    m_gameBoard[i, j] = new Node(i, j);
                }
            }
        }

        public List<Node> Evaluate(Vector2Int from, Vector2Int to)
        {
            return Evaluate(from.x, from.y, to.x, to.y);
        }

        public List<Node> Evaluate(int fromX, int fromY, int toX, int toY)
        {
            fromX = Mathf.Clamp(fromX, 0, m_width - 1);
            fromY = Mathf.Clamp(fromY, 0, m_height - 1);
            toX = Mathf.Clamp(toX, 0, m_width - 1);
            toX = Mathf.Clamp(toY, 0, m_height - 1);

            List<Node> route = new List<Node> { m_gameBoard[fromX, fromY] };
            List<Node> openList = new List<Node>();
            List<Node> closedList = new List<Node>();

            while (openList.Count != 0)
            {
                Node _node = openList[0];
                openList.RemoveAt(0);
                closedList.Add(_node);
            }

            return route;
        }

        private void OnDrawGizmosSelected()
        {
            try
            {
                for (int i = 0; i < m_width; i++)
                {
                    Debug.DrawLine(new Vector3(i, 0), new Vector3(i, m_height - 1));
                }
                for (int j = 0; j < m_height; j++)
                {
                    Debug.DrawLine(new Vector3(0, j), new Vector3(m_width - 1, j));
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}