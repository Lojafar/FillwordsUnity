using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ExtendedSlider : Slider
{
    public event Action<float> OnEndChanging;
    public override void OnPointerUp(PointerEventData eventData)
    {
        OnEndChanging?.Invoke(value);
    }
}
