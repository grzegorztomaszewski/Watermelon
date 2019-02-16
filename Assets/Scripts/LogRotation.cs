using System.Collections;
using UnityEngine;

public class LogRotation : MonoBehaviour
{

    [System.Serializable] //this will allow us to edit it in the editor
    //a custom class representing a single rotation "element" of the log's rotation pattern
    private class RotationElement
    {
        //to get rid of an obnoxious warning about these fields not being initialized
#pragma warning disable 0649
        public float Speed;
        public float Duration;
#pragma warning restore 0649
    }

    [SerializeField] //attribute making private fields editable in the Unity Editor
    //the aforemention full rotation pattern of the log
    private RotationElement[] rotationPattern;

    //this will be set to the Wheel Joint 2D from the LogMotor object
    private WheelJoint2D wheelJoint;
    //something has to actually apply a force to the log through the Wheel Joint 2D
    private JointMotor2D motor;

    private void Awake()
    {
        //setting fields
        wheelJoint = GetComponent<WheelJoint2D>();
        motor = new JointMotor2D();
        //starting an infinitely looping coroutine defined below right when this script loads (awakes)
        StartCoroutine("PlayRotationPattern");
    }

    private IEnumerator PlayRotationPattern()
    {
        int rotationIndex = 0;
        //infinite coroutine loop
        while (true)
        {
            //working with physics, executing as if this was running in a FixedUpdate method
            yield return new WaitForFixedUpdate();

            motor.motorSpeed = rotationPattern[rotationIndex].Speed;
            //hard coded 10000, feel free to experiment with other torques if you wish
            motor.maxMotorTorque = 10000;
            //set the updated motor to be the motor "sitting" on the Wheel Joint 2D
            wheelJoint.motor = motor;

            //let the motor do its thing for the specified duration
            yield return new WaitForSecondsRealtime(rotationPattern[rotationIndex].Duration);
            rotationIndex++;
            //infinite loop through the rotationPattern
            rotationIndex = rotationIndex < rotationPattern.Length ? rotationIndex : 0;
        }
    }
}