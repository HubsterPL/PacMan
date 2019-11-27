using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ALevelAgent : MonoBehaviour
{
    public enum MoveDirection {
        NORTH, WEST, EAST, SOUTH
    }

    public bool vertical;
    public bool ignoreCageGate = false;

    public MoveDirection CurrentDirection {
        get { return currentDirection; }
        set {
            currentDirection = value;
            switch (currentDirection) {
                case MoveDirection.NORTH:
                    directionVector = Vector2Int.up;
                    vertical = true;
                    break;
                case MoveDirection.SOUTH:
                    directionVector = Vector2Int.down;
                    vertical = true;
                    break;
                case MoveDirection.EAST:
                    directionVector = Vector2Int.right;
                    vertical = false;
                    break;
                case MoveDirection.WEST:
                    directionVector = Vector2Int.left;
                    vertical = false;
                    break;
            }
        }
    }
    [SerializeField]protected MoveDirection currentDirection = MoveDirection.EAST;
    public Vector2Int directionVector;
    public Vector2Int intPos;
    public Vector2 transition;

    public void Start() {
        intPos = Vector2Int.FloorToInt(transform.position);
        CurrentDirection = currentDirection;
        Turn(CurrentDirection);
    }


    public bool CheckTile(Vector2Int pos) {

        //Vector2Int nextPos = intPos+Vector2Int.RoundToInt(directionVector);
        if (!ignoreCageGate)
            if (LevelManager.GetCell(pos) == LevelManager.CellContent.GATE)
                return false;

        if (LevelManager.GetCell(pos) != LevelManager.CellContent.WALL)
            return true;
        return false;

    }

    protected void Move() {
       
        if (CheckTile(intPos + Vector2Int.RoundToInt(directionVector))) {
            transform.position += Time.deltaTime * (Vector3)(Vector2)directionVector * 3f;
            transition = (Vector2)transform.position - (Vector2)intPos;
        }
        else
            transform.position = (Vector2)intPos;

        if (transition.magnitude > 1f)
            intPos = Vector2Int.RoundToInt(transform.position);
    }

    protected void Turn(MoveDirection nextDirection) {
        switch (nextDirection) {
            case MoveDirection.NORTH:
                //if (CurrentDirection != MoveDirection.SOUTH) {
                    if (CheckTile(intPos + Vector2Int.up)) {
                        CurrentDirection = nextDirection;
                        transform.position = new Vector3(intPos.x, transform.position.y);
                        RenderTurn(Quaternion.Euler(0f, 0f, 90f));
                    }
                //}
                break;
            case MoveDirection.SOUTH:
                //if (CurrentDirection != MoveDirection.NORTH) {
                    if (CheckTile(intPos + Vector2Int.down)) {
                        CurrentDirection = nextDirection;
                        transform.position = new Vector3(intPos.x, transform.position.y);
                        RenderTurn(Quaternion.Euler(0f, 0f, 270f));
                    }
                //}
                break;
            case MoveDirection.EAST:
                //if (CurrentDirection != MoveDirection.WEST) {
                    if (CheckTile(intPos + Vector2Int.right)) {
                        CurrentDirection = nextDirection;
                        transform.position = new Vector3(transform.position.x, intPos.y);
                        RenderTurn(Quaternion.Euler(0f, 0f, 0));
                    }
                //}
                break;
            case MoveDirection.WEST:
                //if (CurrentDirection != MoveDirection.EAST) {
                    if (CheckTile(intPos + Vector2Int.left)) {
                        CurrentDirection = nextDirection;
                        transform.position = new Vector3(transform.position.x, intPos.y);
                        RenderTurn(Quaternion.Euler(0f, 0f, 180f));
                    }
                //}
                break;

        }
    }

    public abstract void RenderTurn(Quaternion newRotation);

}
