using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {
    List<Transform> waypoints;
    int waypointIndex = 0;
    WaveConfig waveConfig;

    public void setWaveConfig(WaveConfig WhatWave)
    {
        this.waveConfig = WhatWave;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();

        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
    }

    private void EnemyMovement()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetposition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetposition, movementThisFrame);

            if (transform.position == targetposition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
