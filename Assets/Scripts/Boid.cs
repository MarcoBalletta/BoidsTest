using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public Spawner spawner;
    [SerializeField] private float _matchingFactor;
    [SerializeField] float alignWithOthersStrength; //factor determining turn rate to align with other boids
    [SerializeField] float moveToCenterStrength; //factor by which boid will try toward center Higher it is, higher the turn rate to move to the center
    [SerializeField] float avoidOtherStrength; //factor by which boid will try to avoid each other. Higher it is, higher the turn rate to avoid other.
    [SerializeField] float avoidWallsStrength; //factor by which boid will try to avoid walls. Higher it is, higher the turn rate to avoid other.
    [SerializeField] float speed;
    Vector2 direction;
    [SerializeField] private float localBoidsDistance;
    [SerializeField] private float collisionAvoidCheckDistance;
    [SerializeField] private float localBoidsAlignmentCheckDistance;

    void Update()
    {
        AlignWithOthers();
        MoveToCenter();
        AvoidOtherBoids();
        StayInsideWalls();
        transform.Translate(direction * (speed * Time.deltaTime));
    }

    void MoveToCenter()
    {

        Vector2 positionSum = transform.position;//calculate sum of position of nearby boids and get count of boid

        int count = 0;
        foreach (Boid boid in spawner.boids)
        {
            float distance = Vector2.Distance(boid.transform.position, transform.position);
            if (distance <= localBoidsDistance)
            {
                positionSum += (Vector2)boid.transform.position;
                count++;
            }
        }
        if (count == 0) return;

        //get average position of boids
        Vector2 positionAverage = positionSum / count;
        positionAverage = positionAverage.normalized;
        Vector2 faceDirection = (positionAverage - (Vector2)transform.position).normalized;

        //move boid toward center
        float deltaTimeStrength = moveToCenterStrength * Time.deltaTime;
        direction = direction + deltaTimeStrength * faceDirection / (deltaTimeStrength + 1);
        direction = direction.normalized;
    }

    void AvoidOtherBoids()
    {

        Vector2 faceAwayDirection = Vector2.zero;//this is a vector that will hold direction away from near boid so we can steer to it to avoid the collision.

        foreach (Boid boid in spawner.boids)
        {
            float distance = Vector2.Distance(boid.transform.position, transform.position);

            //if the distance is within range calculate away vector from it and subtract from away direction.
            if (distance <= collisionAvoidCheckDistance)
            {
                faceAwayDirection = faceAwayDirection + (Vector2)(transform.position - boid.transform.position);
            }
        }

        faceAwayDirection = faceAwayDirection.normalized;//we need to normalize it so we are only getting direction

        direction = direction + avoidOtherStrength * faceAwayDirection / (avoidOtherStrength + 1);
        direction = direction.normalized;
    }

    void AlignWithOthers()
    {
        //we will need to find average direction of all nearby boids
        Vector2 directionSum = Vector3.zero;

        int count = 0;

        foreach (var boid in spawner.boids)
        {
            float distance = Vector2.Distance(boid.transform.position, transform.position);
            if (distance <= localBoidsAlignmentCheckDistance)
            {
                directionSum += boid.direction;
                count++;
            }
        }

        Vector2 directionAverage = directionSum / count;
        directionAverage = directionAverage.normalized;

        //now add this direction to direction vector to steer towards it
        float deltaTimeStrength = _matchingFactor * Time.deltaTime;
        direction = direction + deltaTimeStrength * directionAverage / (deltaTimeStrength + 1);
        direction = direction.normalized;

    }
    private void StayInsideWalls()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        float distance = 0.1f;
        if (pos.x < distance)
            direction = new Vector2(direction.x + avoidWallsStrength * Time.deltaTime, direction.y);
        if (1 - pos.x < distance) 
            direction = new Vector2(direction.x - avoidWallsStrength * Time.deltaTime, direction.y);
        if (pos.y < distance) 
            direction = new Vector2(direction.x, direction.y + avoidWallsStrength * Time.deltaTime);
        if (1 - pos.y < distance) 
            direction = new Vector2(direction.x, direction.y - avoidWallsStrength * Time.deltaTime);
        direction = direction.normalized;
    }
}
