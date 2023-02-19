using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;


public abstract class BlockInfo : ScriptableObject
{
    
    [BoxGroup("Spawn properties")][SerializeField][Range(0, 500)] protected float priority;
    [BoxGroup("Spawn properties")][SerializeField] public bool moreThenOne = true;
    [BoxGroup("Spawn properties")][ShowIf("moreThenOne")][SerializeField] [Range(0, 1)] public float maxPercentInPack = 1;
    public float mass = 4;
    
    public virtual float Priority => priority;
    public abstract Type BlockType { get; set; }

    [BoxGroup("Animation")]public bool useAnimation;
    [BoxGroup("Animation")][ShowIf("useAnimation")] [MinMaxSlider(-10f, 10f)] public Vector2 animRotation = new Vector2(-0.5f,0.5f);
    [BoxGroup("Animation")][ShowIf("useAnimation")] [MinMaxSlider(-10f, 10f)] public Vector2 animScale = new Vector2(0.9f,1.2f);
    
    [BoxGroup("Shadow")] public bool useShadow;
    [BoxGroup("Shadow")][ShowIf("useShadow")] public Color shadowColor = new Color(0,0,0,180);
    [BoxGroup("Shadow")][ShowIf("useShadow")] public Vector2 shadowOffset = new Vector2(0.2f,-0.2f);
    [BoxGroup("Shadow")][ShowIf("useShadow")] [SortingLayer] public int sortingLayer = 5;

    [BoxGroup("Trail")] public bool useTrail;
    [BoxGroup("Trail")][ShowIf("useTrail")] public Gradient trailColorGradient;
    [BoxGroup("Trail")][ShowIf("useTrail")] public TrailRenderer trailRenderer;
    
    [BoxGroup("Sprites")]public Sprite wholeBlockSprite;
    [BoxGroup("Sprites")] public bool useSpecialSprites;

    [BoxGroup("Sprites")] [ShowIf("useSpecialSprites")] public Sprite[] specialBlockPartsSprites;
    
    public ParticleSystem destroyParticle;
    public bool useOffsetAndRotation;
    public virtual bool CanBeSpawned { get; set; } = true;
    
    private Sprite[] _blockPartSprites = null;
    
    
    public GameObject BlockPrefab { get; set; }
    public Sprite[] BlockPartSprites
    {
        get
        {
            _blockPartSprites = null;
            if (_blockPartSprites == null || _blockPartSprites.Length != 2)
            {
                _blockPartSprites = useSpecialSprites ? specialBlockPartsSprites : CutBlockSprite();
            }

            return _blockPartSprites;
        }
    }
    Sprite[] CutBlockSprite()
    {
        var sprites = new Sprite[2];
        var texture = wholeBlockSprite.texture;

        for (int i = 0; i < 2; i++)
        {
            int coef = i == 1 ? 1 : 0;
            
            var rectPivot = new Vector2(texture.width/2, 0) * coef;
            var rect = new Rect(rectPivot.x,rectPivot.y, texture.width / 2, texture.height);
            sprites[i] = Sprite.Create(texture, rect, Vector2.one * 0.5f);
        }
        return sprites;
    }
    
}

public interface IChanceChanger
{
    public float BustChangedChance(float chance);
}


