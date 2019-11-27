using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{

    string map00 =
        "wwwwwwwwwwwwwwwwwwwwwwwwwwww\n" +
        "w............ww............w\n" +
        "w.wwww.wwwww.ww.wwwww.wwww.w\n" +
        "wowwww.wwwww.ww.wwwww.wwwwow\n" +
        "w.wwww.wwwww.ww.wwwww.wwww.w\n" +
        "w..........................w\n" +
        "w.wwww.ww.wwwwwwww.ww.wwww.w\n" +
        "w.wwww.ww.wwwwwwww.ww.wwww.w\n" +
        "w......ww....ww....ww......w\n" +
        "wwwwww.wwwww ww wwwww.wwwwww\n" +
        "     w.wwwww ww wwwww.w     \n" +
        "     w.ww          ww.w     \n" +
        "     w.ww www--www ww.w     \n" +
        "wwwwww.ww w      w ww.wwwwww\n" +
        "     w.   w 1234 w   .w     \n" +
        "wwwwww.ww w      w ww.wwwwww\n" +
        "     w.ww wwwwwwww ww.w     \n" +
        "     w.ww    c     ww.w     \n" +
        "     w.ww wwwwwwww ww.w     \n" +
        "wwwwww.ww wwwwwwww ww.wwwwww\n" +
        "w............ww............w\n" +
        "w.wwww.wwwww.ww.wwwww.wwww.w\n" +
        "w.wwww.wwwww.ww.wwwww.wwww.w\n" +
        "w...ww........p.......ww...w\n" +
        "www.ww.ww.wwwwwwww.ww.ww.www\n" +
        "www.ww.ww.wwwwwwww.ww.ww.www\n" +
        "w......ww....ww....ww......w\n" +
        "wowwwwwwwwww.ww.wwwwwwwwwwow\n" +
        "w.wwwwwwwwww.ww.wwwwwwwwww.w\n" +
        "w..........................w\n" +
        "wwwwwwwwwwwwwwwwwwwwwwwwwwww";

    public TileBase wallTile;
    public TileBase pointTile;
    public TileBase cageTile;
    public TileBase BPTile;
    public TileBase cherry;

    public GameObject redGhost;
    public GameObject pinkGhost;
    public GameObject blueGhost;
    public GameObject orangeGhost;
    public GameObject pacman;

    Grid tilegrid;
    Tilemap tilemap;

    public Coroutine stateSwitch;

    // Start is called before the first frame update
    void Start()
    {
        tilegrid = gameObject.AddComponent<Grid>();
        tilemap = new GameObject("Tilemap").AddComponent<Tilemap>();
        tilemap.gameObject.AddComponent<TilemapRenderer>();
        tilemap.transform.SetParent(transform);
        LevelManager.wallTile = wallTile;
        LevelManager.pointTile = pointTile;
        LevelManager.cageTile = cageTile;
        LevelManager.BPTile = BPTile;
        LevelManager.cherry = cherry;
        GenerateLevel(map00);
        StartCoroutine("ExitCageSequence");
        stateSwitch = StartCoroutine("LevelStateSwitch");
    }

    IEnumerator ExitCageSequence() {
        Debug.Log("Start ExitCage Sequence");
        yield return new WaitForSeconds(1f);
        foreach (GhostAgent g in LevelManager.ghosts) {
            Debug.Log("Exit!");
            yield return new WaitForSeconds(1f);
            g.isLeavingCage = true;
            
        }
    }

    IEnumerator LevelStateSwitch() {
        yield return new WaitForSeconds(1f);
        while (true) {
            LevelManager.ghostState = LevelManager.GhostState.SCATTER;
            foreach (GhostAgent g in LevelManager.ghosts)
                if (g.isEaten == false && g.isFrightened == false)
                    g.TurnAround();
            yield return new WaitForSeconds(7f);
            LevelManager.ghostState = LevelManager.GhostState.CHASE;
            foreach (GhostAgent g in LevelManager.ghosts)
                if(g.isEaten == false && g.isFrightened == false)
                    g.TurnAround();
            yield return new WaitForSeconds(20f);
        }
    }

    public void GenerateLevel(string map) {
        string[] lines = map.Split('\n');
        GameObject go;
        int x = 0, y = lines.Length-1;

        LevelManager.SetupMap(new Vector2Int(lines[0].Length, lines.Length), tilemap);
        foreach (string line in lines) {

            foreach (char c in line) {
                switch (c) {
                    case 'w':
                        LevelManager.SetCell(new Vector2Int(x, y), LevelManager.CellContent.WALL);
                        break;
                    case '.':
                        LevelManager.SetCell(new Vector2Int(x, y), LevelManager.CellContent.POINT);
                        break;
                    case '1':
                         go = Instantiate(redGhost);
                        go.transform.position = new Vector3(x, y, 0f);
                        break;
                    case '2':
                         go = Instantiate(pinkGhost);
                        go.transform.position = new Vector3(x, y, 0f);
                        break;
                    case '3':
                         go = Instantiate(orangeGhost);
                        go.transform.position = new Vector3(x, y, 0f);
                        break;
                    case '4':
                         go = Instantiate(blueGhost);
                        go.transform.position = new Vector3(x, y, 0f);
                        break;
                    case 'p':
                         go = Instantiate(pacman);
                        go.transform.position = new Vector3(x, y, 0f);
                        break;
                    case '-':
                        LevelManager.SetCell(new Vector2Int(x, y), LevelManager.CellContent.GATE);
                        break;
                    case 'o':
                        LevelManager.SetCell(new Vector2Int(x, y), LevelManager.CellContent.BIG_POINT);
                        break;
                    case 'c':
                        LevelManager.SetCell(new Vector2Int(x, y), LevelManager.CellContent.CHERRY);
                        break;
                }

                x++;
            }
            x = 0;
            y--;
        }

        
    }
}
