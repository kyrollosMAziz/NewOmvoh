using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHapticsService 
{
    public void Play(string hapticEvent);

    public void Play(string hapticEvent, float intensity = 1, float duration = 1f, float angleX = 0, float offsetY = 0);
    
    public void StopHapticEvent();
}
