using UnityEngine;

/// <summary>
/// Controla un flipper (paleta) de pinball en 2D usando un HingeJoint2D + motor.
/// Aseg�rate de que el objeto tenga un Rigidbody2D y un HingeJoint2D.
/// </summary>
[RequireComponent(typeof(HingeJoint2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class FlipperMovement2D : MonoBehaviour
{
    [Header("Entrada")]
    public KeyCode key = KeyCode.LeftArrow;
    public bool invert = false; // marcar true para el flipper derecho

    [Header("Motor del flipper")]
    [Tooltip("Velocidad de rotaci�n al presionar la tecla.")]
    public float motorSpeed = 1000f;
    [Tooltip("Fuerza del motor.")]
    public float motorForce = 8000f;

    [Header("L�mites del �ngulo (grados)")]
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

        // Configura l�mites iniciales
        limits.min = Mathf.Min(restAngle, pressedAngle);
        limits.max = Mathf.Max(restAngle, pressedAngle);
        hinge.limits = limits;
    }

    void Update()
    {
        pressed = Input.GetKey(key);

        // Determinar direcci�n del motor
        float direction = pressed ? 1f : -1f;
        if (invert) direction *= -1f;

        // Aplica velocidad seg�n estado
        motor.motorSpeed = motorSpeed * direction;
        motor.maxMotorTorque = motorForce;
        hinge.motor = motor;
    }
}
