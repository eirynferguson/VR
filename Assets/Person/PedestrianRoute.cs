using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianRoute : MonoBehaviour
{
    public List<Transform> wps;
    public List<Transform> route;
    public int routeNumber = 0;
    public int targetWP;

    // Start is called before the first frame update
    void Start()
    {
        wps = new List<Transform>();
        GameObject wp;

        wp = GameObject.Find("WP1");
        wps.Add(wp.transform);

        wp = GameObject.Find("WP2");
        wps.Add(wp.transform);

        wp = GameObject.Find("WP3");
        wps.Add(wp.transform);

        wp = GameObject.Find("WP4");
        wps.Add(wp.transform);

        wp = GameObject.Find("WP5");
        wps.Add(wp.transform);

        wp = GameObject.Find("WP6");
        wps.Add(wp.transform);

        wp = GameObject.Find("WP7");
        wps.Add(wp.transform);

        wp = GameObject.Find("WP8");
        wps.Add(wp.transform);

        SetRoute();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetRoute()
    {
        //randomise the next route
        routeNumber = Random.Range(0, 12);

        //set the route waypoints
        if (routeNumber == 0) route = new List<Transform> 
            { wps[0], wps[4],wps[5], wps[6] };
        else if (routeNumber == 1) route = new List<Transform> 
            { wps[0], wps[4], wps[5], wps[7] };
        else if (routeNumber == 2) route = new List<Transform> 
            { wps[2], wps[1], wps[4], wps[5], wps[6] };
        else if (routeNumber == 3) route = new List<Transform> 
            { wps[2], wps[1], wps[4], wps[5], wps[7] };
        else if (routeNumber == 4) route = new List<Transform> 
            { wps[3], wps[4], wps[5], wps[6] };
        else if (routeNumber == 5) route = new List<Transform> 
            { wps[3], wps[4], wps[5], wps[7] };
        else if (routeNumber == 6) route = new List<Transform> 
            { wps[6], wps[5], wps[4], wps[0] };
        else if (routeNumber == 7) route = new List<Transform> 
            { wps[6], wps[5], wps[4], wps[3] };
        else if (routeNumber == 8) route = new List<Transform> 
            { wps[6], wps[5], wps[4], wps[1], wps[2] };
        else if (routeNumber == 9) route = new List<Transform> 
            { wps[7], wps[5], wps[4], wps[0] };
        else if (routeNumber == 10) route = new List<Transform> 
            { wps[7],wps[5], wps[4], wps[3] };
        else if (routeNumber == 11) route = new List<Transform> 
            { wps[7], wps[5], wps[4], wps[1], wps[2] };

        //initialise position and waypoint counter
        transform.position = new Vector3(route[0].position.x, 0.0f,
        route[0].position.z);
        targetWP = 1;
    }
}
