using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini.Shared
{
    public class BootLoader : MonoBehaviour
    {
        [SerializeField]
        private SequenceManager m_SequenceManager;
        // Start is called before the first frame update
        void Start()
        {
            Instantiate(m_SequenceManager);
            SequenceManager.Instance.Initialize();  
        }
    }
}
