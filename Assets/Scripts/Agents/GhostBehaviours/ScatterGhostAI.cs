using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterGhostAI : AGhostAI
{
    public enum GhostColor {
        RED, PINK, ORANGE, BLUE
    }
    public GhostColor ghostColor;

    public override Vector2Int ChooseNavigationTarget() {
        if (ghostColor == GhostColor.BLUE)
            return new Vector2Int(100, -100);
        else if (ghostColor == GhostColor.ORANGE)
            return new Vector2Int(-100, -100);
        else if (ghostColor == GhostColor.PINK)
            return new Vector2Int(-100, 100);
        else
            return new Vector2Int(100, 100);
    }

}
