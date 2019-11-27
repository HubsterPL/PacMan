using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedChaseAI : AGhostAI
{
    public override Vector2Int ChooseNavigationTarget() {
        return LevelManager.pacman.intPos;
    }
}
