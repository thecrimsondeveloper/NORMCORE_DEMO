using System.Collections;
using System.Collections.Generic;
using Normal.Realtime.Serialization;
using UnityEngine;

[RealtimeModel]
public partial class ArenaModel
{
    [RealtimeProperty(1, true, true)]
    public float _timer = 0;

}
