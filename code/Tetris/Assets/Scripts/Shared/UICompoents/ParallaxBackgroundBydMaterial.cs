using UnityEngine;

namespace Mini.Shared.UICompoents
{
    public class ParallaxBackground2 : MonoBehaviour
    {
        [SerializeField]
        private Vector2 m_Offset;
        [SerializeField]
        private Vector2 m_ParallaxMultiplier = Vector2.one;

        private Transform m_CameraTransform;
        private SpriteRenderer m_SpriteRenderer;

        private void Start()
        {
            m_CameraTransform = Camera.main.transform;
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            transform.position = new Vector3(m_CameraTransform.position.x + m_Offset.x, m_CameraTransform.position.y + m_Offset.y, 0);
            Vector2 offset = m_SpriteRenderer.material.GetTextureOffset("_MainTex");
            offset += m_ParallaxMultiplier * Time.deltaTime;
            m_SpriteRenderer.material.SetTextureOffset("_MainTex", offset);
        }
    }
}
