using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PinsController : SceneContextSingleton<PinsController>
{
    [SerializeField] private TextMeshProUGUI _pinText;
    [SerializeField] private List<Pin> _pins = new();
    [SerializeField] private int counter;
    private bool fireonce;
    public void PinCodeClick()
    {
        if (counter >= 2)
        {
            if (fireonce) return;
           
            SupermarketGameManager.Instance.BathroomInteractionFired();
            fireonce = !fireonce;
            return;
        }

        var pin = PickRandomPinCode();
        UpdatePinCodeText(pin.PinCode);
        pin.OnPressed();
    }

    private void UpdatePinCodeText(int pinCode)
    {
        if (_pinText && _pinText.text.Contains("-"))
            _pinText.text = pinCode.ToString();
        else
            _pinText.text += pinCode.ToString();


        if (_pinText.text.Count() > 2)
        {
            _pinText.text = "---";
            counter += 1;
        }
    }

    private Pin PickRandomPinCode()
    {
        var index = Random.Range(0, _pins.Count);
        return _pins[index];
    }
}