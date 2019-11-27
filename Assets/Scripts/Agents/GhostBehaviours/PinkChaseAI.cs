using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkChaseAI : AGhostAI
{
    public override Vector2Int ChooseNavigationTarget() {
        return LevelManager.pacman.intPos + LevelManager.pacman.directionVector * 4;
    }
}
