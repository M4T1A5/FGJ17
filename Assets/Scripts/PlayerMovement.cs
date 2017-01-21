using UnityEngine;
using XInputDotNetPure;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float RotationSpeed = 10;
    public float JumpAngle = 45;
    public PlayerIndex PlayerIndex;

    public bool DebugControls;

    public delegate void JumpDelegate();
    public event JumpDelegate PlayerJumpEvent;

    private Rigidbody rb;

    private GamePadState prevState, currentState;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        var localRotationPoint = (-transform.forward / 2) * transform.localScale.z;
        rb.centerOfMass = localRotationPoint;
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

        if(!Mathf.Approximately(Mathf.Abs(leftStick.y), 0))
            rb.AddForce(transform.forward * leftStick.y * 15, ForceMode.Acceleration);

        transform.RotateAround(transform.position - transform.forward / 2, new Vector3(0, 1, 0), leftStick.x * RotationSpeed * Time.deltaTime);
        
        if (buttonAPressed && transform.position.y < 0.5)
        {
            var angle = transform.rotation 
                * Quaternion.AngleAxis(-JumpAngle, new Vector3(1, 0, 0));
            var jumpVector = angle * Vector3.forward;
            rb.AddForce(jumpVector * 5, ForceMode.Impulse);

            if (PlayerJumpEvent != null)
                PlayerJumpEvent.Invoke();
        }

        prevState = currentState;
    }
}