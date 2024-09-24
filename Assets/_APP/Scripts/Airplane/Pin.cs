using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Pin : MonoBehaviour
{
    [SerializeField] float m_MovingSpeed;
    [SerializeField] float m_CurrentZPostion;
    [SerializeField] TextMeshProUGUI screenText;
    [Range(0, 9)][SerializeField] int m_PinNumber;
    
    private void Start()
    {
        screenText = PinCode.Instance?.m_Screen;
        m_CurrentZPostion = transform.localPosition.z;
    }
    // Test
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)) 
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
        if (screenText && screenText.text == "-") 
        {
            
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
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y,m_CurrentZPostion);
    }
}
