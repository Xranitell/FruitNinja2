using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Samurai : Block, IChanceChanger
{
    SamuraiInfo samuraiInfo;
    public static bool isSamuraiEvent;
    private static Coroutine samuraiCorutine;

    protected override void BlockCut()
    {
        samuraiInfo = (SamuraiInfo)blockInfo;
        base.BlockCut();

        samuraiCorutine = StartCoroutine(SamuraiCorutine());
    }
   
    IEnumerator SamuraiCorutine()
    {
        var spawner = DataHolder.BlocksSpawner;

        spawner.countMultiplier = samuraiInfo.countMultiplier;
        spawner.delayBetweenPacksMultiplier = samuraiInfo.delayBetweenPacksMultiplier;
        spawner.blocksToSpawn = samuraiInfo.blocks2Spawn;
        isSamuraiEvent = true;

        yield return new WaitForSeconds(samuraiInfo.duration);

        spawner.countMultiplier = 0;
        spawner.delayBetweenPacksMultiplier = samuraiInfo.waitTimeToNextSpawn;

        yield return new WaitForSeconds(samuraiInfo.waitTimeToNextSpawn);

        spawner.countMultiplier = 1;
        spawner.delayBetweenPacksMultiplier = 1;
        spawner.blocksToSpawn = DataHolder.BlockFactory.BlockInfos;
        isSamuraiEvent = false;

        StopCoroutine(samuraiCorutine);
    }

    public float BustChangedChance(float chance)
    {
        chance *= DataHolder.BlocksSpawner.BoostChanceCurve.Evaluate(Time.time);
        return chance;
    }
}
