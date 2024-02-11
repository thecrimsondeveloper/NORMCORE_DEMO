using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RealtimeModel]
public partial class ArenaModel
{
    [RealtimeProperty(1, true, true)]
    private int _playerColors;
}
