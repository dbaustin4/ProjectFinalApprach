using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;

class Obstacle{
    public GameObject obstacle;
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;
    public float timeBetweenSpawn;
    private float spawnTime;


    void Update() {
        Spawn();
        spawnTime = Time.time + timeBetweenSpawn;
    
    }

    void Spawn(){
        float randomX = Utils.Random(minX, maxX); //similar to unity Random.Range
        float randomY = Utils.Random(minY, maxY);

        
    }


}
