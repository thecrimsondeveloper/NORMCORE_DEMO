using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using UnityEngine;

public class Tile : RealtimeComponent<TileModel>
{
    [SerializeField] PlayerColor _playerColor => model != null ? model.playerColor : PlayerColor.Default;
    [SerializeField] SpriteRenderer _spriteRenderer;

    private void PlayerColorDidChange(TileModel model, PlayerColor value)
    {

        Debug.Log("Color changed to: " + value);
        _spriteRenderer.color = Player.GetColor(value);
    }

    public void SetColor(PlayerColor color)
    {
        if (model != null)
            model.playerColor = color;
    }


    protected override void OnRealtimeModelReplaced(TileModel previousModel, TileModel currentModel)
    {

        Debug.Log("OnRealtimeModelReplaced");
        if (previousModel != null)
        {
            // Unregister from events
            previousModel.playerColorDidChange -= PlayerColorDidChange;
        }

        if (currentModel != null)
        {
            // cALL THE METHODS THAT NEED TO BE CALLED WHEN THE MODEL IS REPLACED
            PlayerColorDidChange(currentModel, currentModel._playerColor);

            // Register for events so we'll know if the color changes later
            currentModel.playerColorDidChange += PlayerColorDidChange;
        }
    }
}
