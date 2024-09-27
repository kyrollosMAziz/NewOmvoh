using DG.Tweening;
using UnityEngine;

public class Pin : MonoBehaviour
{
    [SerializeField] private float targetXPosition;
    [Range(0, 9)] [SerializeField] int m_PinNumber;
    float defaultXPostion;

    public int PinCode => m_PinNumber;

    private void Start()
    {
        defaultXPostion = transform.localPosition.x;
    }

    public void OnPressed()
    {
        transform.DOLocalMoveX(targetXPosition, 1f).OnComplete(ResetPosition);
    }

    private void ResetPosition()
    {
        transform.DOLocalMoveX(defaultXPostion, 1f);
    }
}