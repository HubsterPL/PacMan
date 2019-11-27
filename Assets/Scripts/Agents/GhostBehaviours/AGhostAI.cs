using UnityEngine;

public abstract class AGhostAI : MonoBehaviour {

    protected GhostAgent agent;

    public abstract Vector2Int ChooseNavigationTarget();

    public void Initialize(GhostAgent agent) {
        this.agent = agent;
    }

    public static ALevelAgent.MoveDirection ChooseNextDirection(GhostAgent agent, Vector2Int target) {

        ALevelAgent.MoveDirection next = ALevelAgent.MoveDirection.EAST;
        float dist = float.MaxValue;


        if (agent.CurrentDirection != ALevelAgent.MoveDirection.SOUTH) {
            Vector2Int offsetIntPos = (agent.intPos + Vector2Int.up);
            if (!LevelManager.IsCellWall(offsetIntPos)) {
                float d = (offsetIntPos - target).magnitude;
                if(d < dist) {
                    dist = d;
                    next = ALevelAgent.MoveDirection.NORTH;
                }
            }
        }

        if (agent.CurrentDirection != ALevelAgent.MoveDirection.NORTH) {
            Vector2Int offsetIntPos = (agent.intPos + Vector2Int.down);
            if (!LevelManager.IsCellWall(offsetIntPos)) {
                float d = (offsetIntPos - target).magnitude;
                if (d < dist) {
                    dist = d;
                    next = ALevelAgent.MoveDirection.SOUTH;
                }
            }
        }

        if (agent.CurrentDirection != ALevelAgent.MoveDirection.WEST) {
            Vector2Int offsetIntPos = (agent.intPos + Vector2Int.right);
            if (!LevelManager.IsCellWall(offsetIntPos)) {
                float d = (offsetIntPos - target).magnitude;
                if (d < dist) {
                    dist = d;
                    next = ALevelAgent.MoveDirection.EAST;
                }
            }
        }
        if (agent.CurrentDirection != ALevelAgent.MoveDirection.EAST) {
            Vector2Int offsetIntPos = (agent.intPos + Vector2Int.left);
            if (!LevelManager.IsCellWall(offsetIntPos)) {
                float d = (offsetIntPos - target).magnitude;
                if (d < dist) {
                    dist = d;
                    next = ALevelAgent.MoveDirection.WEST;
                }
            }
        }

        return next;
        
    }

}