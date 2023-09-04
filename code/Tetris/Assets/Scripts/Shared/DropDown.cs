using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini.Shared
{
    public class DropDown : MonoBehaviour
    {
        [SerializeField]
        private string m_PlayerLayerString;
        [SerializeField]
        private string m_PlatformLayerString;

        // Update is called once per frame
        void Update()
        {
            bool isInputDown = Input.GetAxis("Vertical") < 0;
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(m_PlayerLayerString), LayerMask.NameToLayer(m_PlatformLayerString), isInputDown);
            
            
        }
    }

}
