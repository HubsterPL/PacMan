using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PacmanAgent : ALevelAgent
{
    public MoveDirection nextDirection;
    public Transform renderTransform;

    public void Start() {
        intPos = Vector2Int.FloorToInt(transform.position);
        CurrentDirection = currentDirection;
        Turn(CurrentDirection);
        LevelManager.pacman = this;
    }

    private void Update() {

        if (Input.GetButtonDown("Horizontal")) {
            if (Input.GetAxisRaw("Horizontal") > 0f)
                nextDirection = MoveDirection.EAST;
            else
                nextDirection = MoveDirection.WEST;
        }
        else if (Input.GetButtonDown("Vertical")) {
            if (Input.GetAxisRaw("Vertical") > 0f)
                nextDirection = MoveDirection.NORTH;
            else
                nextDirection = MoveDirection.SOUTH;
        }

        Move();


        if (nextDirection == MoveDirection.NORTH || nextDirection == MoveDirection.SOUTH) {
            if (vertical)
                Turn(nextDirection);
        }
        else {
            if (!vertical)
                Turn(nextDirection);
        }

        if (transition.magnitude > 1f) {
            // A chance to turn
            Turn(nextDirection);

            // Picking up stuff
            switch (LevelManager.GetCell(intPos)) { 
                case LevelManager.CellContent.POINT:
                    LevelManager.SetCell(intPos, LevelManager.CellContent.EMPTY);
                    LevelManager.score += 100;
                    break;
                case LevelManager.CellContent.CHERRY:
                    LevelManager.SetCell(intPos, LevelManager.CellContent.EMPTY);
                    LevelManager.score += 500;
                    break;
                case LevelManager.CellContent.BIG_POINT:
                    LevelManager.SetCell(intPos, LevelManager.CellContent.EMPTY);
                    LevelManager.score += 200;
                    LevelManager.StartFrighten();
                    break;
            }
        }

    }

    public override void RenderTurn(Quaternion newRotation) {
        renderTransform.rotation = newRotation;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("Col");
        GhostAgent ghostAgent = collision.transform.GetComponent<GhostAgent>();
        if (ghostAgent.isFrightened)
            ghostAgent.EnterEaten();
        else if (!ghostAgent.isEaten)
            SceneManager.LoadScene(0); // Not enough time for proper GAME OVER, sorry :/
    }

}
