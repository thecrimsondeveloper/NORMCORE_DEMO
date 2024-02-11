using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RealtimeModel]
public partial class PlayerModel
{
    [RealtimeProperty(1, true, true)]
    private Color _color;

    [RealtimeProperty(2, true, true)]
    private string _name;
}
