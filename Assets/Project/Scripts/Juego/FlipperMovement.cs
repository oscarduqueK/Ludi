using UnityEngine;
using UnityEngine.Audio; // 👈 Necesario para usar AudioMixerGroup

[RequireComponent(typeof(HingeJoint2D))]
[RequireComponent(typeof(AudioSource))]
public class FlipperMovement2D : MonoBehaviour
{
    [Header("Controles")]
    public KeyCode key;
    public bool invert = false;

    [Header("Motor")]
    public float motorSpeed = 1000f;
    public float motorForce = 100f;
    public float restAngle = 0f;
    public float pressedAngle = 45f;

    [Header("Sonido")]
    public AudioClip flipperSound;
    public AudioMixerGroup mixerGroup;

    private HingeJoint2D hinge;
    private JointMotor2D motor;
    private JointAngleLimits2D limits;
    private AudioSource audioSource;
    private bool pressed;
    private bool wasPressed;

    void Awake()
    {
        hinge = GetComponent<HingeJoint2D>();
        audioSource = GetComponent<AudioSource>();

        motor = hinge.motor;
        limits = hinge.limits;

        hinge.useLimits = true;
        hinge.useMotor = true;

        limits.min = Mathf.Min(restAngle, pressedAngle);
        limits.max = Mathf.Max(restAngle, pressedAngle);
        hinge.limits = limits;

        if (mixerGroup != null)
            audioSource.outputAudioMixerGroup = mixerGroup;

        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.spatialBlend = 0f;
    }

    void Update()
    {
        pressed = Input.GetKey(key);

        if (pressed && !wasPressed)
        {
            PlayFlipperSound();
        }

        float direction = pressed ? 1f : -1f;
        if (invert) direction *= -1f;

        motor.motorSpeed = motorSpeed * direction;
        motor.maxMotorTorque = motorForce;
        hinge.motor = motor;

        wasPressed = pressed;
    }

    private void PlayFlipperSound()
    {
        if (flipperSound == null) return;
        audioSource.PlayOneShot(flipperSound);
    }
}
