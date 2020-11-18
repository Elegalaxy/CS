using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEdge : MonoBehaviour
{
    private void OnTriggerExit(Collider other) {
        if(other.tag == "Bullet") other.gameObject.GetComponent<Bullet>().destroy(); //If bullet go out of map destroy it
        else if(other.tag == "Player") other.gameObject.GetComponent<Health>().takeDamage(50f); //If player go out of map damage them
    }
}
