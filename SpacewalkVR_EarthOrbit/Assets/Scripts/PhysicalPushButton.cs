using UnityEngine;
using UnityEngine.Events;

public class PhysicalPushButton : MonoBehaviour
{
    [Header("Movement Limits")]
    [SerializeField] private float pushDistance = 0.04f;
    [SerializeField] private float returnSpeed = 10f;
    [SerializeField] private float triggerThreshold = 0.75f;

    [Header("Events")]
    public UnityEvent onPressed;
    public UnityEvent onReleased;

    private Vector3 initialLocalPos;
    private bool isPressed = false;
    private Transform pressingTransform;

    private void Start()
    {
        initialLocalPos = transform.localPosition;
    }

    private void Update()
    {
        if (pressingTransform != null)
        {
            // Calculate how far the hand has pushed the button along the Z axis
            Vector3 handLocal = transform.parent.InverseTransformPoint(pressingTransform.position);
            float targetZ = Mathf.Clamp(handLocal.z, initialLocalPos.z - pushDistance, initialLocalPos.z);
            transform.localPosition = new Vector3(initialLocalPos.x, initialLocalPos.y, targetZ);

            float pressRatio = Mathf.InverseLerp(initialLocalPos.z, initialLocalPos.z - pushDistance, transform.localPosition.z);
            if (!isPressed && pressRatio >= triggerThreshold)
            {
                isPressed = true;
                onPressed?.Invoke();
            }
            else if (isPressed && pressRatio < triggerThreshold)
            {
                isPressed = false;
                onReleased?.Invoke();
            }
        }
        else
        {
            // Spring back to resting position
            transform.localPosition = Vector3.Lerp(transform.localPosition, initialLocalPos, Time.deltaTime * returnSpeed);
            if (isPressed && Vector3.Distance(transform.localPosition, initialLocalPos) < 0.005f)
            {
                isPressed = false;
                onReleased?.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.name.Contains("Controller") || other.name.Contains("Poke"))
        {
            pressingTransform = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (pressingTransform != null && other.transform == pressingTransform)
        {
            pressingTransform = null;
        }
    }
}