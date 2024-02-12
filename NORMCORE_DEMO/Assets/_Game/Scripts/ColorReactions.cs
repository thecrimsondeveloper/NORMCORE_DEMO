using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ColorReactions : MonoBehaviour
{
    [SerializeField] float triggersPerMinute = 120;
    [SerializeField] VisualEffect[] vfx;

    void Awake()
    {
        BounceBall.OnColorChange.AddListener(OnColorChange);
    }


    float lastTimeTriggered = 0;
    private void Update()
    {
        //if the difference between the last time the color was changed and the current time is greater than 0.1
        if (Time.time - lastTimeTriggered > 60 / triggersPerMinute)
        {
            //trigger the color effects
            TriggerColorEffects();
            //set the last time triggered to the current time
            lastTimeTriggered = Time.time;
        }
    }

    void OnColorChange(PlayerColor newColor)
    {
        Color color = Player.GetColor(newColor);
        foreach (VisualEffect effect in vfx)
        {

            effect.SetVector4("Color", color);
        }
    }

    public void TriggerColorEffects()
    {
        foreach (VisualEffect effect in vfx)
        {
            effect.Play();
        }
    }
}
