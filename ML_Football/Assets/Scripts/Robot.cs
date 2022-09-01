using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class Robot : Agent
{
    [Header("Speed"), Range(1, 50)]
    public float speed = 10;

    Rigidbody rigRobot;
    Rigidbody rigBall;

 

    private void Start()
    {
        rigRobot = GetComponent<Rigidbody>();
        rigBall = GameObject.Find("Ball").GetComponent<Rigidbody>();
    }

    //�ƥ�}�l���]
    public override void OnEpisodeBegin()
    {
        rigRobot.velocity = Vector3.zero;
        rigRobot.angularVelocity = Vector3.zero;
        rigBall.velocity = Vector3.zero;
        rigBall.angularVelocity = Vector3.zero;

        Vector3 posRobot = new Vector3(Random.Range(5, 7), 1, Random.Range(-4,4));
        transform.position = posRobot;
        Vector3 posBall = new Vector3(Random.Range(2, 4), 0.32f, Random.Range(-4, 4));
        rigBall.transform.position = posBall;

        //���]���|�����\
        Ball.complete = false;
    }

    //�����[����� �y�лP�t��
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(rigBall.transform.position);
        sensor.AddObservation(rigRobot.velocity.x);
        sensor.AddObservation(rigRobot.velocity.z);


    }


    //���� ���y
    public override void OnActionReceived(ActionBuffers actions)
    {
        //���� Robot;
        Vector3 control = Vector3.zero;
        control.x = actions.ContinuousActions[0];
        control.z = actions.ContinuousActions[1];
        rigRobot.AddForce(control * speed);


        //���\
        if (Ball.complete)
        {
            SetReward(1);
            EndEpisode();
        }
        //����
        if (transform.position.y< 0 || rigBall.transform.position.y < 0)
        {
            SetReward(-1);
            EndEpisode();
        }
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }
}
