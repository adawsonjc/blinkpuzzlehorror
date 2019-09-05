using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPlayerSighting : MonoBehaviour {

	public Vector2 lastSighting;
    public Vector2 reset = new Vector2(0, 0);

    public Vector2 resetPosition()
    {
        this.lastSighting = reset;
        return this.lastSighting;
    }
    public Vector2 getPosition()
    {
        return this.lastSighting;
    }
    public void setPosition(Vector2 p)
    {
        this.lastSighting = p;
    }

}
