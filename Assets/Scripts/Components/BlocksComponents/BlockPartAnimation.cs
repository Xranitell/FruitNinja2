using System;
using DG.Tweening;
using DG.Tweening.Plugins.Options;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BlockPart))]
public class BlockPartAnimation : MonoBehaviour
{
    [Header("Rotation Animation")]
    [MinMaxSlider(-1f, 1f)] public Vector2 rotation;
    [Header("Scale Animation")] public Vector2 scale;
    
    private Sequence _animationSequence;
    private float _speedRotation;

    private void OnEnable()
    {
        _animationSequence.Kill();
        _speedRotation = Random.Range(rotation.x,rotation.y);
        var maxScale = Random.Range(scale.x,scale.y);
        
        _animationSequence = DOTween.Sequence()
            .Append(transform.DOScale(Vector3.one * maxScale, 1f))
            .Append(transform.DOScale(Vector3.one, 1f))
            .SetEase(Ease.InCubic).SetUpdate(false);
    }

    private void OnDisable()
    {
        _animationSequence.Kill();
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, _speedRotation * Time.timeScale);
    }
}
