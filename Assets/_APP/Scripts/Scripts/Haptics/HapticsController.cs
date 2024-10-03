using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bhaptics.SDK2;

public class HapticsController : MonoBehaviour , IHapticsService
{

    public IHapticEventType hapticType;

    BhapticsSettings hapticsSetting = null;
    int currentHapticRequestId =-1;

    public int CurrentHapticEventId { get => currentHapticRequestId; set => currentHapticRequestId = value; }

    private void InitializeSettings()
    {
        hapticsSetting = BhapticsSettings.Instance;
        
    }

    /// <summary>
    /// stop the current haptic event
    /// </summary>
    public void StopHapticEvent()
    {
        //   BhapticsLibrary.StopInt(CurrentHapticEventId);
        BhapticsLibrary.StopAll();
    }

    /// <summary>
    /// Check if any vibration is playing
    /// </summary>
    /// <returns></returns>
    public bool IsPlaying()
    {
        return BhapticsLibrary.IsPlaying();
    }

    /// <summary>
    /// Plays Haptic event with default inputs
    /// </summary>
    /// <param name="hapticEvent"></param>
    public void Play(string hapticEvent)
    {
        if (hapticsSetting == null)
        {
            return;
        }
        hapticType = new NormalEventType(hapticEvent);
        StartCoroutine(TriggerFeedBack<NormalEventType>(hapticType));
    }
    /// <summary>
    /// plays hapticEvent with custom intensity and duration if available
    /// </summary>
    /// <param name="hapticEvent"></param>
    /// <param name="intensity"></param>
    /// <param name="duration"></param>
    /// <param name="angleX"></param>
    /// <param name="offsetY"></param>
    public void Play(string hapticEvent, float intensity = 1, float duration = 1f , float angleX =0, float offsetY =0)
    {
        if (hapticsSetting == null)
        {
            return;
        }
        hapticType = new CustomEventType(hapticEvent, intensity, duration, angleX, offsetY);
        StartCoroutine(TriggerFeedBack<CustomEventType>(hapticType));

    }
    private IEnumerator TriggerFeedBack<T>(IHapticEventType eventType) where T: IHapticEventType
    {
        if (typeof(T) == typeof(NormalEventType))
        {
            NormalEventType type = eventType as NormalEventType;
            BhapticsLibrary.Play(type.HapticEventName);
        }
        else if (eventType.GetType() == typeof(NormalEventType))
        {
            CustomEventType type = eventType as CustomEventType;
            BhapticsLibrary.PlayParam(type.HapticEventName,type.Intensity,type.Duration,
                type.AngleX,type.OffsetY
                );
        }
        yield return null;

        while (true)
        {
            if (!BhapticsLibrary.IsPlayingByRequestId(CurrentHapticEventId))
            {
                break;
            }
            yield return null;
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    void Start()
    {
        InitializeSettings();
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     Play(HapticEvents.continuous_frontback_beat_fast_level1);
        // }
    }
}
