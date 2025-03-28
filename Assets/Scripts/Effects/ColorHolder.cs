using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHolder : MonoBehaviour
{
    public static ColorHolder intance;
    [Header("Elements")]
    [SerializeField] private PaletteSO palette;

    private void Awake()
    {
        if(intance == null)
        {
            intance = this;
        }
        else
            Destroy(gameObject);
    }
    public static Color GetColor(int level)
    {
        level = Mathf.Clamp(level, 0, intance.palette.LevelColors.Length);
        return intance.palette.LevelColors[level];
    }
    public static Color GetOutlineColor(int level)
    {
        level = Mathf.Clamp(level, 0, intance.palette.LevelOutlineColors.Length);
        return intance.palette.LevelOutlineColors[level];
    }


}
