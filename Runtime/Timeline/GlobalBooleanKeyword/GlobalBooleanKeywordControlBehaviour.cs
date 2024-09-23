using UnityEngine;
using UnityEngine.Playables;

namespace Unity.ShaderGlobals.Timeline
{
    class GlobalBooleanKeywordControlBehaviour : PlayableBehaviour
    {
        GlobalBooleanKeywordControlTrack.PostPlaybackState m_PostPlaybackState;

        public string referenceName { get; set; }
        public bool originalValue { get; set; }

        public static ScriptPlayable<GlobalBooleanKeywordControlBehaviour> Create(PlayableGraph graph, int inputCount)
        {
            return ScriptPlayable<GlobalBooleanKeywordControlBehaviour>.Create(graph, inputCount);
        }

        public GlobalBooleanKeywordControlTrack.PostPlaybackState postPlaybackState
        {
            get { return m_PostPlaybackState; }
            set { m_PostPlaybackState = value; }
        }

        public override void OnPlayableDestroy(Playable playable)
        {
            switch (m_PostPlaybackState)
            {
                case GlobalBooleanKeywordControlTrack.PostPlaybackState.Active:
                    Shader.EnableKeyword(referenceName);
                    break;
                case GlobalBooleanKeywordControlTrack.PostPlaybackState.Inactive:
                    Shader.DisableKeyword(referenceName);
                    break;
                case GlobalBooleanKeywordControlTrack.PostPlaybackState.Revert:
                    if (originalValue)
                        Shader.EnableKeyword(referenceName);
                    else
                        Shader.DisableKeyword(referenceName);
                    break;
                case GlobalBooleanKeywordControlTrack.PostPlaybackState.LeaveAsIs:
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

            if (hasInput)
                Shader.EnableKeyword(referenceName);
            else
                Shader.DisableKeyword(referenceName);
        }
    }
}
