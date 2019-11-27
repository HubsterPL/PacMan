using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class LevelManager
{

    public static int score;

    public static TileBase wallTile;
    public static TileBase pointTile;
    public static TileBase cageTile;
    public static TileBase BPTile;
    public static TileBase cherry;

    public static Vector2Int cageExit;

    public enum GhostState {
        CHASE,
        SCATTER
    }
    public static GhostState ghostState = GhostState.CHASE;

    public static Vector2Int mapSize;
    static Tilemap tilemap;

    public static PacmanAgent pacman;
    public static List<GhostAgent> ghosts = new List<GhostAgent>();

    public enum CellContent {
        EMPTY,
        WALL,
        POINT,
        GATE,
        BIG_POINT,
        CHERRY
    }

    static CellContent[,] mapData;

    public static void SetupMap(Vector2Int size, Tilemap tilemap) {
        LevelManager.tilemap = tilemap;
        mapSize = size;
        mapData = new CellContent[size.x, size.y];
        for(int x = 0; x< size.x; x++) {
            for (int y = 0; y < size.y; y++) {
                mapData[x, y] = CellContent.EMPTY;
            }
        }
        
    }

    public static void StartFrighten() {
        foreach(GhostAgent g in ghosts) {
            if (!g.isEaten && !g.isCaged)
                g.EnterFright();
        }
    }

    public static CellContent GetCell(Vector2Int pos) {
        return mapData[pos.x, pos.y];
    }

    public static void SetCell(Vector2Int pos, CellContent cell) {
        if (pos.x > mapSize.x || pos.x < 0 || pos.y > mapSize.y || pos.y < 0)
            return;

        mapData[pos.x, pos.y] = cell;
        Vector3Int p = (Vector3Int)pos;
        switch (cell) {
            case CellContent.WALL:
                tilemap.SetTile(p, wallTile);
                break;
            case CellContent.POINT:
                tilemap.SetTile(p, pointTile);
                break;
            case CellContent.GATE:
                tilemap.SetTile(p, cageTile);
                cageExit = pos;
                break;
            case CellContent.BIG_POINT:
                tilemap.SetTile(p, BPTile);
                break;
            case CellContent.CHERRY:
                tilemap.SetTile(p, cherry);
                break;
            case CellContent.EMPTY:
                tilemap.SetTile(p, null);
                break;
        }
    }

    public static bool IsCellWall(Vector2Int pos) {
        return LevelManager.GetCell(pos) == LevelManager.CellContent.WALL;
    }
}
