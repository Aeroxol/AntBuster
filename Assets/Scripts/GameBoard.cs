using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace AntBuster
{
    public class Cell
    {
        private int _x, _y;
        private bool m_isOccupied;
        private Cannon m_cannon;
        public Cell(int x, int y)
        {
            _x = x;
            _y = y;
        }
        public void SetCannon(Cannon cannon)
        {
            if (cannon == null)
            {
                m_isOccupied = false;
            }
            else
            {
                m_cannon = cannon;
                m_isOccupied = true;
            }
        }
        public Cannon GetCannon()
        {
            if (m_isOccupied)
            {
                return m_cannon;
            }
            else
            {
                return null;
            }
        }
    }

    public class GameBoard : MonoBehaviour
    {
        private Cell[,] m_gameBoard;
        private int m_width, m_height;
        private List<Cannon> m_cannons = new List<Cannon>();

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
            m_gameBoard = new Cell[width, height];
            m_width = width;
            m_height = height;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    m_gameBoard[i, j] = new Cell(i, j);
                }
            }
        }

        public List<Cell> Evaluate(Vector2Int from, Vector2Int to)
        {
            return Evaluate(from.x, from.y, to.x, to.y);
        }

        public List<Cell> Evaluate(int fromX, int fromY, int toX, int toY)
        {
            fromX = Mathf.Clamp(fromX, 0, m_width - 1);
            fromY = Mathf.Clamp(fromY, 0, m_height - 1);
            toX = Mathf.Clamp(toX, 0, m_width - 1);
            toX = Mathf.Clamp(toY, 0, m_height - 1);

            List<Cell> route = new List<Cell> { m_gameBoard[fromX, fromY] };
            List<Cell> openList = new List<Cell>();
            List<Cell> closedList = new List<Cell>();

            while (openList.Count != 0)
            {
                Cell _node = openList[0];
                openList.RemoveAt(0);
                closedList.Add(_node);
            }

            return route;
        }

        public Cell GetNextCell(Vector2Int v)
        {
            return GetNextCell(v.x, v.y);
        }

        public Cell GetNextCell(int x, int y)
        {
            return m_gameBoard[x + 1, y + 1];
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