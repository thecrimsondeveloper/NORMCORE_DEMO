using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] PlayerColor _playerColor;
    [SerializeField] SpriteRenderer _spriteRenderer;



    public void SetColor(PlayerColor color)
    {
        _playerColor = color;

        if (_playerColor == PlayerColor.Yellow)
        {
            _spriteRenderer.color = Color.yellow;
        }
        else if (_playerColor == PlayerColor.Red)
        {
            _spriteRenderer.color = Color.red;
        }
        else if (_playerColor == PlayerColor.Blue)
        {
            _spriteRenderer.color = Color.blue;
        }
    }
}
