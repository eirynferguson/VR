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

        SetRoute();

        initialDelay = Random.Range(2.0f, 12.0f);
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

        //calculate velocity for this frame
        Vector3 velocity = displacement;
        velocity.Normalize();
        velocity *= 2.5f;

        //apply velocity
        Vector3 newPosition = transform.position;
        newPosition += velocity * Time.deltaTime;
        //Rigidbody rb = GetComponent<Rigidbody>();
        rb.MovePosition(newPosition);

        //align to velocity
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, velocity, 10.0f * Time.deltaTime, 0f);
        Quaternion rotation = Quaternion.LookRotation(desiredForward);
        rb.MoveRotation(rotation);
    }

    void SetRoute()
    {
        //randomise the next route
        routeNumber = Random.Range(0, 2);

        //set the route waypoints
        if (routeNumber == 0) route = new List<Transform>
            { wps[0], wps[1], wps[2] };
        else if (routeNumber == 1) route = new List<Transform>
            { wps[3], wps[4], wps[5] };
    

        //initialise position and waypoint counter
        transform.position = new Vector3(route[0].position.x, 0.0f, route[0].position.z);
        targetWP = 1;
    }
}
