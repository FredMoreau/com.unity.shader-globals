using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Unity.ShaderGlobals.Components
{
    [AddComponentMenu("Shaders/Wind Properties"), RequireComponent(typeof(WindZone))]
    [ExecuteAlways, ExecuteInEditMode]
    public class WindProperties : MonoBehaviour
    {
        string _windVectorReferenceName = "_WindPosition";
        string _windFloatValuesReferenceName = "_WindValues";

        GlobalKeyword wind_dir_kw, wind_spherical_kw;
        Vector4 windVector, windValues;

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

        private void Start()
        {
            wind_dir_kw = new GlobalKeyword("_WIND_DIRECTIONAL");
            wind_spherical_kw = new GlobalKeyword("_WIND_SPHERICAL");

            switch (WindZone.mode)
            {
                case WindZoneMode.Directional:
                    Shader.SetKeyword(wind_dir_kw, true);
                    Shader.SetKeyword(wind_spherical_kw, false);
                    break;
                case WindZoneMode.Spherical:
                    Shader.SetKeyword(wind_dir_kw, false);
                    Shader.SetKeyword(wind_spherical_kw, true);
                    break;
            }
        }

        private void Update()
        {
            switch (WindZone.mode)
            {
                case WindZoneMode.Directional:
                    windVector = transform.forward;
                    break;
                case WindZoneMode.Spherical:
                    windVector = transform.position;
                    break;
            }

            windVector.w = WindZone.radius;
            windValues.Set(WindZone.windMain,
                WindZone.windTurbulence,
                WindZone.windPulseMagnitude,
                WindZone.windPulseFrequency
                );

            Shader.SetGlobalVector(_windVectorReferenceName, transform.forward);
            Shader.SetGlobalVector(_windFloatValuesReferenceName, windValues);
        }
    }
}