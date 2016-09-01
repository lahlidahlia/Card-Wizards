using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {
    /* 
     * This script applies to every single projectiles and values will be
     *  set when the projectile is created by their respective spell scripts.
     */

    [System.NonSerialized]
    public float damage;

    void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Enemy") {
            Enemy enemyScript = col.GetComponent<Enemy>();
            enemyScript.health -= damage;
            Destroy(gameObject);
        }
    }
}
