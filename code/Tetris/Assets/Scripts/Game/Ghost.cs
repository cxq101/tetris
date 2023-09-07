using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini.Game
{
    public class Ghost : MonoBehaviour
    {
        public static Ghost Instance => s_Instance;
        private static Ghost s_Instance;
        [SerializeField]
        private Vector2Int m_Pos;
        [SerializeField]
        private Brick[] m_Bricks;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            s_Instance = this;
            foreach (var item in m_Bricks)
            {
                var sp = item.gameObject.GetComponent<SpriteRenderer>();
                sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 0.3f);
            }
        }

        public void Transform(Vector2Int[] posArray)
        {
            for (int i = 0; i < m_Bricks.Length; i++)
            {
                m_Bricks[i].Pos = posArray[i];
            }
        }

        public void MoveBy(Vector2Int pos)
        {
            SetPos(m_Pos + pos);
        }

        public void SetPos(Vector2Int pos)
        {
            m_Pos = pos;
            transform.position = new Vector3(pos.x, pos.y, 0);
        }

        public Vector2Int[] PosArray(Vector2Int movePos = new Vector2Int())
        {
            Vector2Int pos = m_Pos + movePos;
            Vector2Int[] vector2Ints = new Vector2Int[m_Bricks.Length];
            for (int i = 0; i < m_Bricks.Length; i++)
            {
                Brick brick = m_Bricks[i];
                vector2Ints[i] = pos + brick.Pos;
            }
            return vector2Ints;
        }


    }

}
