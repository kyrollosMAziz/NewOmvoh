using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// interface for making regular haptic event or with custom values everytime
/// </summary>
public interface IHapticEventType 
{
}

/// <summary>
/// Allows to play event with regular event defined on portal
/// </summary>
public class NormalEventType : IHapticEventType
{
    string hapticEventName;

    public NormalEventType(string hapticEventName)
    {
        this.hapticEventName = hapticEventName;
    }

    public string HapticEventName { get => hapticEventName; set => hapticEventName = value; }
}

/// <summary>
/// Allows to play event with event defined on portal with modified Values
/// </summary>
public class CustomEventType : IHapticEventType
{
    string hapticEventName;
    float intensity = 1;
    float duration = 1f;
    float angleX = 0;
    float offsetY = 0;

    public CustomEventType(string hapticEventName, float intensity=1, float duration =1, float angleX =0, float offsetY = 0)
    {
        this.hapticEventName = hapticEventName;
        this.intensity = intensity;
        this.duration = duration;
        this.angleX = angleX;
        this.offsetY = offsetY;
    }

    public string HapticEventName { get => hapticEventName; set => hapticEventName = value; }
    public float Intensity { get => intensity;}
    public float Duration { get => duration; }
    public float AngleX { get => angleX;}
    public float OffsetY { get => offsetY;}
}
