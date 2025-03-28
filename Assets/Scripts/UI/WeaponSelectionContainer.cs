using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Accessibility;

public class WeaponSelectionContainer : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;

    [Header("Color")]
    [SerializeField] private Image[] levelDependentImages;


    [field: SerializeField] public Button Button { get; private set; }

    public void Configure(Sprite sprite, string name, int level)
    {
        icon.sprite = sprite;
        nameText.text = name;
        Color imageColor = ColorHolder.GetColor(level);
        foreach (Image image in levelDependentImages)
        {
            image.color = imageColor; 
        }

    }
    public void Select()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector3.one * 1.075f, .3f).setEase(LeanTweenType.easeInOutSine);
    }
    public void DeSelect()
    {
        LeanTween.cancel(gameObject);

        LeanTween.scale(gameObject, Vector3.one, .3f);

    }
}
