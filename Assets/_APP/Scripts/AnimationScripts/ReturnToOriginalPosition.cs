using DG.Tweening;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ReturnToOriginalPosition : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Transform _parent;
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        // Store the original position and rotation
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        _parent=transform.parent;
        // Get the XRGrabInteractable component
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Register event handlers
        grabInteractable.selectExited.AddListener(OnSelectExited);
    }

    // This method will be called when the object is ungrabbed
    private void OnSelectExited(SelectExitEventArgs args)
    {
        //Invoke(nameof(ReturnToPosition),.2f);
        ReturnToPosition();
    }
    public void ReturnToPosition()
    {
        transform.parent = _parent;
        // Reset the position and rotation of the object
        //transform.position = originalPosition;
        //transform.rotation = originalRotation;

        transform.DOMove(originalPosition,2);
        transform.DORotateQuaternion(originalRotation,2);
    }
}
