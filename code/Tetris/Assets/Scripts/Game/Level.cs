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

        private bool IsExistAtPos(Vector2Int pos)
        {
            return m_BrickList[pos.x, pos.y] != null;
        }

        private bool CheckFull(out List<int> fullIndexs)
        {
            fullIndexs = new List<int>();
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
                    fullIndexs.Add(h);  
                }
            }
            return fullIndexs.Count > 0;
        }

        #region Editor
        [SerializeField]
        [ContextMenuItem("CreateBrick", nameof(CreateBrick))]
        private Vector2Int m_NewBrickPos;

        [SerializeField]
        private GameObject m_BrickPrefab;

        private void CreateBrick()
        {
            if (IsExistAtPos(m_NewBrickPos))
            {
                Debug.Log("Exist a brick at pos " + m_NewBrickPos);
                return;
            }
            var brick = Instantiate(m_BrickPrefab, transform);
            Brick brickComponent = brick.GetComponent<Brick>();
            brickComponent.Pos = m_NewBrickPos;
            m_BrickList[m_NewBrickPos.x, m_NewBrickPos.y] = brickComponent;
        }

        private void OnValidate()
        {
            Generate();
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
                            Destroy(brick.gameObject);
                            m_BrickList[i, j] = null;
                        }
                    }
                }
            }
        }
        #endregion
    }
}
