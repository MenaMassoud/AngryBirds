using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monsters : MonoBehaviour
{
    //use that to start a cloud
    [SerializeField] private GameObject _cloudParticlePrefab;

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        //if the monsters was hit by a bird
        Birds bird = collision.collider.GetComponent<Birds>();
        if (bird != null) {
            //this alone will spawn the particle cloud in the default location.(use overload)
            //give it the current location and default rotation
            Instantiate(_cloudParticlePrefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
        //if it was hit by a box
        Monsters monster = collision.collider.GetComponent<Monsters>();
        if (monster != null) {
            return;
        }

        //contacts is many hits .. we only care about the first.
        //normal gives the angle of the impact
        if (collision.contacts[0].normal.y < 0.5) {
            Instantiate(_cloudParticlePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }


}
