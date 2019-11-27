using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrightenedAI : AGhostAI
{
    public override Vector2Int ChooseNavigationTarget() {
        return new Vector2Int(Random.Range(0, LevelManager.mapSize.x), Random.Range(0, LevelManager.mapSize.y));
    }
}
