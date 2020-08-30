using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

[RequireComponent(typeof(Rigidbody))]
public class gamerule : Agent
{
    float speed = 0.5f;

    public Transform Target;

    public worldtimer timer;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<RayPerceptionSensorComponent3D>().DetectableTags.Add("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(code))
                {
                    // 入力されたキー名を表示
                    Debug.Log(code.ToString());
                }
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(-transform.forward * speed, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(-transform.right * speed, ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(transform.right * speed, ForceMode.Impulse);
        }


        Vector3 rotation = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.Q))
        {
            this.gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 1, 0)*2.0f;
        }
        if (Input.GetKey(KeyCode.Z))
        {
            this.gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 1, 0) * -2.0f;
        }

        
        //this.gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 1, 0);

    }

    public override void OnEpisodeBegin()
    {
        this.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.transform.localPosition = new Vector3(UnityEngine.Random.value * 8 - 4, 0.5f, UnityEngine.Random.value * 8 - 4);
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.position);
        sensor.AddObservation(this.transform.rotation);
        sensor.AddObservation(Target);
    }
    public override void OnActionReceived(float[] vectorAction)
    {
        this.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * vectorAction[0], ForceMode.Impulse);
        this.gameObject.GetComponent<Rigidbody>().AddForce(-transform.forward * vectorAction[1], ForceMode.Impulse);
        this.gameObject.GetComponent<Rigidbody>().AddForce(-transform.right * vectorAction[2], ForceMode.Impulse);
        this.gameObject.GetComponent<Rigidbody>().AddForce(transform.right * vectorAction[3], ForceMode.Impulse);
        this.gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, vectorAction[4], 0) * -2.0f;
        this.gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, vectorAction[5], 0) * -2.0f;


        float enemyspace = Vector3.Distance(this.transform.localPosition, Target.localPosition);


        if (enemyspace < 1.0f)
        {
            SetReward(0.5f);
            EndEpisode();
        }

        if (timer.Reset)
        {
            SetReward(-0.5f);
            EndEpisode();
        }
    }
}