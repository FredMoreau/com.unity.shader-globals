#if UNITY_EDITOR
using System.ComponentModel;
#endif
using System;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

namespace Unity.ShaderGlobals.Timeline
{
#if UNITY_EDITOR
    [DisplayName("Shader Globals/Boolean Track")]
#endif
    [Serializable]
    [TrackClipType(typeof(GlobalBooleanControlAsset))]
    [TrackBindingType(null)]
    [ExcludeFromPreset]
    public class GlobalBooleanControlTrack : TrackAsset
    {
        [SerializeField] string _referenceName = "_customBoolean";

        [SerializeField]
        PostPlaybackState m_PostPlaybackState = PostPlaybackState.LeaveAsIs;

        public string referenceName
        {
            get { return _referenceName; }
            set { _referenceName = value; UpdateTrackReference(); }
        }

        GlobalBooleanControlBehaviour m_ActivationMixer;

        public enum PostPlaybackState { Active, Inactive, Revert, LeaveAsIs }

        public PostPlaybackState postPlaybackState
        {
            get { return m_PostPlaybackState; }
            set { m_PostPlaybackState = value; UpdateTrackMode(); }
        }

        /// <inheritdoc/>
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            var mixer = GlobalBooleanControlBehaviour.Create(graph, inputCount);
            m_ActivationMixer = mixer.GetBehaviour();

            UpdateTrackMode();
            UpdateTrackReference();

            return mixer;
        }

        internal void UpdateTrackMode()
        {
            if (m_ActivationMixer != null)
                m_ActivationMixer.postPlaybackState = m_PostPlaybackState;
        }

        /// <inheritdoc/>
        protected override void OnCreateClip(TimelineClip clip)
        {
            clip.displayName = "On";
            base.OnCreateClip(clip);
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
