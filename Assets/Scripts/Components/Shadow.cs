using NaughtyAttributes;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    [OnValueChanged("RepaintShadow")] public Vector2 offset = new Vector2(0.2f, -0.2f);
    [OnValueChanged("RepaintShadow")] [SortingLayer] public int sortingLayer = 5;
    [OnValueChanged("RepaintShadow")] public Color color = new Color(0,0,0,180);
    private SpriteRenderer spriteRenderer;
    

    private void RepaintShadow()
    {
        if (spriteRenderer is null)
        {
            spriteRenderer = new GameObject().AddComponent<SpriteRenderer>();
        }
        
        spriteRenderer.transform.SetParent(transform);
        spriteRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
        spriteRenderer.color = color;
        spriteRenderer.sortingLayerID = sortingLayer;
        spriteRenderer.transform.localPosition = offset;
    }
    
    private void Awake()
    {
        RepaintShadow();
    }

    private void Update()
    {
        spriteRenderer.transform.position = transform.position + (Vector3)offset * transform.localScale.x;
        spriteRenderer.transform.rotation = transform.rotation;
    }

    private void OnDestroy()
    {
        Destroy(spriteRenderer.gameObject);
    }
}
