using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;

public class LightMapPointDisabler : MonoBehaviour
{
    List<Light> lights;

    void Awake()
    {
        lights = new List<Light>();
    }

    private void Start()
    {
        foreach (GameObject lightcomponent in GameObject.FindGameObjectsWithTag("MapPoints"))
            if (lightcomponent != null && lightcomponent.GetComponent<Light>() != null)
                lights.Add(lightcomponent.GetComponent<Light>());
    }

    void Update()
    {
        
    }

    private void OnPreRender()
    {
        foreach (Light light in lights)
            light.enabled = false;
    }
    private void OnPreCull()
    {
        foreach (Light light in lights)
            light.enabled = true;
    }
}
