using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using NaughtyAttributes;
using Random = UnityEngine.Random;

[ExecuteAlways]
public class SpawnZones : MonoBehaviour
{
    [OnValueChanged("UpdateZones")] public SpawnZone downZone = new SpawnZone("Down", Side.Down);
    [OnValueChanged("UpdateZones")] public SpawnZone rightZone = new SpawnZone("Right", Side.Right);
    [OnValueChanged("UpdateZones")] public SpawnZone leftZone = new SpawnZone("Left", Side.Left);

    public List<SpawnZone> Zones { get; private set; } = new List<SpawnZone>();

    private void Awake()
    {
        Zones =new List<SpawnZone>(){downZone, rightZone, leftZone };
    }

    private void Start()
    {
        DataHolder.SpawnZones = this;
        DataHolder.MainCamera = Camera.main;

        DataHolder.DeathZone = downZone;
        UpdateZones();
    }

    private void UpdateZones()
    {
        Zones =new List<SpawnZone>(){downZone, rightZone, leftZone };
        foreach (var zone in Zones)
        {
            zone.UpdateValues();
        }
    }
    
    public Vector2 GetPointInRandomZone()
    {
        var currentValue = 0f;
        var maxValue = Zones.Sum(x => x.chance);
        var randomValue = Random.Range(0, maxValue);

        foreach (var zone in Zones)
        {
            if (currentValue + zone.chance < randomValue)
            {
                currentValue += zone.chance;
            }
            else
            {
                return zone.GetRandomPoint();
            }
        }
        return new Vector2();
    }
}

[Serializable]
public class SpawnZone
{
    public readonly string Name;
    [Tooltip("Chance to  ")]
    [Range(0,100)] public float chance;

    [Header("Spawn zone properties")]
    [Range(0,1)] public float size;
    [Range(0,100)]public float offset;
    public Side SideType { get; set; }
    public Vector2 StartPoint { get; set; }
    public Vector2 EndPoint { get; set; }

    public SpawnZone(string name, Side sideType)
    {
        this.Name = name;
        this.SideType = sideType;
    }
    
    public void UpdateValues()
    {
        Vector2 min = DataHolder.MainCamera.ViewportToWorldPoint (new Vector2 (0,0));// bottom-left corner
        Vector2 max = DataHolder.MainCamera.ViewportToWorldPoint (new Vector2 (1,1)); // top-right corner
        
        if (SideType == Side.Down)
        {
            StartPoint = new Vector2(min.x * size, min.y - offset);
            EndPoint = new Vector2(max.x * size,min.y - offset);
        }
        else if(SideType == Side.Left)
        {
            StartPoint = new Vector2(min.x - offset, min.y);
            EndPoint = new Vector2(min.x - offset, max.y * size);
        }
        else
        {
            StartPoint = new Vector2(max.x + offset, min.y);
            EndPoint = new Vector2(max.x + offset,max.y * size);
        }
    }
    public Vector2 GetRandomPoint()
    {
        float randomX = Random.Range(StartPoint.x, EndPoint.x);
        float randomY = Random.Range(StartPoint.y,EndPoint.y);
        return new Vector2(randomX, randomY);
    }
}
public enum Side
{
    Right,
    Left,
    Down
}
