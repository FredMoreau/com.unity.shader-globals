using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Unity.ShaderGlobals.Timeline
{
    [System.Serializable]
    public class GlobalColorControlBehaviour : PlayableBehaviour
    {
        [ColorUsage(false, true)] public Color color = Color.white;
    }
}