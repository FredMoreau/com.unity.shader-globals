float3 _WindPosition;
float3 _WindDirection;
float _WindValues[5];

void Wind_float(out float3 Position, out float3 Direction, out float Radius, out float Strength, out float Turbulence, out float PulseMagnitude, out float PulseFrequency)
{
    #if defined(SHADERGRAPH_PREVIEW)
    Position = float3(-1, 0, -1);
    Direction = float3(.7, 0, .7);
    Radius = 1;
    Strength = 1;
    Turbulence = 1;
    PulseMagnitude = 0.5;
    PulseFrequency = 0.01;
    #else
    Position = _WindPosition;
    Direction = _WindDirection;
    Radius = _WindValues[0];
    Strength = _WindValues[1];
    Turbulence = _WindValues[2];
    PulseMagnitude = _WindValues[3];
    PulseFrequency = _WindValues[4];
    #endif
}