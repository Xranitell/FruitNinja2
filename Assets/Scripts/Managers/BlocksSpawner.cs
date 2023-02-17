using System;
using System.Collections;
using System.Collections.Generic;
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

    [Range(0, 1)] [SerializeField] private float maxPercentOfBomb = 0.5f;

    public float countMultiplier = 1;
    public float delayBetweenPacksMultiplier = 1;

    public List<BlockInfo> blocksToSpawn;

    public AnimationCurve BoostChanceCurve => boostSpawnChance;
    
    private void Start()
    {
        DataHolder.BlocksSpawner = this;
        blocksToSpawn = DataHolder.BlockFactory.BlockInfos;
        StartCoroutine(SpawnPacks());
    }
    
    private IEnumerator SpawnPacks()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayBetweenPacks.Evaluate(Time.timeSinceLevelLoad) * delayBetweenPacksMultiplier);
            StartCoroutine( SpawnPack());
        }
    }
    private IEnumerator SpawnPack()
    {
        var cam = DataHolder.MainCamera;
        var bombsCount = 0;
        
        BombInfo.CanBeSpawned = true;
        LifeInfo.CanBeSpawned = !DataHolder.HealthManager.LivesIsFull();

        var blockCountInPack = Mathf.RoundToInt(Random.Range(1,spawnCount.Evaluate(Time.timeSinceLevelLoad) * countMultiplier));
        
        for (int i = 0; i < blockCountInPack; i++)
        {
            var selectedBlock = BlockFactory.GetBlockByPriority(blocksToSpawn);
            var blockType = selectedBlock.GetType();

            CheckBombSpawn(blockType,ref bombsCount,blockCountInPack);
            
            var spawnPosition = DataHolder.SpawnZones.GetPointInRandomZone();
            var targetPosition = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth/2, cam.pixelHeight)); 
            
            var block = SpawnBlock(selectedBlock, spawnPosition);
            var throwVector = GetForceVector(block.wholeBlock.rectTransform.position, targetPosition);
            
            ThrowBlock(block, throwVector, throwVector.magnitude + Random.Range(force.x,force.y));
            
            yield return new WaitForSeconds(delayBetweenSpawnBlocks.Evaluate(Time.timeSinceLevelLoad));
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
    void CheckBombSpawn(Type blockType, ref int bombsCount, int blockCountInPack)
    {
        if (blockType == typeof(Bomb))
        {
            bombsCount++;
            if ((bombsCount) / blockCountInPack > maxPercentOfBomb)
            {
                BombInfo.CanBeSpawned = false;
            }
        }
    }
}



