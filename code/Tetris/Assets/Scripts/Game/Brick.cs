using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

using Mini.Shared;
namespace Mini.Game
{
    public class Brick : MonoBehaviour
    {
        [SerializeField]
        private Vector2Int m_Pos;
        private FadeTool m_FadeTool;

        public Vector2Int Pos
        {
            get { return m_Pos; }
            set
            {
                m_Pos = value;
                transform.localPosition = new Vector3(value.x, value.y, transform.localPosition.z);
            }
        }

        void Awake()
        {
            m_FadeTool = GetComponent<FadeTool>();
        }
        
        public void Bling()
        {
            m_FadeTool.Bling();
        }
#if UNITY_EDITOR
        private void OnValidate()
        {
            transform.localPosition = new Vector3(Pos.x, Pos.y, transform.localPosition.z);
        }
#endif
    }
}
