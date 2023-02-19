using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;


public partial class BlocksSpawner : MonoBehaviour
{
    [SerializeField] private AnimationCurve delayBetweenPacks;
    [SerializeField] private AnimationCurve delayBetweenSpawnBlocks;
    [SerializeField] private AnimationCurve spawnCount;
    [SerializeField] private AnimationCurve boostSpawnChance;
    
    [MinMaxSlider(-10,10)] public Vector2 force;
    [SerializeField] private Transform blocksStorage;

    public float CountMultiplier = 1;
    public float DelayBetweenPacksMultiplier = 1;

    public List<BlockInfo> blocksCollection;

    public AnimationCurve BoostChanceCurve => boostSpawnChance;
    
    private void Start()
    {
        DataHolder.BlocksSpawner = this;
        blocksCollection = DataHolder.BlockFactory.BlockInfos;
        StartCoroutine(SpawnPacks());
    }

    private IEnumerator SpawnPacks()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayBetweenPacks.Evaluate(Time.timeSinceLevelLoad) * DelayBetweenPacksMultiplier);

            StartCoroutine(SpawnPack());
        }
    }
    
    IEnumerator SpawnPack()
    {
        var cam = DataHolder.MainCamera;
        var blockCountInPack = Mathf.RoundToInt(Random.Range(1,spawnCount.Evaluate(Time.timeSinceLevelLoad) * CountMultiplier));
        var blockList = GenerateBlockList(blockCountInPack);

        foreach (var selectedBlock in blockList.ToList())
        {
            var spawnPosition = DataHolder.SpawnZones.GetPointInRandomZone();
            var targetPosition = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth/2, cam.pixelHeight)); 
            
            var block = SpawnBlock(selectedBlock, spawnPosition);
            var throwVector = GetForceVector(block.wholeBlock.rectTransform.position, targetPosition);
            
            ThrowBlock(block, throwVector, throwVector.magnitude + Random.Range(force.x,force.y));
            
            yield return new WaitForSeconds(delayBetweenSpawnBlocks.Evaluate(Time.timeSinceLevelLoad));
            StopCoroutine(nameof(SpawnPack));
        }
    }

    public static Block SpawnBlock(BlockInfo blockInfo, Vector3 startPosition)
    {
        Block block = null;
        var blockInPull = DataHolder.Pull.Find(x => x.name == blockInfo.name);
        var blockStorage = DataHolder.BlocksSpawner.blocksStorage;

        if (blockInPull is null)
        {
            block = Instantiate(blockInfo.BlockPrefab, blockStorage).GetComponent<Block>();
            block.name = blockInfo.name;
        }
        else
        {
            DataHolder.Pull.Remove(blockInPull);
            block = blockInPull;
        }

        block.wholeBlock.transform.position = startPosition;
        
        return block;
    }
    public static void ThrowBlock(Block block, Vector3 throwVector, float force)
    {
        var wholeBlockObject = block.wholeBlock;
        wholeBlockObject.physicalObject.AddForce(throwVector, force);
        wholeBlockObject.gameObject.SetActive(true);
    }
    public static Vector2 GetForceVector(Vector3 startPosition, Vector3 targetPosition)
    {
        return targetPosition- startPosition;
    }
}

partial class BlocksSpawner
{
    private List<BlockInfo> blocksToSpawn = new List<BlockInfo>();
    List<BlockInfo> GenerateBlockList(int blockCountInPack)
    {
        blocksToSpawn.Clear();
        while (blocksToSpawn.Count < blockCountInPack)
        {
            //here we are check selected block on contains in generated pack
            var selectedBlock = BlockFactory.GetBlockByPriority(blocksCollection);
            var blocksCount = 0;
            foreach (var x in blocksToSpawn)
            {
                if (x == selectedBlock) blocksCount++;
            }
            
            //if block can be only one
            var oneBlockLimitationCorrect = !selectedBlock.moreThenOne && blocksCount == 0;
            
            //if block limited in percents of blocks count in pack
            double percentSelectedBlockTypesInPack = (blocksCount+1) / (double)blockCountInPack;
            var percentLimitationCorrect = (selectedBlock.maxPercentInPack >= percentSelectedBlockTypesInPack) && selectedBlock.moreThenOne;
            
            // CanBeSpawned - unique property for blocks which can be edited
            if ((oneBlockLimitationCorrect || percentLimitationCorrect) && selectedBlock.CanBeSpawned)
            {
                blocksToSpawn.Add(selectedBlock);
            }

        }
        return blocksToSpawn;
    }
}



