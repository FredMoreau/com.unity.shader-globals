using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
#if HDRP_ENABLED
using UnityEngine.Rendering.HighDefinition;
#elif URP_ENABLED
using UnityEngine.Rendering.Universal;
#endif

namespace Unity.ShaderGlobals.Volume
{
    [ExecuteAlways]
    public sealed class VolumeComponentUpdater : MonoBehaviour
    {
        VolumeStack previousStack;
        ShaderGlobalsVolumeComponent shaderGlobalsVolumeComponent;
        new Camera camera;

        void LateUpdate()
        {
            camera = Camera.main;

            if (!camera)
                return;
            VolumeStack stack;

#if HDRP_ENABLED
            stack = HDCamera
                .GetOrCreate(camera)
                .volumeStack;
#elif URP_ENABLED
            stack = camera
                .GetComponent<UniversalAdditionalCameraData>()
                .volumeStack;
#endif

            if (stack == null)
                return;

            if (stack != previousStack)
            {
                previousStack = stack;
                shaderGlobalsVolumeComponent = stack.GetComponent<ShaderGlobalsVolumeComponent>();
            }

            shaderGlobalsVolumeComponent.Update();
        }
    }
}