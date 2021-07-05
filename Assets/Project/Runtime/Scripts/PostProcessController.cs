using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessController : MonoBehaviour
{
    [SerializeField] Volume volume;

    static DepthOfField depth;
    static Bloom bloom;
    static ColorCurves colorCurves;

    public static void ChangeBlur(float value)
    {
        depth.focalLength.value = value;
    }

    T GetVolumeComponent<T>() where T : VolumeComponent
    {
        if (volume.profile.TryGet(out T component))
        {
            return component;
        }
        return null;
    }

    private void Awake()
    {
        depth = GetVolumeComponent<DepthOfField>();
        bloom = GetVolumeComponent<Bloom>();
        colorCurves = GetVolumeComponent<ColorCurves>();
    }
}





