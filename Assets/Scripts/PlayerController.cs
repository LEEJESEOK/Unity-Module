using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    MovementBehavior movement;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<MovementBehavior>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckMoveInput();
    }

    void CheckMoveInput()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 moveVector = new Vector3(h, 0, v);

        if (moveVector.magnitude == 0)
            return;

        // movement.Move(moveVector);
        movement.MoveFPS(moveVector);
    }
}
