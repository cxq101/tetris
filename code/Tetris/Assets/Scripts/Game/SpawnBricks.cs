using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini.Game
{
    [CreateAssetMenu(fileName =nameof(SpawnBricks), menuName ="Runner/" + nameof(SpawnBricks))]
    public class SpawnBricks : ScriptableObject
    {
        public SpawnBrickItem[] Bricks;
    }
}
