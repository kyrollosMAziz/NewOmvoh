using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PinsController : SceneContextSingleton<PinsController>
{
    [SerializeField] private TextMeshProUGUI _pinText;
    [SerializeField] private List<Pin> _pins = new();
    [SerializeField] private AudioSource _clickSfx;
    [SerializeField] private PlayGlow _playGlow;
    private int counter;

    private int[] myNum = { 4, 4, 2, 0 };  
    private int arrayIndex = 0;        
    private int fullEntryCount = 0;     

    public void PinCodeClick()
    {
        _playGlow.ResetMaterial(); 

        var pin = PickRandomPinCode();
        UpdatePinCodeText(pin.PinCode);
        pin.OnPressed();
    }

    private void UpdatePinCodeText(int pinCode)
    {
        _clickSfx.Play();

        if (_pinText.text.Contains("---") || _pinText.text.Contains("ERR"))
        {
            _pinText.text = myNum[arrayIndex].ToString();
        }
        else
        {
            _pinText.text += myNum[arrayIndex].ToString();
        }

        if (_pinText.text.Contains("442"))
        {
            _pinText.text = "---";
            arrayIndex = 0;
            fullEntryCount = 0;
            return;
        }

        arrayIndex++;

        if (arrayIndex >= myNum.Length)
        {
            arrayIndex = 0;
            fullEntryCount++;

            _pinText.text = "<color=#FF0000>Err</color>";

            if (fullEntryCount >= 2)
            {
                fullEntryCount = 0;
                SupermarketGameManager.Instance.BathroomInteractionFired();
                gameObject.SetActive(false);
            }
        }
    }

    private Pin PickRandomPinCode()
    {
        var index = Random.Range(0, _pins.Count);
        return _pins[index];
    }
}




//       else
//{
//    isSetErr = true;
//    StartCoroutine(setErr());
//    IEnumerator setErr()
//    {
//        yield return new WaitForSeconds(0.01f);

//        if (arrayIndex >= myNum.Length)
//        {
//            arrayIndex = 0;
//            fullEntryCount++;

//            _pinText.text = "<color=#FF0000>Err</color>";

//            if (fullEntryCount >= 2)
//            {
//                fullEntryCount = 0;

//                SupermarketGameManager.Instance.BathroomInteractionFired();
//                gameObject.SetActive(false);
//            }
//            isSetErr = false;
//        }
//    }
//}