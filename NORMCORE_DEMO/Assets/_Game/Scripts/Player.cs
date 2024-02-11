using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class Player : RealtimeComponent<PlayerModel>
{
    [SerializeField] SpriteRenderer spriteRenderer;

    private void Awake()
    {
        // // When the color changes, update the sprite renderer
        // model.colorDidChange += ColorDidChange;
        // model.nameDidChange += NameDidChange;

    }

    private void Start()
    {
        SetColor(Color.white);
    }

    private void Update()
    {
        if (realtimeView.isOwnedLocallyInHierarchy == false) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Color[] colors = new Color[] { Color.red, Color.green, Color.blue, Color.yellow, Color.magenta, Color.cyan };
            Color random = colors[Random.Range(0, colors.Length)];
            SetColor(random);
        }
    }

    //set color
    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
        // Set the color property locally and on the network
        if (model != null) model.color = color;
    }

    private void ColorDidChange(PlayerModel model, Color value)
    {
        spriteRenderer.color = value;
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
            // Update the sprite renderer to match the new model
            ColorDidChange(currentModel, currentModel.color);
            NameDidChange(currentModel, currentModel.name);

            // Register for events so we'll know if the color changes later
            currentModel.colorDidChange += ColorDidChange;
            currentModel.nameDidChange += NameDidChange;
        }
    }
}
