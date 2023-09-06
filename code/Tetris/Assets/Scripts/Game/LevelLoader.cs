using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini.Game
{
    public class LevelLoader : MonoBehaviour
    {
        public static LevelLoader Instance => s_Instance;
        private static LevelLoader s_Instance;
        [SerializeField]
        private GameObject[] m_PreloadedAssets;

        private GameObject[] m_InstantiateObjects;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            s_Instance = this;
        }

        public void Load()
        {
            m_InstantiateObjects = new GameObject[m_PreloadedAssets.Length];
            for (int i = 0; i < m_PreloadedAssets.Length; i++)
            {
                m_InstantiateObjects[i] = Instantiate(m_PreloadedAssets[i]);
            }
        }      
        
        public void Unload()
        {
            for (var i = 0; i < m_InstantiateObjects.Length; i++)
            {
                Destroy(m_InstantiateObjects[i]);
                m_InstantiateObjects[i] = null;
            }
        }
    }
}
