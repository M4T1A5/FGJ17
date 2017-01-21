﻿using UnityEngine;
using XInputDotNetPure;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float ForwardForce = 5;
    public float ReverseForce = 5;
    public float RotationSpeed = 10;
    public float JumpAngle = 45;
    public float RollForce = 5;
    public PlayerIndex PlayerIndex;

    public bool DebugControls;

    public delegate void JumpDelegate();
    public event JumpDelegate PlayerJumpEvent;

    private bool allowJump;

    private Rigidbody rb;

    private GamePadState prevState, currentState;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        PlayerIndex = (PlayerIndex) GetComponent<Player>().PlayerId;
    }

    private void Update()
    {
        currentState = GamePad.GetState(PlayerIndex);

        var leftStick = new Vector2(currentState.ThumbSticks.Left.X, currentState.ThumbSticks.Left.Y);
        var rightStick = new Vector2(currentState.ThumbSticks.Right.X, currentState.ThumbSticks.Right.Y);

        var buttonAPressed = currentState.Buttons.A == ButtonState.Released && prevState.Buttons.A == ButtonState.Pressed;

        if (DebugControls)
        {
            float horizontal = 0;
            float vertical = 0;

            if (Input.GetKey(KeyCode.A))
                horizontal += -1;
            if (Input.GetKey(KeyCode.D))
                horizontal += 1;

            if (Input.GetKey(KeyCode.W))
                vertical += 1;
            if (Input.GetKey(KeyCode.S))
                vertical += -1;

            leftStick = new Vector2(horizontal, vertical);
            buttonAPressed = Input.GetKeyUp(KeyCode.Space);
        }

        if (allowJump)
        {
            var force = leftStick.y > 0 ? ForwardForce : ReverseForce;
            rb.AddForce(transform.forward * leftStick.y * force, ForceMode.Acceleration);
        }

        transform.RotateAround(transform.position - transform.forward / 2, new Vector3(0, 1, 0), leftStick.x * RotationSpeed * Time.deltaTime);

        if (buttonAPressed && allowJump)
        {
            rb.velocity = new Vector3();

            var angle = transform.rotation
                * Quaternion.AngleAxis(-JumpAngle, new Vector3(1, 0, 0));
            var jumpVector = angle * Vector3.forward;
            rb.AddForce(jumpVector * 5, ForceMode.Impulse);

            if (PlayerJumpEvent != null)
                PlayerJumpEvent.Invoke();

            allowJump = false;
        }

        if (currentState.Triggers.Left > 0)
        {
            rb.AddRelativeTorque(0, 0, RollForce);
        }
        else if (currentState.Triggers.Right > 0)
        {
            rb.AddRelativeTorque(0, 0, -RollForce);
        }

        prevState = currentState;
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
            allowJump = true;
    }
}