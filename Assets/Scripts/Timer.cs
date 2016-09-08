using UnityEngine;
using System.Collections;
using System;

public class Timer{
	private float timer; // In seconds
	private Action toBeCalled = null;  // See Set(seconds, toBeCalled);
	public Timer() {
	/* Initialize the timer with the finished state.*/
		timer = 0;
	}

	public Timer(float seconds) {
		/* Initialize the timer with the specified amount of time. */
		Set(seconds);
	}

	public bool Tick() {
		/*
		 * Tick the timer down. Call this function in the update loop.
		 * Returns true if the timer finished. Otherwise returns false.
		 */
		timer -= Time.deltaTime;
		if (timer <= 0) {
			timer = 0;
			// If there is a function to be called, call it and remove it.
			if (toBeCalled != null) {
				Action temp = toBeCalled;  // Temp is to prevent if the action set another action, this code won't remove that one.
				toBeCalled = null;
				temp();
			}
			return true;
		}
		return false;
	}

	public void Set(float seconds) {
		timer = seconds;
	}

	public void Set(float seconds, Action toBeCalled) {
		/* Set the timer. After the timer finishes, call the specified function. */
		timer = seconds;
		this.toBeCalled = toBeCalled;
	}

	public float GetTime() {
		/* Get the remaining time on the timer. */
		return timer;
	}

	public bool IsReady() {
		/* Get the state of the timer, with true being done and false being still ticking. */
		return timer <= 0;
	}
}
