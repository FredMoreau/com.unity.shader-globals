#if UNITY_EDITOR
using System.ComponentModel;
#endif
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

namespace Unity.ShaderGlobals.Timeline
{
#if UNITY_EDITOR
    [DisplayName("Boolean Keyword Clip")]
#endif
    class GlobalBooleanKeywordControlAsset : PlayableAsset, ITimelineClipAsset
    {
        public ClipCaps clipCaps { get { return ClipCaps.None; } }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            return Playable.Create(graph);
        }
    }
}
