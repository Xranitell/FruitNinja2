using UnityEngine;

public class Box:Block
{
    private BoxInfo boxInfo;
    protected override void BlockCut()
    {
        boxInfo = (BoxInfo)blockInfo;
        base.BlockCut();
        SpawnBlocks((int)Random.Range(boxInfo.countOfFruit.x, boxInfo.countOfFruit.y));
    }

    void SpawnBlocks(int blocksCount)
    {
        boxInfo = (BoxInfo)blockInfo;
        var fruitsInfos = ((BoxInfo)blockInfo).blocksToSpawn;

        for (int i = 0; i < blocksCount; i++)
        {
            var fruit = BlockFactory.GetBlockByPriority(fruitsInfos);

            var fruitSpawnPoint = new Vector2();
            fruitSpawnPoint.x = Random.Range(-2, 2);
            fruitSpawnPoint.y = Mathf.Sqrt(Mathf.Pow(boxInfo.spawnRadius,2) - Mathf.Pow(fruitSpawnPoint.x,2));
            
            
            var position = wholeBlock.rectTransform.position;
            var block = BlocksSpawner.SpawnBlock(fruit, position + new Vector3(fruitSpawnPoint.x,fruitSpawnPoint.y,0));
            
            var throwVector =
                BlocksSpawner.GetForceVector(position,position + new Vector3(fruitSpawnPoint.x, fruitSpawnPoint.y, 0).normalized);
            
            BlocksSpawner.ThrowBlock(block, throwVector, 3);
        }
        
    }
}