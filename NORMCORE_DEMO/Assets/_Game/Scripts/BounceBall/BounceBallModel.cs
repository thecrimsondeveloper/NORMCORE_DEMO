using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RealtimeModel(createMetaModel: true)]

public partial class BounceBallModel
{
    [RealtimeProperty(1, true, true)]
    public PlayerColor _color;
}
