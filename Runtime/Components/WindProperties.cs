using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.ShaderGlobals.Components
{
    [AddComponentMenu("Shaders/Wind Properties"), RequireComponent(typeof(WindZone))]
    [ExecuteAlways, ExecuteInEditMode]
    public class WindProperties : MonoBehaviour
    {
        string _windDirectionVectorReferenceName = "_WindDirection";
        string _windPositionVectorReferenceName = "_WindPosition";
        string _windFloatValuesReferenceName = "_WindValues";

        float[] windValues = new float[5];

        WindZone _windZone;
        WindZone WindZone
        {
            get
            {
                if (_windZone == null)
                    _windZone = GetComponent<WindZone>();
                return _windZone;
            }
        }

        private void Update()
        {
            switch (WindZone.mode)
            {
                case WindZoneMode.Directional:
                    Shader.SetGlobalVector(_windDirectionVectorReferenceName, transform.forward);
                    break;
                case WindZoneMode.Spherical:
                    Shader.SetGlobalVector(_windPositionVectorReferenceName, transform.position);
                    break;
            }

            windValues[0] = WindZone.radius;
            windValues[1] = WindZone.windMain;
            windValues[2] = WindZone.windTurbulence;
            windValues[3] = WindZone.windPulseMagnitude;
            windValues[4] = WindZone.windPulseFrequency;

            Shader.SetGlobalFloatArray(_windFloatValuesReferenceName, windValues);
        }
    }
}