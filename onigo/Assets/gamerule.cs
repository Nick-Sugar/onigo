using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Agent))]
public class gamerule : Agent
{
    float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<RayPerceptionSensorComponent3D>().DetectableTags.Add("Player");
    }

    // Update is called once per frame
    void Update()
    {


        Vector3 move = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            move = new Vector3(0, 0, 0.5f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            move = new Vector3(0, 0, -0.5f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            move = new Vector3(-0.5f, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            move = new Vector3(0.5f, 0, 0);
        }


        Vector3 rotation = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.Q))
        {
            move = new Vector3(-90f,0,0);
        }
        if (Input.GetKey(KeyCode.Z))
        {
            move = new Vector3(90f,0,0);
        }

        this.gameObject.GetComponent<Rigidbody>().angularVelocity = rotation;
        
        this.gameObject.GetComponent<Rigidbody>().AddForce(move * speed, ForceMode.Impulse);
    }

    public override void OnEpisodeBegin()
    {
        this.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.transform.localPosition = new Vector3(0, 0.5f, 0);
    }
    public override void CollectObservations(VectorSensor sensor)
    {

    }
    public override void OnActionReceived(float[] vectorAction)
    {
        Vector3 position = new Vector3(vectorAction[0], vectorAction[1], vectorAction[2]);
        this.gameObject.GetComponent<Rigidbody>().AddForce(position, ForceMode.Impulse);
    }
}