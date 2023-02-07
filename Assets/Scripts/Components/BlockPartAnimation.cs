using System;
using DG.Tweening;
using DG.Tweening.Plugins.Options;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlockPartAnimation : MonoBehaviour
{
    [Header("Rotation Animation")]
    [MinMaxSlider(-1f, 1f)] public Vector2 rotation;
    [Header("Scale Animation")] public Vector2 scale;

    private float speedRotation;
    private void OnEnable()
    {
        speedRotation = Random.Range(rotation.x,rotation.y);
        var maxScale = Random.Range(scale.x,scale.y);
        
        DOTween.Sequence()
            .Append(transform.DOScale(Vector3.one * maxScale, 1f))
            .Append(transform.DOScale(Vector3.one, 1f))
            .SetEase(Ease.InCubic);
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, speedRotation);
    }
}
