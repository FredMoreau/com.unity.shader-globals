using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Unity.ShaderGlobals.Timeline
{
    public class GlobalColorControlAsset : PlayableAsset, IPropertyPreview
    {
        public GlobalColorControlBehaviour template;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<GlobalColorControlBehaviour>.Create(graph, template);
            return playable;
        }

        public void GatherProperties(PlayableDirector director, IPropertyCollector driver)
        {

        }
    }
}