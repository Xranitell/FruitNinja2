using System;
using UnityEngine;

[CreateAssetMenu (menuName = "Blocks/Life")]
public class LifeInfo : BlockInfo,IChanceChanger
{
    public override Type BlockType { get; set; } = typeof(Life);
    public override bool CanBeSpawned => !DataHolder.HealthManager.LivesIsFull;
    public float BustChangedChance(float chance)
    {
        chance *= DataHolder.BlocksSpawner.BoostChanceCurve.Evaluate(Time.time);
        return chance;
    }
}
