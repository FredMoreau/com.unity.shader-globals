﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Unity.ShaderGlobals.Timeline
{
    [System.Serializable]
    public class AmbientLightControlBehaviour : PlayableBehaviour
    {
        public static Color originalColor = default;
        [ColorUsage(false, true)] public Color color = Color.white;
    }
}