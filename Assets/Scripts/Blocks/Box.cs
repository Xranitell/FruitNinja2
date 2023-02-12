using UnityEngine;

public class Box:Block
{
    protected override void BlockCut()
    {
        var boxInfo = (BoxInfo)blockInfo;
        base.BlockCut();
        SpawnBlocks((int)Random.Range(boxInfo.countOfFruit.x, boxInfo.countOfFruit.y));
    }

    void SpawnBlocks(int blocksCount)
    {
        var fruitsInfos = ((BoxInfo)blockInfo).blocksToSpawn;

        for (int i = 0; i < blocksCount; i++)
        {
            var fruit = BlockFactory.GetBlockByPriority(fruitsInfos);
            var fruitSpawnPoint = new Vector2(Random.Range(-2, 2), Random.Range(0, 2));
            var position = wholeBlock.rectTransform.position;
            var block = BlocksSpawner.SpawnBlock(fruit, position + new Vector3(fruitSpawnPoint.x,fruitSpawnPoint.y,0));
            
            var throwVector =
                BlocksSpawner.GetForceVector(position,position + new Vector3(fruitSpawnPoint.x, fruitSpawnPoint.y, 0).normalized);
            
            BlocksSpawner.ThrowBlock(block, throwVector, 3);
        }
        
    }
}