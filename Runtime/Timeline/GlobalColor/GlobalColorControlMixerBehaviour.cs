using UnityEngine;
using UnityEngine.Playables;

namespace Unity.ShaderGlobals.Timeline
{
    public class GlobalColorControlMixerBehaviour : PlayableBehaviour
    {
        public string referenceName { get; set; }
        public Color originalColor { get; set; }

        bool _restoreOriginalValue = false;
        public bool restoreOriginalValue
        {
            get { return _restoreOriginalValue; }
            set { _restoreOriginalValue = value; }
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            Color finalColor = Color.black;

            int inputCount = playable.GetInputCount();

            for (int i = 0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);
                ScriptPlayable<GlobalColorControlBehaviour> inputPlayable = (ScriptPlayable<GlobalColorControlBehaviour>)playable.GetInput(i);
                GlobalColorControlBehaviour input = inputPlayable.GetBehaviour();

                // Use the above variables to process each frame of this playable.
                finalColor += input.color * inputWeight;
            }

            Shader.SetGlobalColor(referenceName, finalColor);
        }

        public override void OnPlayableDestroy(Playable playable)
        {
            base.OnPlayableDestroy(playable);

            if (_restoreOriginalValue)
                Shader.SetGlobalColor(referenceName, originalColor);
        }
    }
}
