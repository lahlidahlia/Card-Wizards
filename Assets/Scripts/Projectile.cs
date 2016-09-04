using UnityEngine;
using System.Collections;

public abstract class Projectile : MonoBehaviour {
    /* 
     * This script will be inherited by most non-persistent projectiles and values will be
     *  set when the projectile is created by their respective spell scripts.
     *  
     * Each spell class should have their own nested class that defines their projectile script and
     *  added to their projectile.
     */

    public virtual float damage { get; set; }

    protected virtual void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Enemy") {
            Enemy enemyScript = col.GetComponent<Enemy>();
            enemyScript.health -= damage;
            Destroy(gameObject);
        }
    }
}
