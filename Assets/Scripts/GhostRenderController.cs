using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRenderController : MonoBehaviour
{

    public GameObject eye1, eye2, body;
    public Color defaultBodyColor;

    SpriteRenderer bodyRenderer;

    private void Start() {
        bodyRenderer = body.GetComponent<SpriteRenderer>();
        bodyRenderer.color = defaultBodyColor;
    }

    //direction%4 == 0 - right, 1 - up, 2 - left, 3 - down
    void TurnEyes(int direction) {
        float angle = (direction%4) * 90f;
        eye1.transform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }


}
