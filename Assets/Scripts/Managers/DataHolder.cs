using System.Collections.Generic;
using UnityEngine;

public static class DataHolder
{
    public static HealthManager HealthManager { get; set; }
    public static ScoreManager ScoreManager { get; set; }
    public static Cutter Cutter { get; set; }
    public static Camera MainCamera { get; set; }
    public static SpawnZones SpawnZones { get; set; }
    public static BlockFactory BlockFactory { get; set; }
    public static SpawnZone DeathZone { get; set; }
    public static BlocksSpawner BlocksSpawner { get; set; }
    public static ScoreLabels ScoreLabels { get; set; }

    public static List<Block> Pull = new List<Block>();
    public static List<BlockPart> PullOfBlockParts = new List<BlockPart>();
}
 