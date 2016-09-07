using UnityEngine;
using System.Collections;

public class Timer{
	private float timer; // In seconds

	public Timer() {
	/* Initialize the timer with the finished state.*/
		timer = 0;
	}

	public Timer(float seconds) {
	/* Initialize the timer with the specified amount of time. */
		timer = seconds;
	}

	public bool Tick() {
		/*
		 * Tick the timer down. Call this function in the update loop.
		 * Returns true if the timer finished. Otherwise returns false.
		 */
		timer -= Time.deltaTime;
		if (timer <= 0) {
			timer = 0;
			return true;
		}
		return false;
	}

	public void Set(float seconds) {
		timer = seconds;
	}

	public float GetTime() {
		/* Get the remaining time on the timer. */
		return timer;
	}

	public bool GetState() {
		/* Get the state of the timer, with true being done and false being still ticking. */
		return timer <= 0;
	}
}
