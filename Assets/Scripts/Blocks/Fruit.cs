using System;
using UnityEngine;

public class Fruit : Block
{
    public int points;
    
    protected override void Start()
    {
        base.Start();
        
        var info = ((FruitInfo)blockInfo);
        var main = destroyParticle.main;
        main.startColor = info.particleColor; //info.particleColor;
        points = info.points;
    }

    protected override void BlockCut()
    {
        base.BlockCut();
        DataHolder.ScoreManager.RegisterNewCut(this);
    }

    protected override void BlockOutFromScreen(bool isWhole)
    {
        base.BlockOutFromScreen(isWhole);
        
        if (isWhole)
        {
            DataHolder.HealthManager.ChangeHealthValue(-1);
        }
    }
}
