using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyLight : MonoBehaviour
{
    public void ChangeLights()
    {
        RenderSettings.fog = !RenderSettings.fog;
    }
}
