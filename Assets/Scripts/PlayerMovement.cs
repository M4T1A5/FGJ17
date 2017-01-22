using UnityEngine;
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

    [Range(-1, 1)]
    public float UpsideDownThreshold = -0.1f;

    public bool DebugControls;

    public delegate void JumpDelegate();
    public event JumpDelegate PlayerJumpEvent;

    private bool allowJump = true;

    private Rigidbody rb;

    private Animator animator;

    private GamePadState prevState, currentState;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        PlayerIndex = (PlayerIndex) GetComponent<Player>().PlayerId;

        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Update()
    {
        currentState = GamePad.GetState(PlayerIndex, GamePadDeadZone.Circular);

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

        // Move forward when not in air or upside down
        if (allowJump && !IsUpsideDown())
        {
            var force = leftStick.y > 0 ? ForwardForce : ReverseForce;
            rb.AddForce(transform.forward * leftStick.y * force, ForceMode.Acceleration);

            if (leftStick.y > 0.1f)
            {
                animator.SetBool("Hop", true);
            }
            else
            {
                animator.SetBool("Hop", false);
            }
        }

        if (Mathf.Abs(leftStick.x) > 0.1f)
        {
            // Turn the seal with the "tail" as the pivot point
            transform.RotateAround(transform.position - transform.forward / 2, new Vector3(0, 1, 0),
                leftStick.x * RotationSpeed * Time.deltaTime);

            if(leftStick.y < 0.3f)
                animator.SetBool("Move", true);
            else
                animator.SetBool("Move", false);
        }
        else
            animator.SetBool("Move", false);

        // Jump/attack
        if (buttonAPressed && CanJump() && !IsUpsideDown())
        {
            rb.velocity = new Vector3();
            StartCoroutine(Jump());
            allowJump = false;
        }

        // Roll the seal around its local z-axis
        if (currentState.Triggers.Left > 0)
            rb.AddRelativeTorque(0, 0, RollForce);
        else if (currentState.Triggers.Right > 0)
            rb.AddRelativeTorque(0, 0, -RollForce);

        prevState = currentState;
    }

    private IEnumerator Jump()
    {
        animator.SetTrigger("StartJump");
        yield return new WaitForSeconds(0.24f);

        var angle = transform.rotation
                    * Quaternion.AngleAxis(-JumpAngle, new Vector3(1, 0, 0));
        var jumpVector = angle * Vector3.forward;
        rb.AddForce(jumpVector * 5, ForceMode.Impulse);

        if (PlayerJumpEvent != null)
            PlayerJumpEvent.Invoke();

        yield return new WaitForSeconds(0.45f);
        animator.SetTrigger("LandJump");

        yield return new WaitForSeconds(1.2f);

        allowJump = true;
    }

    private bool CanJump()
    {
        var ray = new Ray(transform.position, Vector3.down);

        var colliders = GetComponentsInChildren<Collider>();
        var totalBounds = new Bounds(transform.position, new Vector3());
        foreach (var collider in colliders)
        {
            totalBounds.Encapsulate(collider.bounds);
        }

        return Physics.Raycast(ray, totalBounds.extents.y) && allowJump;
    }

    private bool IsUpsideDown()
    {
        var dotProduct = Vector3.Dot(transform.up, Vector3.up);
        if (dotProduct < UpsideDownThreshold)
            return true;

        return false;
    }

    //public void OnCollisionStay(Collision collision)
    //{
    //    if (collision.transform.CompareTag("Ground"))
    //        allowJump = true;
    //}
}