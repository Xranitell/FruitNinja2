using DG.Tweening;
using UnityEngine;

public class AdditiveLifeAnimation : MonoBehaviour
{
    private static Transform _transform;
    private void Start()
    {
        _transform = transform;
    }

    public static void StartAnimation(Vector3 startPosition)
    {
        _transform.localScale = Vector3.one;
        
        Vector3 targetPos= DataHolder.HealthDisplay.LastChangedHeart.transform.position;

        DOTween.Sequence()
            .AppendCallback(() => _transform.position = startPosition)
            .Append(_transform.DOMove(targetPos, 1f))
            .AppendCallback(() => DataHolder.HealthManager.ChangeHealthValue(1))
            .Append(_transform.DOScale(Vector3.one * 0.5f,1f))
            .AppendCallback(()=> _transform.localScale = Vector3.zero);
    }
}
