using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatenAI : AGhostAI
{
    public override Vector2Int ChooseNavigationTarget() {
        agent.ignoreCageGate = true;
        if (agent.inCagePos == agent.intPos) {
            agent.EnterDefault();
            agent.isCaged = true;
            agent.isLeavingCage = true;
            
        }
        return agent.inCagePos;
    }

}
