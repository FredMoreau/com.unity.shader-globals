﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Unity.ShaderGlobals.Timeline
{
    public class AmbientLightControlMixerBehaviour : PlayableBehaviour
    {
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            Color finalColor = Color.black;

            int inputCount = playable.GetInputCount();

            for (int i = 0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);
                ScriptPlayable<AmbientLightControlBehaviour> inputPlayable = (ScriptPlayable<AmbientLightControlBehaviour>)playable.GetInput(i);
                AmbientLightControlBehaviour input = inputPlayable.GetBehaviour();

                // Use the above variables to process each frame of this playable.
                finalColor += input.color * inputWeight;
            }

            RenderSettings.ambientLight = finalColor;
        }

        public override void OnPlayableDestroy(Playable playable)
        {
            base.OnPlayableDestroy(playable);
            RenderSettings.ambientLight = AmbientLightControlBehaviour.originalColor;
        }
    }
}