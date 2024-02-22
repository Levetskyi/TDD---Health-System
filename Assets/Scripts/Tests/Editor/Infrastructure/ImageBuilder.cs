using UnityEngine.UI;
using UnityEngine;

public class ImageBuilder : TestDataBuilder<Image>
{
	private float _fillAmount;

    public ImageBuilder(float fillAmount)
    {
        _fillAmount = fillAmount;
    }

    public ImageBuilder() : this(0f)
    {
    }

    public ImageBuilder WithFillAmount(float fillAmount)
    {
        _fillAmount = fillAmount;
        return this;
    }

    public override Image Build()
    {
        var image = new GameObject().AddComponent<Image>();
        image.fillAmount = _fillAmount;
        return image;
    }
}