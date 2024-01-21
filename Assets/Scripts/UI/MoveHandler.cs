using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoveHandler : MonoBehaviour
{
    public TextMeshProUGUI text;
    public String movesLeft;
    public int moves;

    public void Start() {
        text.text = movesLeft + " " + moves.ToString();
    }

    public bool isMoveAllowed() {
        return moves > 0;
    }

    public void MoveMade() {
        moves --;
        text.text = movesLeft + " " + moves.ToString();
    }
}
