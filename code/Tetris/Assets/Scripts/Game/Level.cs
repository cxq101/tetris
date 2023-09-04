using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini.Game
{
    public class Level : MonoBehaviour
    {
        [SerializeField, Range(1, 50)]
        private int m_Width = 5;
        [SerializeField, Range(1, 50)]
        private int m_Height = 10;
        [SerializeField]
        private Transform m_OutlineTransform;

        private Brick[,] m_BrickList;

        [ContextMenu(nameof(Generate))]
        public void Generate()
        {
            if (!m_OutlineTransform) return;
            m_BrickList = new Brick[m_Width, m_Height];

            m_OutlineTransform.localScale = new Vector3(m_Width, m_Height, 1);
            m_OutlineTransform.position = new Vector3(m_Width / 2, m_Height / 2, m_OutlineTransform.position.z);

            Camera.main.orthographicSize = m_Height / 2 + 2;
            Camera.main.transform.position = new Vector3(m_Width / 2, m_Height / 2, Camera.main.transform.position.z);
        }

        public void AddBrickGroup(Vector2Int[] posArr)
        {
            foreach (var pos in posArr)
            {
                CreateBrick(pos);
            }
        }

        public bool IsValid(Vector2Int[] posArr)
        { 
            return posArr.All(e => IsExistPos(e) && !IsExistAtPos(e));
        }
        private bool IsExistPos(Vector2Int pos)
        {
            return IsInRange(pos.x, 0, m_Width - 1) && IsInRange(pos.y, 0, m_Height - 1);
        }

        private bool IsInRange(int value, int min, int max)
        {
            return min <= value && value <= max;    
        }
        
        private bool IsExistAtPos(Vector2Int pos)
        {
            return m_BrickList[pos.x, pos.y] != null;
        }    
        private bool IsExistAtPos(int posX, int posY)
        {
            return m_BrickList[posX, posY] != null;
        }

        public bool CheckFull(out int[] fullIndexs)
        {
            List<int> tmpList = new List<int>();
            for (int h = 0; h < m_BrickList.GetLength(1); h++)
            {
                bool isFull = true;
                for (int w = 0; w < m_BrickList.GetLength(0); w++)
                {
                    var brick = m_BrickList[w, h];
                    if (brick == null)
                    {
                        isFull = false;
                        break;
                    }
                }
                if (isFull)
                {
                    tmpList.Add(h);
                }
            }
            fullIndexs = tmpList.ToArray();
            return tmpList.Count > 0;
        }

        [SerializeField]
        private GameObject m_BrickPrefab;
        private void CreateBrick(Vector2Int pos)
        {
            if (IsExistAtPos(pos))
            {
                Debug.Log("Exist a brick at pos " + pos);
                return;
            }
            var brick = Instantiate(m_BrickPrefab, transform);
            Brick brickComponent = brick.GetComponent<Brick>();
            brickComponent.Pos = pos;
            m_BrickList[pos.x, pos.y] = brickComponent;
        }

        public void RomoveLinesAndDrop(int[] lines)
        {
            int width = m_BrickList.GetLength(0);
            int height = m_BrickList.GetLength(1);
            for (int i = 0; i < lines.Length; i++)
            {
                int h = lines[i];
                for (int w = 0; w < width; w++)
                {
                    if (IsExistAtPos(w, h))
                    {
                        Destroy(m_BrickList[w, h].gameObject);
                        m_BrickList[w, h] = null;
                    }
                }
            }

            for (int h = 0; h < height; h++)
            {
                bool isExist = false;
                int dropDownSpace = 0;
                for (int i = 0; i < lines.Length; i++)
                {
                    int line = lines[i];
                    if (line >= h)
                    {
                        isExist = line == h;
                        break;
                    }
                    else
                    {
                        dropDownSpace++;
                    }
                }
                if (isExist) continue;
                for (int i = 0; i < width; i++)
                {
                    if (m_BrickList[i, h])
                    {
                        m_BrickList[i, h].Pos = new Vector2Int(m_BrickList[i, h].Pos.x, m_BrickList[i, h].Pos.y - dropDownSpace);
                        m_BrickList[i, h - dropDownSpace] = m_BrickList[i, h];
                    }
                }
            }
        }

        [ContextMenu(nameof(Clear))]
        private void Clear()
        {
            if (m_BrickList != null)
            {
                for (int i = 0; i < m_BrickList.GetLength(0); i++)
                {
                    for (int j = 0; j < m_BrickList.GetLength(1); j++)
                    {
                        var brick = m_BrickList[i, j];
                        if (brick)
                        {
                            DestroyImmediate(brick.gameObject);
                            m_BrickList[i, j] = null;
                        }
                    }
                }
            }
        }
    }
}
