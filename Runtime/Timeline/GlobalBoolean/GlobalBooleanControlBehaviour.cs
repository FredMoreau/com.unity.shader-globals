using UnityEngine;
using UnityEngine.Playables;

namespace Unity.ShaderGlobals.Timeline
{
    class GlobalBooleanControlBehaviour : PlayableBehaviour
    {
        GlobalBooleanControlTrack.PostPlaybackState m_PostPlaybackState;

        public string referenceName { get; set; }
        public float originalValue { get; set; }

        public static ScriptPlayable<GlobalBooleanControlBehaviour> Create(PlayableGraph graph, int inputCount)
        {
            return ScriptPlayable<GlobalBooleanControlBehaviour>.Create(graph, inputCount);
        }

        public GlobalBooleanControlTrack.PostPlaybackState postPlaybackState
        {
            get { return m_PostPlaybackState; }
            set { m_PostPlaybackState = value; }
        }

        public override void OnPlayableDestroy(Playable playable)
        {
            switch (m_PostPlaybackState)
            {
                case GlobalBooleanControlTrack.PostPlaybackState.Active:
                    Shader.SetGlobalFloat(referenceName, 1f);
                    break;
                case GlobalBooleanControlTrack.PostPlaybackState.Inactive:
                    Shader.SetGlobalFloat(referenceName, 0f);
                    break;
                case GlobalBooleanControlTrack.PostPlaybackState.Revert:
                    Shader.SetGlobalFloat(referenceName, originalValue);
                    break;
                case GlobalBooleanControlTrack.PostPlaybackState.LeaveAsIs:
                default:
                    break;
            }
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            int inputCount = playable.GetInputCount();
            bool hasInput = false;
            for (int i = 0; i < inputCount; i++)
            {
                if (playable.GetInputWeight(i) > 0)
                {
                    hasInput = true;
                    break;
                }
            }

            Shader.SetGlobalFloat(referenceName, hasInput ? 1f : 0f);
        }
    }
}
