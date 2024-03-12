using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace AntBuster
{
    [Serializable]
    public class Cell
    {
        private int m_x, m_y;
        private Vector3 m_position;
        private bool m_isOccupied;
        private Cannon m_cannon;
        public Vector3 position
        {
            get { return m_position; }
        }

        public Cell(int x, int y)
        {
            m_x = x;
            m_y = y;
            m_position = new Vector3(m_x, m_y);
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
        private int[] dx = new int[8] { -1, -1, -1, 0, 0, 1, 1, 1 };
        private int[] dy = new int[8] { -1, 0, 1, -1, 1, -1, 0, 1 };

        private Cell[,] m_gameBoard;
        private int m_width, m_height;
        private Vector3 offset = Vector3.zero;
        private Vector2 cellSize = Vector2.one;
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

        public List<Cell> GetAdjacentCells(Vector2Int v)
        {
            return GetAdjacentCells(v.x, v.y);
        }

        public List<Cell> GetAdjacentCells(int x, int y)
        {
            List<Cell> cells = new List<Cell>();
            for(int i = 0; i < 8; i++)
            {
                if (x + dx[i] < 0 || y + dy[i] < 0 || x + dx[i] >= m_width || y + dy[i] >= m_height) continue;
                cells.Add(m_gameBoard[x + dx[i], y + dy[i]]);
            }
            return cells;
        }

        public Cell GetNextCell(Vector3 pos)
        {
            int x = Mathf.RoundToInt(pos.x);
            int y = Mathf.RoundToInt(pos.y);
            return GetNextCell(x, y);
        }

        public Cell GetNextCell(Vector2Int v)
        {
            return GetNextCell(v.x, v.y);
        }

        public Cell GetNextCell(int x, int y)
        {
            int _x = Mathf.Clamp(x, 0, m_width - 1);
            int _y = Mathf.Clamp(y, 0, m_height - 1);
            List<Cell> cells = GetAdjacentCells(_x, _y);
            int rand = UnityEngine.Random.Range(0, cells.Count);
            return cells[rand];
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