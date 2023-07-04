using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Unity.ShaderGlobals.Timeline
{
    public class GlobalFloatControlAsset : PlayableAsset, IPropertyPreview
    {
        public GlobalFloatControlBehaviour template;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<GlobalFloatControlBehaviour>.Create(graph, template);
            return playable;
        }

        public void GatherProperties(PlayableDirector director, IPropertyCollector driver)
        {

        }
    }
}