using UnityEngine;

[ExecuteAlways]
public class DrawZones : MonoBehaviour
{
    [SerializeField] SpawnZones spawnZones;
    private void Awake()
    {
        spawnZones = GetComponent<SpawnZones>();
    }

    private void OnDrawGizmos()
    {
        if (spawnZones != null)
        {
            foreach (var zone in spawnZones.Zones)
            {
                Debug.DrawLine(zone.StartPoint,zone.EndPoint);
            }
        }
    }
}
