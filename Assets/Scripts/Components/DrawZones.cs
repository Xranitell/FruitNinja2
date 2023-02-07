using UnityEngine;

[ExecuteAlways]
public class DrawZones : MonoBehaviour
{
    private SpawnZones _spawnZones;
    private void Awake()
    {
        _spawnZones = GetComponent<SpawnZones>();
    }

    private void OnDrawGizmos()
    {
        if (_spawnZones != null)
        {
            foreach (var zone in _spawnZones.Zones)
            {
                Debug.DrawLine(zone.StartPoint,zone.EndPoint);
            }
        }
    }
}
