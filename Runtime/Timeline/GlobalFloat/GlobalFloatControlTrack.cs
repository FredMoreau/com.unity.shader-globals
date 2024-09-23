#if UNITY_EDITOR
using System.ComponentModel;
#endif
using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Unity.ShaderGlobals.Timeline
{
#if UNITY_EDITOR
    [DisplayName("Shader Globals/Float Track")]
#endif
    [Serializable]
    [TrackClipType(typeof(GlobalFloatControlAsset))]
    [TrackBindingType(null)]
    [ExcludeFromPreset]
    public class GlobalFloatControlTrack : TrackAsset
    {
        [SerializeField] string _referenceName = "_customFloat";
        [SerializeField] bool _restoreOriginalValue = false;

        GlobalFloatControlMixerBehaviour m_ActivationMixer;

        public string referenceName
        {
            get { return _referenceName; }
            set { _referenceName = value; UpdateTrackReference(); }
        }

        public bool restoreOriginalValue
        {
            get { return _restoreOriginalValue; }
            set { _restoreOriginalValue = value; UpdateTrackMode(); }
        }

        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            var mixer = ScriptPlayable<GlobalFloatControlMixerBehaviour>.Create(graph, inputCount);
            m_ActivationMixer = mixer.GetBehaviour();

            UpdateTrackMode();
            UpdateTrackReference();

            return mixer;
        }

        internal void UpdateTrackMode()
        {
            if (m_ActivationMixer != null)
                m_ActivationMixer.restoreOriginalValue = _restoreOriginalValue;
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
