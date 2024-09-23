using UnityEngine;
using UnityEngine.Playables;

namespace Unity.ShaderGlobals.Timeline
{
    [System.Serializable]
    public class GlobalColorControlBehaviour : PlayableBehaviour
    {
        [ColorUsage(false, true)] public Color color = Color.white;
    }
}
