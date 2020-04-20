using rclcs;
using sensor_msgs.msg;
using UnityEngine;

public class ImuSensor : MonoBehaviourRosSensorNode<Imu>
{
    private TwistBaseController twistBaseController;
    private bool outputLinearAcceleration = false;

    protected override void StartRos()
    {
        twistBaseController = GetComponent<TwistBaseController>();
        if (twistBaseController == null)
        {
            Debug.LogWarning("TwistBaseController not found on GameObject {name}");
        }

        base.StartRos();
    }
    
    protected override Imu Read()
    {
        if (twistBaseController == null)
        {
            return null;
        }

        Imu imu = new Imu();
        imu.Header.Update(clock);
        imu.Header.Frame_id = "base_footprint";

        imu.Orientation.Unity2Ros(transform.rotation);
        
        imu.Angular_velocity.Unity2Ros(twistBaseController.commandVelocityAngular);

        if (outputLinearAcceleration)
        {
            imu.Linear_acceleration.Unity2Ros(twistBaseController.currentLinearAcceleration);
        }
        else
        {
            imu.Linear_acceleration_covariance[0] = -1;
        }

        return imu;
    }
}
