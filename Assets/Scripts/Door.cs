using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class Door : MonoBehaviour
{
    private HingeJoint joint;
    private JointLimits angleLimit;

    [SerializeField]
    private HingeJoint handle;

    [SerializeField]
    private float lockedAngle = 2f;

    [SerializeField][Range(0f, 359f)]
    private float maxHandleLockedAngle = 20f;

    static JointLimits zeroAngleLimits = new();

    private void Start()
    {
        TryGetComponent<HingeJoint>(out joint);
        angleLimit = joint.limits;
    }

    private void FixedUpdate()
    {
        if (Math.Abs(joint.angle) > lockedAngle || Math.Abs(handle.angle) > maxHandleLockedAngle)
            joint.limits = angleLimit;
        else if (joint.limits.min != joint.limits.max)
            joint.limits = zeroAngleLimits;
    }
}
