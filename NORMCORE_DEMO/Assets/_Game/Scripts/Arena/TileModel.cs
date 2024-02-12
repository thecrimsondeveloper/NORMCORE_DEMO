using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RealtimeModel]
public partial class TileModel
{
    [RealtimeProperty(1, true, true)]
    public PlayerColor _playerColor;
}
