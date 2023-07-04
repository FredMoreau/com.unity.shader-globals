using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Unity.ShaderGlobals.Timeline
{
    public class GlobalFloatControlMixerBehaviour : PlayableBehaviour
    {
        public string referenceName { get; set; }
        public float originalValue { get; set; }

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
            Shader.SetGlobalFloat(referenceName, originalValue);
        }
    }
}