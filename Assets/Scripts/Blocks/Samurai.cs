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

        StartCoroutine(StartTimer());
        
        spawner.CountMultiplier = samuraiInfo.countMultiplier;
        spawner.DelayBetweenPacksMultiplier = samuraiInfo.delayBetweenPacksMultiplier;
        spawner.blocksCollection = samuraiInfo.blocks2Spawn;
        isSamuraiEvent = true;

        yield return new WaitForSeconds(samuraiInfo.duration-1);

        spawner.CountMultiplier = 0;
        spawner.DelayBetweenPacksMultiplier = samuraiInfo.waitTimeToNextSpawn;

        yield return new WaitForSeconds(samuraiInfo.waitTimeToNextSpawn);

        spawner.CountMultiplier = 1;
        spawner.DelayBetweenPacksMultiplier = 1;
        spawner.blocksCollection = DataHolder.BlockFactory.BlockInfos;
        isSamuraiEvent = false;

        StopCoroutine(samuraiCorutine);
    }

    IEnumerator StartTimer()
    {
        int timer = Mathf.RoundToInt(samuraiInfo.duration);
        
        while (timer > 0)
        {
            SamuraiTimer.instance.UpdateValue("Осталось: " + timer);
            timer--;
            yield return new WaitForSeconds(1f);
        }

        SamuraiTimer.instance.UpdateValue("");
    }
    
    public float BustChangedChance(float chance)
    {
        chance *= DataHolder.BlocksSpawner.BoostChanceCurve.Evaluate(Time.time);
        return chance;
    }
}
