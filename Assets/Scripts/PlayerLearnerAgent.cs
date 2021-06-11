using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class PlayerLearnerAgent : Agent
{
    private BufferSensorComponent _buffer;
    private ProjectileManager _projectileManager;

    public float agentSpeed = 9;

    private void Awake()
    {
        _buffer = GetComponent<BufferSensorComponent>();
        _projectileManager = FindObjectOfType<ProjectileManager>();
    }

    private void FixedUpdate()
    {
        AddReward(.0001f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);

        foreach(GameObject projectile in _projectileManager.bullets)
        {
            if(projectile.activeInHierarchy)
            {
                float[] overseer = { projectile.transform.position.x, projectile.transform.position.y };
                _buffer.AppendObservation(overseer);
            }
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float X = actions.ContinuousActions[0];
        transform.position += new Vector3(X, 0) * agentSpeed * Time.deltaTime;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Bola"))
        {
            collision.gameObject.SetActive(false);
            SetReward(-1f);
            EndEpisode();
            transform.localPosition = Vector3.zero;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            AddReward(-.1f);
        }
    }
}
