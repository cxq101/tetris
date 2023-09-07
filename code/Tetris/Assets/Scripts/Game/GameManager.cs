using System.Collections;
using UnityEngine;
using Mini.Core;

namespace Mini.Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance => s_Instance;
        private static GameManager s_Instance;
        [SerializeField]
        private AbstractGameEvent m_LoseEvent;

        private int m_Score;
        private BrickGroup m_CurrentBrickGroup;
        private Spawner m_Spawner;

        [SerializeField]
        private float m_LoopTime = 1.0f;
        [SerializeField]
        private float m_HoldTime = 1.0f;
        private float m_CurrLoopTime;
        private float m_CurrHoldTime;
        private bool m_IsRunning;
        private bool m_IsFading;

        public Vector2Int SpawnPos => new Vector2Int(Level.Instance.Width / 2, Level.Instance.Height);
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            s_Instance = this;
            m_Spawner = GetComponent<Spawner>();
        }

        public void StartGame()
        {
            Level.Instance.Generate();
            m_IsRunning = true;
        }

        private void Update()
        {
            if (!m_IsRunning) return;
            if (m_CurrHoldTime > 0)
            {
                m_CurrHoldTime -= Time.deltaTime;
                return;
            }
            if (m_CurrLoopTime > 0)
            {
                m_CurrLoopTime -= Time.deltaTime;
                return;
            }
            Step();
            m_CurrLoopTime = m_LoopTime;
        }

        [ContextMenu(nameof(CheckScoreNow))]
        private void CheckScoreNow()
        {
            if (!m_IsFading && Level.Instance.CheckFull(out int[] fullLines))
            {
                Level.Instance.Bling(fullLines);
                StartCoroutine(BlingAndRemove(fullLines));
            }
        }

        private IEnumerator BlingAndRemove(int[] fullLines)
        {
            m_IsFading = true;
            yield return new WaitForSeconds(0.6f);
            Level.Instance.RomoveLinesAndDrop(fullLines);
            // Add Score
            m_Score += fullLines.Length;
            UIManager.Instance.GetView<Hud>().SetScore(m_Score);
            m_IsFading = false;
            Debug.Log($"Score add: {fullLines.Length}  total: {m_Score}");
        }

        private void Step()
        {
            if (m_CurrentBrickGroup == null)
            {
                Spawn();
            }
            else
            {
                if (!TryMoveBy(Vector2Int.down))
                {
                    if (!TryStopMoveAddToLevel())
                    {
                        Debug.Log("====Game Over====");
                        m_LoseEvent.Raise();
                    }
                }
            }
            CheckScoreNow();
        }

        private bool TryStopMoveAddToLevel()
        {
            Vector2Int[] posArr = m_CurrentBrickGroup.PosArray();
            if (Level.Instance.IsValid(posArr, false))
            {
                Level.Instance.AddBrickGroup(posArr);
                Destroy(m_CurrentBrickGroup.gameObject);
                m_CurrentBrickGroup = null;
                Ghost.Instance.gameObject.SetActive(false);
                return true;
            }
            return false;
        }

        public void OnInputMove(Vector2Int pos)
        {
            if (TryMoveBy(pos))
            {
                m_CurrHoldTime = m_HoldTime;
            }
        }

        public void OnInputTransform()
        {
            if (TryTransForm())
            {
                m_CurrHoldTime = m_HoldTime;
            }
        }

        private bool TryTransForm()
        {
            if (m_CurrentBrickGroup == null) return false;
            if (Level.Instance.IsValid(m_CurrentBrickGroup.NextTransformPosArray()))
            {
                m_CurrentBrickGroup.TransformNext();
                Ghost.Instance.Transform(m_CurrentBrickGroup.BrickPosArray());
                AdaptGhostPos();
                return true;
            }
            return false;
        }

        private bool TryMoveBy(Vector2Int pos)
        {
            if (m_CurrentBrickGroup == null) return false;
            if (Level.Instance.IsValid(m_CurrentBrickGroup.PosArray(pos)))
            {
                m_CurrentBrickGroup.MoveBy(pos);
                AdaptGhostPos();
                return true;
            }
            return false;
        }

        private void Spawn()
        {
            GameObject gameObject = m_Spawner.Generate();
            gameObject.transform.parent = transform;
            BrickGroup brickGroup = gameObject.GetComponent<BrickGroup>();
            m_CurrentBrickGroup = brickGroup;
            m_CurrentBrickGroup.SetPos(SpawnPos);
            
            Ghost.Instance.gameObject.SetActive(true);
            Ghost.Instance.Transform(m_CurrentBrickGroup.BrickPosArray());
            AdaptGhostPos();
        }

        private void AdaptGhostPos()
        {
            Vector2Int pos = new Vector2Int();
            while (Level.Instance.IsValid(m_CurrentBrickGroup.PosArray(pos)))
            {
                pos.y -= 1;
            }
            pos.y++;
            pos.y = pos.y > 0 ? 0 : pos.y;
            Ghost.Instance.SetPos(pos + m_CurrentBrickGroup.Pos);
        }
    }
}

