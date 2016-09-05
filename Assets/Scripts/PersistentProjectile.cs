using UnityEngine;
using System.Collections;

public abstract class PersistentProjectile: MonoBehaviour {
    /* 
     * This script applies to most persistent projectiles and values will be
     *  set when the projectile is created by their respective spell scripts.
     *  
     * The key difference between a persistent projectile and a normal projectile
     *  is that persistent projectiles won't be destroyed when collided with an enemy.
     *  
     * The projectile will be destroyed by the respective spell script.
     */

    protected virtual float damage { get; set; }  // Damage per second.

    protected virtual IEnumerator OnTriggerStay2D(Collider2D col) {
        if (col.tag == "Enemy") {
            Enemy enemyScript = col.GetComponent<Enemy>();
            enemyScript.health -= damage/60f;
        }
        yield return new WaitForSeconds((1f / 60f));  // Ticks every so often.
    }
}
