using System;
using UnityEngine;

[CreateAssetMenu (menuName ="Blocks/Fruit")]
public class FruitInfo : BlockInfo
{
    public int points;
    public ParticleSystem.MinMaxGradient particleColor;
    
    public override Type BlockType { get; set; } = typeof(Fruit);
}
