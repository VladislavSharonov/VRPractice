using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
public class Button : MonoBehaviour
{
    [SerializeField][Range(0f, 1f)]
    private float threshold = 0f;

    [SerializeField]
    private float deadzone = 0f;

    public delegate void ButtonHandler();
    public event ButtonHandler OnReleased;
    public event ButtonHandler OnPressed;

    private ConfigurableJoint joint;
    private Vector3 startPosition;
    private bool isPressed;

    private void Start()
    {
        joint = GetComponent<ConfigurableJoint>();
        startPosition = transform.position;

//#if UNITY_EDITOR
        OnPressed += () => Debug.Log("Button is pressed");
        OnReleased += () => Debug.Log("Button is released");
//#endif

        OnPressed += () => isPressed = true;
        OnReleased += () => isPressed = false;
    }

    private void FixedUpdate()
    {
        CheckState();
    }

    private void CheckState()
    {
        var offset = Vector3.Distance(startPosition, transform.localPosition) / joint.linearLimit.limit;

        if (!isPressed && offset + threshold >= 1)
            OnPressed?.Invoke();
        else if (isPressed && offset - threshold <= 0)
            OnReleased?.Invoke();
    }
}
