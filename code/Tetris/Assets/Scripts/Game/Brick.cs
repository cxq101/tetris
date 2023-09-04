using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini.Game
{
    public class Brick : MonoBehaviour
    {
        [SerializeField]
        private Vector2Int m_Pos;

        public Vector2Int Pos
        {
            get { return m_Pos; }
            set
            {
                m_Pos = value;
                transform.position = new Vector3(value.x, value.y, transform.position.z);
            }
        }
        private void OnValidate()
        {
            transform.position = new Vector3(Pos.x, Pos.y, transform.position.z);
        }
    }
}
