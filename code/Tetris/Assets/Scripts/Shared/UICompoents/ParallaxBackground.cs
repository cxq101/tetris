using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Mini.Shared.UICompoents
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ParallaxBackground : MonoBehaviour
    {
        [SerializeField]
        // true 为 在X轴无限滚动
        private bool m_IsInfiniteHorzontal;
        [SerializeField]
        // true 为 在Y轴无限滚动
        private bool m_IsInfiniteVertical;
        [SerializeField]
        // 滚动效果系数 [0~1]值越小时差效果越明显
        private Vector2 m_ParallaxEffectMultiplier = Vector2.one;
        [SerializeField]
        private Vector2 m_Offset = Vector2.zero;

        private float m_TextureUnitSizeX;
        private float m_TextureUnitSizeY;
        private Transform m_CameraTransform;
        private Vector3 m_LastCameraPosition;

        private void Awake()
        {
            m_CameraTransform = Camera.main.transform;
        }

        // Start is called before the first frame update
        void Start()
        {
            Sprite sprite = GetComponent<SpriteRenderer>().sprite;
            Texture2D texture = sprite.texture;
            m_TextureUnitSizeX = texture.width / sprite.pixelsPerUnit;
            m_TextureUnitSizeY = texture.height / sprite.pixelsPerUnit;
        }

        private void OnEnable()
        {
            m_LastCameraPosition = m_CameraTransform.position;
            transform.position = new Vector3(m_LastCameraPosition.x + m_Offset.x, m_LastCameraPosition.y + m_Offset.y, 0);
        }

        // Update is called once per frame
        void LateUpdate()
        {
            var positionDelta = m_CameraTransform.position - m_LastCameraPosition;
            if (!Mathf.Approximately(positionDelta.x, 0.0f) || !Mathf.Approximately(positionDelta.y, 0.0f))
            {
                transform.position += new Vector3(positionDelta.x * m_ParallaxEffectMultiplier.x, positionDelta.y * m_ParallaxEffectMultiplier.y, 0);
                m_LastCameraPosition = m_CameraTransform.position;
            }
            if (m_IsInfiniteHorzontal && Mathf.Abs(m_CameraTransform.position.x - transform.position.x) >= m_TextureUnitSizeX)
            {
                int count = Mathf.FloorToInt((m_CameraTransform.position.x - transform.position.x) / m_TextureUnitSizeX);
                transform.position = new Vector3(transform.position.x + (positionDelta.x > 0 ? 1 : -1) * count * m_TextureUnitSizeX, transform.position.y);
            }
            if (m_IsInfiniteVertical && Mathf.Abs(m_CameraTransform.position.y - transform.position.y) >= m_TextureUnitSizeY)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + (positionDelta.y > 0 ? 1 : -1) * m_TextureUnitSizeY);
            }
        }
    }
}
