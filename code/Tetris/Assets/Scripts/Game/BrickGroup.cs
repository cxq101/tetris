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
        [SerializeField]
        private int m_TransformIndex;
        [SerializeField]
        private Vector2Int[] m_Transforms;
        public int TransformSize => m_Transforms.Length / m_Bricks.Length;
        public Vector2Int Pos => m_Pos;
        public int NextTransformIndex
        {
            get
            { 
                int nextIndex = m_TransformIndex + 1;
                nextIndex = nextIndex >= TransformSize ? 0 : nextIndex;  
                return nextIndex;
            }
        } 
        //{return m_Transforms.Length / m_Bricks.Length }

        public int TransformIndex
        {
            get { return m_TransformIndex; }
            set { 
                m_TransformIndex = value;
                for (int i = 0; i < m_Bricks.Length; i++)
                {
                    Brick brick = m_Bricks[i];
                    brick.Pos = m_Transforms[m_TransformIndex * m_Bricks.Length + i];
                }
            }
        }

        public void Step()
        {

        }

        private void OnEnable()
        {
            Transform(m_TransformIndex);
        }

        public void TransformNext()
        {
            Transform(NextTransformIndex);
        }
        public void Transform(int transformIndex)
        {
            TransformIndex = transformIndex;
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

        public Vector2Int[] NextTransformPosArray()
        {
            int brickCount = m_Bricks.Length;
            int nextTransformIndex = NextTransformIndex;
            Vector2Int[] posArray = new Vector2Int[brickCount];
            for (int i = 0; i < brickCount; i++)
            {
                posArray[i] = m_Pos + m_Transforms[nextTransformIndex * brickCount + i];
            }
            return posArray;
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
        public Vector2Int[] BrickPosArray()
        {
            Vector2Int[] vector2Ints = new Vector2Int[m_Bricks.Length];
            for (int i = 0; i < m_Bricks.Length; i++)
            {
                Brick brick = m_Bricks[i];
                vector2Ints[i] = brick.Pos;
            }
            return vector2Ints;
        }


    }

}
