using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="Blocks/Samurai")]
public class SamuraiInfo : BlockInfo
{
    public override Type BlockType { get; set; } = typeof(Samurai);

    [Range(1,10)] public float countMultiplier;
    [Range (0,1)] public float delayBetweenPacksMultiplier = 0.01f;
    [Range(0, 20)] public float duration;
    [Range(0, 20)] public float waitTimeToNextSpawn = 2;



    public List<BlockInfo> blocks2Spawn = new List<BlockInfo>();
}
