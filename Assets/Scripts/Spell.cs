using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Spell{
    public abstract float cooldown { get; }  // Time in seconds
	public virtual float castTime { get { return 0; } }  // Time in seconds
	public virtual float channelTime { get { return 0; } }  // How long can the spell be channeled for.
    public int ID { get; set; }

	protected Timer channelTimer = new Timer();

    public abstract void effect(GameObject player);

    protected GM gm;
    protected List<GameObject> assets = new List<GameObject>();  // Contains 

    public Spell(GM gm) {
        this.gm = gm;
	}

    protected void getAsset(string name) {
		/*
		 *  Get assets from the associated gameObject's child.
		 */
        GameObject obj = GameObject.Find(name);
        foreach (Transform child in obj.transform) {
            assets.Add(child.gameObject);
        }
    }

	protected Coroutine channel(Player playerScr, GameObject projectile) {
		/* Encapsulator function, returns the coroutine*/
		return gm.RunCoroutine(_channel(playerScr, projectile));
	}
	protected virtual IEnumerator _channel(Player playerScr, GameObject projectile) {
		/* 
		 *  Channel the spell, putting the player in the state of channeling.
		 *   Channeling ends when the player release the channel button, or the timer finish.
		 *	Do not run this method, but run channel(...) instead.
		 */
		channelTimer.Set(channelTime);
		playerScr.currentState = PlayerState.CHANNEL;
		while (playerScr.currentState == PlayerState.CHANNEL && !channelTimer.IsDone()) {
			channelTimer.Tick();
			yield return null;
		}
		if (playerScr.currentState == PlayerState.CHANNEL) {  // To prevent the case when the player release button and currentState already went back to free and then set back again.
			playerScr.currentState = PlayerState.FINISH;
		}
		Object.Destroy(projectile);
	}
}
