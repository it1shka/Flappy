using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    public GameObject wallPrefab;
    public float timeBtwSpawn = .75f;
    private float curTimeBtwSpawn;
    public Transform bound1, bound2;
    private List<Transform> walls;
    public float wallSpeed = 5f;
    public float addSpeed = .1f;
    void Start()
    {
        curTimeBtwSpawn = timeBtwSpawn;
        walls = new List<Transform>();
    }

    void Update()
    {
        curTimeBtwSpawn -= Time.deltaTime;
        if(curTimeBtwSpawn <= 0f)
        {
            Spawn();
            curTimeBtwSpawn = timeBtwSpawn;
        }

        foreach (var wall in walls)
            wall.position -= new Vector3(wallSpeed, 0f) * Time.deltaTime;
        if (walls.Count > 0)
        {
            var lastWall = walls[0];
            if (lastWall.position.x < -11f)
            {
                walls.Remove(lastWall);
                Destroy(lastWall.gameObject);
            }
        }
    }

    private void Spawn()
    {
        var wall = Instantiate(wallPrefab, 
            new Vector3(
                Mathf.Max(bound1.position.x, bound2.position.x), 
                Random.Range(bound1.position.y, bound2.position.y)), 
            Quaternion.identity);
        walls.Add(wall.transform);
    }

    public void IncreaseSpeed()
    {
        wallSpeed += addSpeed;
    }
}
