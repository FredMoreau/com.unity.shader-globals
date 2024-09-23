float4 _WindVector;
float4 _WindValues;

void Wind_float(out float3 Position, out float3 Direction, out float Radius, out float Strength, out float Turbulence, out float PulseMagnitude, out float PulseFrequency)
{
    #if defined(SHADERGRAPH_PREVIEW)
    Position = float3(-1, 0, -1);
    Direction = float3(.7, .2, .7);
    Radius = 1;
    Strength = 1;
    Turbulence = 1;
    PulseMagnitude = 0.5;
    PulseFrequency = 0.01;
    #else
    Position = _WindVector.xyz;
    Direction = _WindVector.xyz;
    Radius = _WindVector.w;
    Strength = _WindValues.x;
    Turbulence = _WindValues.y;
    PulseMagnitude = _WindValues.z;
    PulseFrequency = _WindValues.w;
    #endif
}