using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueChaseAI : AGhostAI
{
    public override Vector2Int ChooseNavigationTarget() {
        Vector2Int target = LevelManager.pacman.intPos + LevelManager.pacman.directionVector * 2;
        foreach(GhostAgent ghost in LevelManager.ghosts) {
            if(ghost.chaseGhostAI is RedChaseAI) {
                return target * 2 - ghost.intPos;
            }
        }
        return target;
    }

}
