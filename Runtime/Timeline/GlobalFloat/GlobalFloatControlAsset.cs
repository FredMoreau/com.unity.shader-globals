#if UNITY_EDITOR
using System.ComponentModel;
#endif
using UnityEngine;
using UnityEngine.Playables;

namespace Unity.ShaderGlobals.Timeline
{
#if UNITY_EDITOR
    [DisplayName("Float Clip")]
#endif
    public class GlobalFloatControlAsset : PlayableAsset
    {
        public GlobalFloatControlBehaviour template;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<GlobalFloatControlBehaviour>.Create(graph, template);
            return playable;
        }
    }
}
