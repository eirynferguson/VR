using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public List<Transform> wps;
    public List<Transform> route;
    public int routeNumber = 0;
    public int targetWP;
    public float dist;
    public bool go = false;
    public float initialDelay;
    public bool isColliding = false;
    public Vector3 velocity;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        wps = new List<Transform>();
        GameObject wp;

        wp = GameObject.Find("CarWP1");
        wps.Add(wp.transform);

        wp = GameObject.Find("CarWP2");
        wps.Add(wp.transform);

        wp = GameObject.Find("CarWP3");
        wps.Add(wp.transform);

        wp = GameObject.Find("CarWP4");
        wps.Add(wp.transform);

        wp = GameObject.Find("CarWP5");
        wps.Add(wp.transform);

        wp = GameObject.Find("CarWP6");
        wps.Add(wp.transform);

        wp = GameObject.Find("CarWP7");
        wps.Add(wp.transform);
        
        wp = GameObject.Find("CarWP8");
        wps.Add(wp.transform);

        wp = GameObject.Find("CarWP9");
        wps.Add(wp.transform);

        wp = GameObject.Find("CarWP10");
        wps.Add(wp.transform);

        wp = GameObject.Find("CarWP11");
        wps.Add(wp.transform);

        wp = GameObject.Find("CarWP12");
        wps.Add(wp.transform);

        SetRoute();

        initialDelay = Random.Range(5.0f, 12.0f);
        transform.position = new Vector3(0.0f, -5.0f, 0.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!go)
        {
            initialDelay -= Time.deltaTime;
            if (initialDelay <= 0.0f)
            {
                go = true;
                SetRoute();
            }
            else return;
        }

        Vector3 displacement = route[targetWP].position - transform.position;
        displacement.y = 0;
        float dist = displacement.magnitude;

        if (dist < 0.1f)
        {
            targetWP++;
            if (targetWP >= route.Count)
            {
                SetRoute();
                return;
            }
        }

        if (isColliding == true)
        {
            //calculate velocity for this frame
            velocity = displacement;
            velocity.Normalize();
            velocity *= 0f;
        }
        else
        {
            //calculate velocity for this frame
            velocity = displacement;
            velocity.Normalize();
            velocity *= 5.5f;
        }
        

        //apply velocity
        Vector3 newPosition = transform.position;
        newPosition += velocity * Time.deltaTime;
        rb.MovePosition(newPosition);

        //align to velocity
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, velocity, 10.0f * Time.deltaTime, 0f);
        Quaternion rotation = Quaternion.LookRotation(desiredForward);
        rb.MoveRotation(rotation);
    }

    void SetRoute()
    {
        //randomise the next route
        routeNumber = Random.Range(0, 6);

        //set the route waypoints
        if (routeNumber == 0) route = new List<Transform>
            { wps[0], wps[1] };
        else if (routeNumber == 1) route = new List<Transform>
            { wps[2], wps[3] };
        else if (routeNumber == 2) route = new List<Transform>
            {wps[0], wps[11], wps[7], wps[6] };
        else if (routeNumber == 3) route = new List<Transform>
            { wps[2], wps[8], wps[7], wps [6] };
        else if (routeNumber == 4) route = new List<Transform>
            { wps[5], wps[4], wps[10], wps[1]};
       else if (routeNumber == 5) route = new List<Transform>
            { wps[5], wps[4], wps[9], wps[3]};


        //initialise position and waypoint counter
        transform.position = new Vector3(route[0].position.x, 0.6f, route[0].position.z);
        targetWP = 1;
    }

   void OnTriggerEnter(Collider other)
   {
        isColliding = true; 

   }

    void OnTriggerExit(Collider other)
    {
        isColliding = false;
    }
}
