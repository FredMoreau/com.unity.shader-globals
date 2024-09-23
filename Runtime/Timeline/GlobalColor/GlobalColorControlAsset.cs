#if UNITY_EDITOR
using System.ComponentModel;
#endif
using UnityEngine;
using UnityEngine.Playables;

namespace Unity.ShaderGlobals.Timeline
{
#if UNITY_EDITOR
    [DisplayName("Color Clip")]
#endif
    public class GlobalColorControlAsset : PlayableAsset
    {
        public GlobalColorControlBehaviour template;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<GlobalColorControlBehaviour>.Create(graph, template);
            return playable;
        }
    }
}
