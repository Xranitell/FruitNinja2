using System;
using UnityEngine;

[CreateAssetMenu (menuName = "Blocks/Ice")]
public class IceInfo: BlockInfo, IChanceChanger
{
    public override Type BlockType { get; set; } = typeof(Ice);
    
    public AnimationCurve stopTimeAnimation;
    public float updateRate = 0.02f;
    public float timeDuration = 5f;

    public override float Priority => BustChangedChance(priority);

    public float BustChangedChance(float chance)
    {
        chance *= DataHolder.BlocksSpawner.BoostChanceCurve.Evaluate(Time.time);
        return chance;
    }
}