using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldtimer : MonoBehaviour
{

    public float time;

    public bool Reset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            Reset = true;
        }
        else
        {
            Reset = false;
        }
    }

}
