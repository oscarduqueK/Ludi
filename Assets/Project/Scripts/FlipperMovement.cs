using UnityEngine;

public class FlipperMovement2D : MonoBehaviour
{
    public KeyCode key;
    public bool invert = false;
    public float motorSpeed;
    public float motorForce;
    public float restAngle;
    public float pressedAngle;

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

        // 👇 Si está invertido, intercambiamos los ángulos
        if (invert)
        {
            float temp = restAngle;
            restAngle = pressedAngle;
            pressedAngle = temp;
        }

        // 👇 Establecemos los límites correctamente
        limits.min = Mathf.Min(restAngle, pressedAngle);
        limits.max = Mathf.Max(restAngle, pressedAngle);
        hinge.limits = limits;
    }

    void Update()
    {
        pressed = Input.GetKey(key);

        float direction = pressed ? 1f : -1f;

        // 👇 Invertimos solo la dirección del motor
        if (invert) direction *= -1f;

        motor.motorSpeed = motorSpeed * direction;
        motor.maxMotorTorque = motorForce;
        hinge.motor = motor;
    }
}
