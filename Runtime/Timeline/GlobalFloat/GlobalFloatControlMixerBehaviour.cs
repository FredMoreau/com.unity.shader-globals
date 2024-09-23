using UnityEngine;
using UnityEngine.Playables;

namespace Unity.ShaderGlobals.Timeline
{
    public class GlobalFloatControlMixerBehaviour : PlayableBehaviour
    {
        public string referenceName { get; set; }
        public float originalValue { get; set; }

        bool _restoreOriginalValue = false;
        public bool restoreOriginalValue
        {
            get { return _restoreOriginalValue; }
            set { _restoreOriginalValue = value; }
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            float finalValue = 0;

            int inputCount = playable.GetInputCount();

            for (int i = 0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);
                ScriptPlayable<GlobalFloatControlBehaviour> inputPlayable = (ScriptPlayable<GlobalFloatControlBehaviour>)playable.GetInput(i);
                GlobalFloatControlBehaviour input = inputPlayable.GetBehaviour();

                // Use the above variables to process each frame of this playable.
                finalValue += input.value * inputWeight;
            }

            Shader.SetGlobalFloat(referenceName, finalValue);
        }

        public override void OnPlayableDestroy(Playable playable)
        {
            base.OnPlayableDestroy(playable);

            if (_restoreOriginalValue)
                Shader.SetGlobalFloat(referenceName, originalValue);
        }
    }
}
