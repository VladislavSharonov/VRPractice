using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ConfigurableJoint))]
public class Button : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Offset up to which the button is not pressed (always less than \"Actuation offset\")")]
    private float deadzone = 0.025f;

    [SerializeField]
    [Tooltip("Offset after which the button is pressed (always greater than \"deadzone\")")]
    private float actuationOffset = 0.1f;

    [Header("Events")]
    [SerializeField]
    private UnityEvent OnReleased;

    [SerializeField]
    private UnityEvent OnPressed;

    private ConfigurableJoint joint;
    private Vector3 startPosition;
    private bool isPressed;

    private void Start()
    {
        joint = GetComponent<ConfigurableJoint>();
        startPosition = transform.localPosition;
    }

    private void FixedUpdate()
    {
        var offset = Vector3.Distance(startPosition, transform.localPosition) / joint.linearLimit.limit;
        offset = Mathf.Abs(offset) < deadzone ? 0 : Mathf.Clamp(offset, 0f, 1f);

        if (!isPressed && offset + actuationOffset >= 1)
            Pressed();
        else if (isPressed && offset - actuationOffset <= 0)
            Released();
    }

    private void Pressed()
    {
        isPressed = true;
        OnPressed?.Invoke();
        Debug.Log("1");
    }

    private void Released()
    {
        isPressed = false;
        OnReleased?.Invoke();
        Debug.Log("2");
    }
}
