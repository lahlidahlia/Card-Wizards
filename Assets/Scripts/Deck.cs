using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Deck{
    private List<int> deck;
	private List<int> discard;  // Keep track of used spells to reshuffle it.

    public Deck(List<int> deck) {
        this.deck = deck;
		discard = new List<int>();
    }

    public void Shuffle() {
		/*
		 * Fisher-Yates shuffle.
		 * https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle#The_modern_algorithm
		 */
        for (int i = deck.Count-1; i > 0; i--) {
            int r = Random.Range(0, i+1);

            int n = deck[r];
            deck[r] = deck[i];
            deck[i] = n;
        }
    }

    public int Draw() {
	   /* Removes the last number from the deck. */
		int ret = deck[deck.Count - 1];
		deck.RemoveAt(deck.Count - 1);
		if (deck.Count == 0) {  // If out of cards, reshuffle cards from the discard pile.
			deck.AddRange(discard);
			Shuffle();
			discard.Clear();
			Debug.Log("RESHUFFLING");
		}

		return ret;
    }

	public void AddToDiscard(int card) {
		/* Add a card to the discard pile for future use. */
		discard.Add(card);
	}

	public Spell GetSpell(int index) {
		/* Returns the spell card at the given index.*/
		return GM.spellDictionary[deck[index]];
	}

	public override string ToString() {
		/* Returns a string that represents the cards in deck. */
		string ret = "";
        foreach(int i in deck) {
            ret += GM.spellDictionary[i].name + ", ";
        }
        return ret;
    }

	public void AddToTop(int card) {
		deck.Add(card);
	}
	public void AddToBottom(int card) {
		deck.Insert(0, card);
	}
}
