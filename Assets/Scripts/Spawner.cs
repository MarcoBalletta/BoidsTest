using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private uint numberOfSpawn;
    [SerializeField] private Boid boid;
    [SerializeField] private uint borderLimit;
    public List<Boid> boids = new List<Boid>();
    // Start is called before the first frame update
    void Start()
    {
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);

        for(int i = 0; i< numberOfSpawn; i++)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(borderLimit, screenSize.x - borderLimit), Random.Range(borderLimit, screenSize.y - borderLimit), Camera.main.nearClipPlane)); 
            var boidInst = Instantiate(boid, position, Quaternion.identity);
            boidInst.spawner = this;
            boids.Add(boidInst);
        }
    }
}
