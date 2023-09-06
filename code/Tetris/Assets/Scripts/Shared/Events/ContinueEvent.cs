using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mini.Core;

namespace Mini.Shared
{
    [CreateAssetMenu(fileName =nameof(ContinueEvent), menuName = "Runner/"+ nameof(ContinueEvent))]
    public class ContinueEvent : AbstractGameEvent
    {
        public override void Reset()
        {
        }
    }

}
