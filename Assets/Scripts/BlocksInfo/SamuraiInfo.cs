using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu (menuName ="Blocks/Samurai")]
public class SamuraiInfo : BlockInfo
{
    public override Type BlockType { get; set; } = typeof(Samurai);

    [BoxGroup("Event properties")][Range(1,10)] public float countMultiplier;
    [BoxGroup("Event properties")][Range (0,1)] public float delayBetweenPacksMultiplier = 0.01f;
    [BoxGroup("Event properties")][Range(0, 20)] public float duration = 5;
    [BoxGroup("Event properties")][Range(0, 20)] public float waitTimeToNextSpawn = 2;



    public List<BlockInfo> blocks2Spawn = new List<BlockInfo>();
}
