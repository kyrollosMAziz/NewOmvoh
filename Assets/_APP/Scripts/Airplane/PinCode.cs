using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PinCode : ProjectContextSingleton<PinCode>
{
    public TextMeshProUGUI m_Screen;
    public List<IFailedPinCode> m_Pins = new List<IFailedPinCode>();

    public void AssignPins(IFailedPinCode Pin) 
    {
        m_Pins.Add(Pin);
        Debug.Log(Pin);
    }
    public void RemovePins() 
    {
        m_Pins = null;
    }
    public void NotifyPins() 
    {
        m_Pins.ForEach(p => p.OnPinCodeFail());
        RemovePins();
    }
}
