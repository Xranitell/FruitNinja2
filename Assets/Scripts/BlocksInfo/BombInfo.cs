using System;
using UnityEngine;

[CreateAssetMenu (menuName = "Blocks/Bomb")]
public class BombInfo : BlockInfo
{
    public float explosionForce = 10;
    public override Type BlockType { get; set; } = typeof(Bomb);
    public static bool CanBeSpawned { get; set; }

    public override float ChanceToSpawn
    {
        get
        {
            if (CanBeSpawned)
            {
                return chanceToSpawn;
            }
            else
            {
                return 0;
            }
        }
    }
}
