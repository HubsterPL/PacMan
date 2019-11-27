using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeGhostAI : AGhostAI
{
    public override Vector2Int ChooseNavigationTarget() {
        if ((LevelManager.pacman.intPos - agent.intPos).magnitude > 8)
            return LevelManager.pacman.intPos;

        return agent.scatterGhostAI.ChooseNavigationTarget();
        
    }

}
