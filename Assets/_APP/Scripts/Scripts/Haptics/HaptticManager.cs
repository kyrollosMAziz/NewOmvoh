using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Bhaptics.SDK2;
using Unity.VisualScripting;
using UnityEngine;

public class HaptticManager : ProjectContextSingleton<HaptticManager>
{
    [SerializeField] private HapticsController hapticsController;
    private bool _cancel;
    private CancellationTokenSource _cancellationTokenSource;
    private bool cancelEvent;


    public void PlayHapticFeedback(string eventName)
    {
        StartCoroutine(CallHapticWithTime(10f, 2f, eventName));
    }

    public void PlayHapticLoop(string eventName)
    {
        cancelEvent = false;
        StartCoroutine(LoopingHaptic(eventName));
    }

    public void StopHapticLoop()
    {
        cancelEvent = true;
    }

    private IEnumerator LoopingHaptic(string eventName)
    {
        while (!cancelEvent)
        {
            if (!hapticsController.IsPlaying())
                hapticsController.Play(eventName);
            yield return null;
        }
        hapticsController.StopHapticEvent();
    }
    
    /// <summary>
    /// CallHaptic with total time and loopTime the vibration again
    /// </summary>
    /// <param name="Time"></param>
    /// <param name="loopTime"></param>
    /// <returns></returns>
    public IEnumerator CallHapticWithTime(float Time, float loopTime, string eventName)
    {
        float currentTime = 0;
        while (currentTime <= Time)
        {
            if (!hapticsController.IsPlaying())
                hapticsController.Play(eventName);
            yield return new WaitForSeconds(loopTime);
            currentTime += loopTime;
        }
    }

 
}