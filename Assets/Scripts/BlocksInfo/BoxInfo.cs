using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu (menuName = "Blocks/Box")]
public class BoxInfo : BlockInfo
{
    public override Type BlockType { get; set; } = typeof(Box);
    [MinMaxSlider(1, 10)] public Vector2 countOfFruit;
    public List<BlockInfo> blocksToSpawn;
    [Range(1,10)]public float spawnRadius;
}
