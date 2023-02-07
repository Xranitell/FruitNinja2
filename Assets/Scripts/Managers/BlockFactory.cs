using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public partial class BlockFactory : MonoBehaviour
{
    [Expandable]
    public List<BlockInfo> blockInfos = new List<BlockInfo>();

    public void Awake()
    {
        DataHolder.BlockFactory = this;
        DataHolder.Pull = new List<Block>();
        DataHolder.PullOfBlockParts = new List<BlockPart>();
        
        foreach (var info in blockInfos)
        {
            info.BlockPrefab =CreateBlockPrefab(info);
        }
    }
    private GameObject CreateBlockPrefab(BlockInfo info)
    {
        var block = new GameObject(info.name);
        block.transform.SetParent(transform);
        
        var blockComponent = (Block)block.AddComponent(info.BlockType);
        blockComponent.enabled = true;
        
        //wholeBlock
        blockComponent.wholeBlock = CreateWholeBlock();
        blockComponent.wholeBlock.gameObject.SetActive(false);
        
        //sliced parts
        for (int partIndex = 0; partIndex < info.BlockPartSprites.Length; partIndex++)
        {
            blockComponent.blockParts.Add(CreatePartBlock(partIndex));
        }
        SetGeneralSettingsForParts(new []
        {
            blockComponent.wholeBlock, blockComponent.blockParts[0], blockComponent.blockParts[1]
        }, info, block.transform);
        
        //destroy Particle
        blockComponent.destroyParticle = Instantiate(info.destroyParticle, block.transform);

        blockComponent.blockInfo = info;
        return block;
    }
    private void SetGeneralSettingsForParts(BlockPart[] objects, BlockInfo info,Transform parent)
    {
        var blockComponent = parent.GetComponent<Block>();
        
        foreach (var obj in objects)
        {
            obj.transform.SetParent(parent);
            obj.rectTransform = obj.gameObject.AddComponent<RectTransform>();
            obj.rectTransform.sizeDelta = Vector2.one;
            
            obj.Block = blockComponent;
            obj.physicalObject = obj.gameObject.AddComponent<PhysicalObject>();
            
            AddSpriteRenderer(obj,info);
            AddAnimation(obj,info);
            AddShadow(obj,info);

            if (obj.GetType() == typeof(WholeBlock))
            {
                AddTrail(obj,info);
                obj.physicalObject.mass = info.mass;
            }
            else
            {
                obj.physicalObject.mass = info.mass * 2;
            }
        }
    }

    private BlockPart CreatePartBlock(int index)
    {
        var blockPart = new GameObject("Part" + (index + 1)).AddComponent<BlockPart>();
        blockPart.isWhole = false;
        blockPart.gameObject.SetActive(false);
        return blockPart;
    }
    private WholeBlock CreateWholeBlock()
    {
        var wholeComponent = new GameObject("Whole").AddComponent<WholeBlock>();
        wholeComponent.isWhole = true;
        return wholeComponent;
    }
    
}

partial class BlockFactory
{
    #region Add components
    private void AddSpriteRenderer(BlockPart obj,BlockInfo info)
    {
        var spriteRenderer = obj.gameObject.AddComponent<SpriteRenderer>();
        obj.spriteRenderer = spriteRenderer;
        spriteRenderer.sortingLayerName = "Blocks";
        
        switch (obj.name)
        {
            case "Whole": spriteRenderer.sprite = info.wholeBlockSprite;
                break;
            case "Part1": 
                spriteRenderer.sprite = info.BlockPartSprites[0];
                spriteRenderer.sortingOrder = 0;
                break;
            case "Part2": spriteRenderer.sprite = info.BlockPartSprites[1];
                spriteRenderer.sortingOrder = 1;
                break;
        }
    }
    private void AddAnimation(BlockPart obj,BlockInfo info)
    {
        if (info.useAnimation == true)
        {
            var animation = obj.gameObject.AddComponent<BlockPartAnimation>();
            animation.rotation = info.animRotation;
            animation.scale = info.animScale;
        }
    }
    private void AddShadow(BlockPart obj,BlockInfo info)
    {
        if (info.useShadow == true)
        {
            var shadow = obj.gameObject.AddComponent<Shadow>();
            shadow.color = info.shadowColor;
            shadow.offset = info.shadowOffset;
            shadow.sortingLayer = info.sortingLayer;
        }
    }
    private void AddTrail(BlockPart obj,BlockInfo info)
    {
        if (info.useTrail == true)
        {
            var trail = Instantiate(info.trailRenderer,obj.transform);
            trail.colorGradient = info.trailColorGradient;
            trail.sortingLayerName = "Shadow";
            trail.sortingOrder = -1;
        }
    }
    #endregion
    public static BlockInfo GetBlockByPriority(List<BlockInfo> blocks)
    {
        float maxValue = blocks.Sum(x => x.ChanceToSpawn);;
        float randomValue = Random.Range(0,maxValue);
        float currentValue = 0;

        foreach (var blockData in blocks)
        {
            if (currentValue + blockData.ChanceToSpawn < randomValue)
            {
                currentValue += blockData.ChanceToSpawn;
            }
            else
            {
                return blockData;
            }
        }
        return null;
    }
}
