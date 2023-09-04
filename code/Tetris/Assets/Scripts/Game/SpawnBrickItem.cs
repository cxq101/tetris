using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini.Game
{
    [CreateAssetMenu(fileName =nameof(SpawnBrickItem), menuName ="Runner/"+nameof(SpawnBrickItem))]
    public class SpawnBrickItem : ScriptableObject
    {
        public int Weight;
        public GameObject BrickPref;
    }
}
