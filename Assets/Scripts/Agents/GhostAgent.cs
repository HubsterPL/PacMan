using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAgent : ALevelAgent
{

    public AGhostAI scatterGhostAI;
    public AGhostAI chaseGhostAI;
    public AGhostAI frightGhostAI;
    public AGhostAI eatenGhostAI;
    public GhostRenderController ghostRender;

    public bool isFrightened = false;
    public bool isEaten = false;
    public bool isCaged = true;
    public bool isLeavingCage = false;
    public Vector2Int inCagePos;

    public float frightTimer;

    public override void RenderTurn(Quaternion newRotation) {
        ghostRender.eye1.transform.rotation = newRotation;
        ghostRender.eye2.transform.rotation = newRotation;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (scatterGhostAI != null)
            scatterGhostAI.Initialize(this);
        if (chaseGhostAI != null)
            chaseGhostAI.Initialize(this);
        if (frightGhostAI != null)
            frightGhostAI.Initialize(this);
        if (eatenGhostAI != null)
            eatenGhostAI.Initialize(this);

        intPos = Vector2Int.FloorToInt(transform.position);
        inCagePos = intPos;
        CurrentDirection = currentDirection;

        LevelManager.ghosts.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2Int targetLoc;

        if (isCaged) {
            targetLoc = inCagePos;
            if (isLeavingCage) {
                targetLoc = LevelManager.cageExit;
                ignoreCageGate = true;
                if (intPos == targetLoc) {
                    isLeavingCage = false;
                    isCaged = false;
                }
            }
        }
        else if (isEaten)
            targetLoc = eatenGhostAI.ChooseNavigationTarget();
        else if (isFrightened) {
            targetLoc = frightGhostAI.ChooseNavigationTarget();
            frightTimer += Time.deltaTime;

            if (frightTimer > 6f) {
                if (frightTimer % 1f < .5f)
                    ghostRender.body.GetComponent<SpriteRenderer>().color = Color.white;
                else
                    ghostRender.body.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0f, 0f, 1f);
            }

            if (frightTimer > 10f)
                EnterDefault();
        }
        else if (LevelManager.ghostState == LevelManager.GhostState.CHASE)
            targetLoc = chaseGhostAI.ChooseNavigationTarget();
        else
            targetLoc = scatterGhostAI.ChooseNavigationTarget();

        Move();
        if (transition.magnitude > 1f)
            Turn(AGhostAI.ChooseNextDirection(this, targetLoc));

        ignoreCageGate = false;
    }

    public void EnterFright() {
        TurnAround();
        ghostRender.body.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0f, 0f, 1f);
        isFrightened = true;
        frightTimer = 0f;
    }

    public void EnterEaten() {
        ghostRender.body.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
        isEaten = true;
        isFrightened = false;
        ignoreCageGate = true;
    }

    public void EnterDefault() {
        ghostRender.body.GetComponent<SpriteRenderer>().color = ghostRender.defaultBodyColor;
        isFrightened = false;
        isEaten = false;
        
    }

    public void TurnAround() {
        switch (CurrentDirection) {
            case MoveDirection.NORTH:
                CurrentDirection = MoveDirection.SOUTH;
                break;
            case MoveDirection.SOUTH:
                CurrentDirection = MoveDirection.NORTH;
                break;
            case MoveDirection.EAST:
                CurrentDirection = MoveDirection.WEST;
                break;
            case MoveDirection.WEST:
                CurrentDirection = MoveDirection.EAST;
                break;
        }
    }
}
