using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ReturnToOriginalPosition : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Transform _parent;
    [SerializeField] private Transform _defaultTransform;
    private XRGrabInteractable grabInteractable;
    private bool flag = true;
    void Start()
    {
        _parent = transform.parent;
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnSelectEntered);
        grabInteractable.selectExited.AddListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs arg0)
    {
        flag = false;
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        flag = true;
    }

    private void Update()
    {
        if (flag)
        {
            transform.position = _defaultTransform.position;
            transform.rotation = _defaultTransform.rotation;
        }
    }
}
