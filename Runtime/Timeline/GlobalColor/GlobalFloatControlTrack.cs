using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Unity.ShaderGlobals.Timeline
{
    [TrackClipType(typeof(GlobalColorControlAsset))]
    [TrackBindingType(null)]
    public class GlobalColorControlTrack : TrackAsset
    {
        [SerializeField] string _referenceName = "_customFloat";
        GlobalColorControlMixerBehaviour m_ActivationMixer;

        public string referenceName
        {
            get { return _referenceName; }
            set { _referenceName = value; UpdateTrackReference(); }
        }

        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            var mixer = ScriptPlayable<GlobalColorControlMixerBehaviour>.Create(graph, inputCount);
            m_ActivationMixer = mixer.GetBehaviour();

            UpdateTrackReference();

            return mixer;
        }

        internal void UpdateTrackReference()
        {
            if (m_ActivationMixer != null)
            {
                m_ActivationMixer.referenceName = referenceName;
                m_ActivationMixer.originalColor = Shader.GetGlobalColor(referenceName);
            }
        }
    }
}