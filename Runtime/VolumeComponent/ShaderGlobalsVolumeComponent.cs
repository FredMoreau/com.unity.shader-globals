using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Unity.ShaderGlobals.Volume
{
    [Serializable, VolumeComponentMenu("Shader/Globals")]
    public class ShaderGlobalsVolumeComponent : VolumeComponent
    {
        public VolumeParameter<float> myCustomProperty = new VolumeParameter<float>();
        public VolumeParameter<ShaderGlobals> globals = new VolumeParameter<ShaderGlobals>();
        public VolumeParameter<List<VolumeParameter<ShaderGlobals.ShaderGlobal<float>>>> myFloats = new VolumeParameter<List<VolumeParameter<ShaderGlobals.ShaderGlobal<float>>>>();

        public void Update()
        {
            if (myCustomProperty.overrideState)
            {
                Shader.SetGlobalFloat("_MyCustomProperty", myCustomProperty.value);
            }

            if (globals.overrideState)
            {
                globals.value.SetGlobals();
            }

            if (myFloats.overrideState)
            {
                foreach (var floatParam in myFloats.value)
                {
                    Shader.SetGlobalFloat(floatParam.value.referenceName, floatParam.value.value);
                }
            }
        }
    }
}