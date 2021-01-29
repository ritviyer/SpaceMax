using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldUI : MonoBehaviour
{
    [SerializeField] RectTransform barRect;

    float maxWidth;
    float maxHeight;

    private void Awake()
    {
        maxWidth = barRect.rect.width;
        maxHeight = barRect.rect.height;
    }
    private void OnEnable()
    {
        EventManager.onHealthDamage += UpdateShieldDisplay;
    }

    private void OnDisable()
    {
        EventManager.onHealthDamage -= UpdateShieldDisplay;
    }

    void UpdateShieldDisplay(float percent)
    {
        barRect.sizeDelta = new Vector2(maxWidth * percent, maxHeight);
    }
}
