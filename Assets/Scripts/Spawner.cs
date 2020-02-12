using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] hazards;
    public GameObject[] boosters;
    public GameObject[] powers;

    private float timeBtwSpawns = 0;
    public float minTimeBetweenSpawns;
    public float maxTimeBetweenSpawns;
    public float FixedMaxTime;
    public float decrease;
    
    private float timeBtwBoosters = 0;
    public float minTimeBetweenBoosters;
    public float maxTimeBetweenBoosters;
    public float FixedMinTimeBooster;
    public float increase;
    
    private float timeBtwPowers = 0;
    public float minTimeBetweenPowers;
    public float maxTimeBetweenPowers;

    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (Pause.instance != null && Pause.instance.IsPaused)
        {
            return;
        }

        if (player != null)
        {
            
            if (timeBtwSpawns <= 0)
            {
                Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                GameObject randomHazard = hazards[Random.Range(0, hazards.Length)];
                Spawn r = randomSpawnPoint.GetComponentInChildren(typeof(Spawn)) as Spawn;


                float randomAngle = Random.Range(360 - r.MinAngle, 360 + r.MaxAngle) - 360;
                //Debug.Log("Random Angle: " + randomAngle);
                Instantiate(randomHazard, randomSpawnPoint.position, Quaternion.Euler(0, 0, randomAngle));
                //Instantiate(randomHazard, randomSpawnPoint.position, Quaternion.Euler(0, 0, 0));

                if (maxTimeBetweenSpawns > FixedMaxTime)
                {
                    maxTimeBetweenSpawns -= decrease;
                }

                timeBtwSpawns = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
                //Debug.Log("New Time Btw " + hazards[0].ToString() + " (" + minTimeBetweenSpawns + "-" + maxTimeBetweenSpawns + "): " + timeBtwSpawns);
            }
            else
            {
                timeBtwSpawns -= Time.deltaTime;
            }
            

            
            if (timeBtwBoosters <= 0)
            {
                Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                GameObject randomHazard = boosters[Random.Range(0, boosters.Length)];
                Spawn r = randomSpawnPoint.GetComponentInChildren(typeof(Spawn)) as Spawn;


                float randomAngle = Random.Range(360 - r.MinAngle, 360 + r.MaxAngle) - 360;
                //Debug.Log("Random Angle: " + randomAngle);
                Instantiate(randomHazard, randomSpawnPoint.position, Quaternion.Euler(0, 0, randomAngle));
                //Instantiate(randomHazard, randomSpawnPoint.position, Quaternion.Euler(0, 0, 0));
                
                maxTimeBetweenBoosters += increase;

                timeBtwBoosters = Random.Range(minTimeBetweenBoosters, maxTimeBetweenBoosters);
                //Debug.Log("New Time Btw " + boosters[0].ToString() + " (" + minTimeBetweenBoosters + "-" + maxTimeBetweenBoosters + "): " + timeBtwBoosters);
            }
            else
            {
                timeBtwBoosters -= Time.deltaTime;
            }



            if (timeBtwPowers <= 0)
            {
                Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                GameObject randomHazard = powers[Random.Range(0, powers.Length)];
                Spawn r = randomSpawnPoint.GetComponentInChildren(typeof(Spawn)) as Spawn;


                float randomAngle = Random.Range(360 - r.MinAngle, 360 + r.MaxAngle) - 360;
                //Debug.Log("Random Angle: " + randomAngle);
                Instantiate(randomHazard, randomSpawnPoint.position, Quaternion.Euler(0, 0, randomAngle));
                //Instantiate(randomHazard, randomSpawnPoint.position, Quaternion.Euler(0, 0, 0));
                

                timeBtwPowers = Random.Range(minTimeBetweenPowers, maxTimeBetweenPowers);
                //Debug.Log("New Time Btw " + Powers[0].ToString() + " (" + minTimeBetweenPowers + "-" + maxTimeBetweenPowers + "): " + timeBtwPowers);
            }
            else
            {
                timeBtwPowers -= Time.deltaTime;
            }
        }
    }
}
