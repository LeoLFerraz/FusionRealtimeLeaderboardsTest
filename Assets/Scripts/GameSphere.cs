using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class GameSphere : NetworkBehaviour {
    [Networked]
    public NetworkBool hit {get; set;}
}
