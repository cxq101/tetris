using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini.Game
{
    public class BrickGroup : MonoBehaviour
    {
        [SerializeField]
        private Vector2Int m_Pos;
        [SerializeField]
        private Brick[] m_Bricks;
        // Update is called once per frame
        public void Step()
        {

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
