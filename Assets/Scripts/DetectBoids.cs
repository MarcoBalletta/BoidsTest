//using System.Collections.Generic;
//using UnityEngine;

//[RequireComponent(typeof(CircleCollider2D))]
//public class DetectBoids : MonoBehaviour
//{
//    [SerializeField] private TypeOfDetection detectionType;
//    private Boid boid;
//    private List<Boid> boidsDetected = new List<Boid>();

//    private void Awake()
//    {
//        boid = GetComponentInParent<Boid>();
//    }

//    private void OnTriggerStay2D(Collider2D other)
//    {
//        if(other.TryGetComponent(out Boid boidDetected))
//        {
//            if (!boidsDetected.Contains(boidDetected))
//            {
//                boidsDetected.Add(boidDetected);
//            }
//        }
//        boid.UpdateListDetection(ref boidsDetected, detectionType);
//    }

//    private void OnTriggerExit2D(Collider2D other)
//    {
//        if (other.TryGetComponent(out Boid boidDetected))
//        {
//            if (!boidsDetected.Contains(boidDetected))
//            {
//                boidsDetected.Remove(boidDetected);
//            }
//        }
//        boid.UpdateListDetection(ref boidsDetected, detectionType);
//    }


//}
