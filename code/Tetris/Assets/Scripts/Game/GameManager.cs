using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini.Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance => s_Instance;
        private static GameManager s_Instance;

        [SerializeField]
        private Level m_Level;
        private int m_Score;
        private BrickGroup m_CurrentBrickGroup;
        [SerializeField]
        private SpawnBrickItem[] m_SpawnBricks;
        [SerializeField]
        private Vector2Int m_SpawnPos;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            s_Instance = this;
        }

        private void Start()
        {
            m_Level.Generate();
            StartGameLoop();
        }

        [ContextMenu(nameof(CheckScoreNow))]
        private void CheckScoreNow()
        {
            if (m_Level.CheckFull(out int[] fullLines))
            {
                m_Level.RomoveLinesAndDrop(fullLines);
                // Add Score
                m_Score += fullLines.Length;
                // Hud change
            }
        }

        private void StartGameLoop()
        {
            InvokeRepeating(nameof(Step), 0.0f, 1);
        }

        private void StopGameLoop()
        {
            CancelInvoke(nameof(Step));
        }

        private void Step()
        {
            CheckSpawn();
            TryMoveDown();
        }

        public void OnInputMove(int direction)
        {
            Move(direction);
        }

        private void Move(int direction)
        {
            Vector2Int pos = direction == 1 ? Vector2Int.right : Vector2Int.left;   
            TryMoveBy(pos);
        }

        private bool TryMoveBy(Vector2Int pos)
        {
            if (m_CurrentBrickGroup == null) return false;
            if (m_Level.IsValid(m_CurrentBrickGroup.PosArray(pos)))
            {
                m_CurrentBrickGroup.MoveBy(pos);
                return true;
            }
            return false;
        }

        private void CheckSpawn()
        {
            if (m_CurrentBrickGroup != null) return;
            Spawn();
        }        
        
        private void TryMoveDown()
        {
            if(!TryMoveBy(Vector2Int.down))
            {
                Vector2Int[] posArr = m_CurrentBrickGroup.PosArray(); 
                m_Level.AddBrickGroup(posArr);
                Destroy(m_CurrentBrickGroup.gameObject);
                m_CurrentBrickGroup = null;
            }
        }
        private void Spawn()
        {
            GameObject randomPrefab = RandomBrickGroup();
            GameObject gameObject = Instantiate(randomPrefab);
            BrickGroup brickGroup = gameObject.GetComponent<BrickGroup>();
            Vector2Int[] posArr = brickGroup.PosArray(m_SpawnPos);
            if (m_Level.IsValid(posArr))
            {
                m_CurrentBrickGroup = brickGroup;
                m_CurrentBrickGroup.SetPos(m_SpawnPos);
            }
            else
            {
                Destroy(gameObject);
                Debug.Log("Game Over========");
            }
        }

        private GameObject RandomBrickGroup()
        {
            int totalWeight = m_SpawnBricks.Sum(e => e.Weight);
            int randomWeight = Random.Range(0, totalWeight);
            SpawnBrickItem target = null;
            int countWeight = 0;
            foreach (var element in m_SpawnBricks)
            {
                countWeight+= element.Weight;
                if (countWeight <= randomWeight)
                {
                    target = element;
                    break;
                }
            }
            target = m_SpawnBricks.Last();
            
            return target.BrickPref;
        }

    }
}

