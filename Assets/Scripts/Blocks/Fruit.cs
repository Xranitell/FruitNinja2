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
        PointsUIManager.ThisInstance.AddText(points,wholeBlock.transform.position, ((FruitInfo)blockInfo).particleColor.color);
    }

    protected override void BlockOutFromScreen(bool isWhole)
    {
        base.BlockOutFromScreen(isWhole);
        
        if (isWhole && wholeBlock.canRemoveHealth)
        {
            DataHolder.HealthManager.ChangeHealthValue(-1);
        }
    }
}
