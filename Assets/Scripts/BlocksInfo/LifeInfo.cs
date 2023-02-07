using System;
using UnityEngine;

[CreateAssetMenu (menuName = "Blocks/Life")]
public class LifeInfo : BlockInfo,IChanceChanger
{
    public override Type BlockType { get; set; } = typeof(Life);
    public static bool CanBeSpawned { get; set; }
    public override float ChanceToSpawn => CanBeSpawned ? BustChangedChance(chanceToSpawn) : 0;

    public float BustChangedChance(float chance)
    {
        chance *= DataHolder.BlocksSpawner.BustChanceCurve.Evaluate(Time.time);
        return chance;
    }
}
