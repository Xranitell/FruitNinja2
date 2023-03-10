using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Blocks/Magnet")]
public class MagnetInfo : BlockInfo, IChanceChanger
{
    public override Type BlockType { get; set; } = typeof(Magnet);
    public override float Priority => BustChangedChance(priority);

    public float BustChangedChance(float chance)
    {
        chance *= DataHolder.BlocksSpawner.BoostChanceCurve.Evaluate(Time.time);
        return chance;
    }
}
