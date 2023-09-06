using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mini.Core;

namespace Mini.Shared
{
    [CreateAssetMenu(fileName = nameof(PauseEvent), menuName = "Runner/" + nameof(PauseEvent))]
    public class PauseEvent : AbstractGameEvent
    {
        public override void Reset()
        {
        }
    }
}

