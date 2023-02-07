public class Life : Block
{
    protected override void BlockCut()
    {
        base.BlockCut();
        AdditiveLifeAnimation.StartAnimation(DataHolder.MainCamera.WorldToScreenPoint(wholeBlock.rectTransform.position));
    }

    protected override void BlockOutFromScreen(bool isWhole)
    {
        base.BlockOutFromScreen(isWhole);
    }
}
