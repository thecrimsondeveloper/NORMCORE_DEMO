using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Sirenix.OdinInspector;

public class Player : RealtimeComponent<PlayerModel>
{
    [SerializeField] SpriteRenderer spriteRenderer;

    [ShowInInspector] public PlayerColor color => model != null ? model.color : PlayerColor.Default;
    PlayerColor startingColor;
    private void Awake()
    {
        // // When the color changes, update the sprite renderer
        // model.colorDidChange += ColorDidChange;
        // model.nameDidChange += NameDidChange;

        startingColor = Arena.GetFirstAvailableColor();
        Arena.AddPlayer(this);

        transform.position = Arena.PlayerPosition(this);
    }

    void OnDestroy()
    {
        // // When the color changes, update the sprite renderer
        // model.colorDidChange -= ColorDidChange;
        // model.nameDidChange -= NameDidChange;
        Arena.RemovePlayer(this);
    }


    private void Update()
    {
        if (realtimeView.isOwnedLocallyInHierarchy == false) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Color[] colors = new Color[] { Color.red, Color.green, Color.blue, Color.yellow, Color.magenta, Color.cyan };
            Color random = colors[Random.Range(0, colors.Length)];
        }
    }

    private void ColorDidChange(PlayerModel model, PlayerColor value)
    {
        Debug.Log("Color changed to: " + value);
        spriteRenderer.color = GetColor(value);
    }

    private void NameDidChange(PlayerModel model, string value)
    {
        Debug.Log("Name changed to: " + value);
    }

    protected override void OnRealtimeModelReplaced(PlayerModel previousModel, PlayerModel currentModel)
    {

        Debug.Log("OnRealtimeModelReplaced");
        if (previousModel != null)
        {
            // Unregister from events
            previousModel.colorDidChange -= ColorDidChange;
            previousModel.nameDidChange -= NameDidChange;
        }

        if (currentModel != null)
        {
            if (currentModel.color != startingColor) currentModel.color = startingColor;
            // Update the sprite renderer to match the new model
            // ColorDidChange(currentModel, currentModel.color);
            ColorDidChange(currentModel, startingColor);
            NameDidChange(currentModel, currentModel.name);

            // Register for events so we'll know if the color changes later
            currentModel.colorDidChange += ColorDidChange;
            currentModel.nameDidChange += NameDidChange;
        }
    }

    public static Color GetColor(PlayerColor color)
    {
        if (color == PlayerColor.Yellow)
        {
            return Color.yellow;
        }
        else if (color == PlayerColor.Red)
        {
            return Color.red;
        }
        else if (color == PlayerColor.Blue)
        {
            return Color.blue;
        }
        else if (color == PlayerColor.Green)
        {
            return Color.green;
        }
        else
        {
            return Color.clear;
        }
    }


}
