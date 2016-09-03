using UnityEngine;
using System.Collections;

public abstract class PersistentProjectile: MonoBehaviour {
    /* 
     * This script applies to every persistent projectiles and values will be
     *  set when the projectile is created by their respective spell scripts.
     *  
     *  The projectile will be destroyed by the respective spell script.
     */

    protected virtual float damage { get; set; }  // Damage per second.

    protected virtual IEnumerator OnTriggerStay2D(Collider2D col) {
        Debug.Log("TRIGGERED");
        if (col.tag == "Enemy") {
            Enemy enemyScript = col.GetComponent<Enemy>();
            enemyScript.health -= damage/60f;
        }
        yield return new WaitForSeconds((1f / 60f));  // Ticks every so often.
    }
}
