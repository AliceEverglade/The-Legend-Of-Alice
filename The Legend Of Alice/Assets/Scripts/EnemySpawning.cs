using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public Transform prefab;
    public float spread = 5f;
    public Vector3 min;
    public Vector3 max;
    private Vector3 rand;
    public int spawnAmount = 10;
    // Start is called before the first frame update
    void Start()
    {
        min = new Vector3(-spread, -spread, -spread);
        max = new Vector3(spread, spread, spread);
        for(int i = 0; i < spawnAmount; i++)
        {
            rand = new Vector3(Random.Range(min.x,max.x),Random.Range(min.y,max.y),Random.Range(min.z,max.z));
            Instantiate(prefab, new Vector3(60, 60, 60) + rand, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
