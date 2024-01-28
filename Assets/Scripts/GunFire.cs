using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{
    [SerializeField] ParticleSystem sys;

    List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    void OnParticleCollision(GameObject other) {
        IShootable iShootable = other.GetComponent<IShootable>();
        if(iShootable!=null) {
            int numCollisions = sys.GetCollisionEvents(other, collisionEvents);

            for(int i=0; i<numCollisions; i++) {
                iShootable.BeingShot();
            }
        
        
        }
    }
    
}