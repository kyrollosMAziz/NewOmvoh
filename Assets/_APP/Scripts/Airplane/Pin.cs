using System;
using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Pin : MonoBehaviour, IFailedPinCode
{
    [SerializeField] float m_MovingSpeed;
    [SerializeField] float m_CurrentZPostion;
    [SerializeField] TextMeshProUGUI screenText;
    [Range(0, 9)][SerializeField] int m_PinNumber;
    [SerializeField] int counter;
    Collider myCollider;
    private void Awake()
    {
        myCollider = gameObject.GetComponent<Collider>();
        PinCode.Instance.AssignPins(this);
    }
    private void Start()
    {
        screenText = PinCode.Instance?.m_Screen;
        m_CurrentZPostion = transform.localPosition.z;
    }
    // Test
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && myCollider.enabled == true)
        {
            OnPressed();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        OnPressed();
    }
    void OnPressed()
    {
        ChangePinCode();
        StartCoroutine(ButtonPressEffect());
    }
    void ChangePinCode()
    {
        ValidatePinCode();
    }

    private void ValidatePinCode()
    {
        if (screenText && screenText.text.Contains("-"))
        {
            screenText.text = m_PinNumber.ToString();
        }
        else
        {
            screenText.text += m_PinNumber.ToString();
        }
        if (screenText.text.Count() > 3)
        {
            screenText.text = "----";
            counter += 1;
        }
        if (counter > 2)
        {
            PinCode.Instance.NotifyPins();
            #region PlayReplayLogic
            //TODO: Add rest of the logic here
            #endregion
        }
    }

    IEnumerator ButtonPressEffect()
    {
        while (transform.localPosition.z <= 0)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + m_MovingSpeed);
            yield return new WaitForEndOfFrame();
        }
        while (transform.localPosition.z >= m_CurrentZPostion)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - m_MovingSpeed);
            yield return new WaitForEndOfFrame();
        }
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, m_CurrentZPostion);
    }

    public void OnPinCodeFail()
    {
        Collider myCollider = gameObject.GetComponent<Collider>();
        if (myCollider != null) 
        {
            myCollider.enabled = false;
        }
    }
}
