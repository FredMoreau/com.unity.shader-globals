using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Unity.ShaderGlobals.Timeline
{
    [TrackClipType(typeof(GlobalFloatControlAsset))]
    [TrackBindingType(null)]
    public class GlobalFloatControlTrack : TrackAsset
    {
        [SerializeField] string _referenceName = "_customFloat";
        GlobalFloatControlMixerBehaviour m_ActivationMixer;

        public string referenceName
        {
            get { return _referenceName; }
            set { _referenceName = value; UpdateTrackReference(); }
        }

        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            var mixer = ScriptPlayable<GlobalFloatControlMixerBehaviour>.Create(graph, inputCount);
            m_ActivationMixer = mixer.GetBehaviour();

            UpdateTrackReference();

            return mixer;
        }

        internal void UpdateTrackReference()
        {
            if (m_ActivationMixer != null)
            {
                m_ActivationMixer.referenceName = referenceName;
                m_ActivationMixer.originalValue = Shader.GetGlobalFloat(referenceName);
            }
        }
    }
}