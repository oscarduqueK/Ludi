using UnityEngine;

public class FlipperMovement2D : MonoBehaviour
{

    public KeyCode key;
    public bool invert = false;
    public float motorSpeed = 1000f;
    public float motorForce = 8000f;
    public float restAngle = 0f;
    public float pressedAngle = 45f;
    private HingeJoint2D hinge;
    private JointMotor2D motor;
    private JointAngleLimits2D limits;
    private bool pressed;

    void Awake()
    {
        hinge = GetComponent<HingeJoint2D>();
        motor = hinge.motor;
        limits = hinge.limits;

        hinge.useLimits = true;
        hinge.useMotor = true;

        limits.min = Mathf.Min(restAngle, pressedAngle);
        limits.max = Mathf.Max(restAngle, pressedAngle);
        hinge.limits = limits;
    }

    void Update()
    {
        pressed = Input.GetKey(key);

        float direction = pressed ? 1f : -1f;
        if (invert) direction *= -1f;

        motor.motorSpeed = motorSpeed * direction;
        motor.maxMotorTorque = motorForce;
        hinge.motor = motor;
    }
}
