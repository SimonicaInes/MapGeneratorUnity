using UnityEngine;
using UnityEngine.Tilemaps;


public class ExtraTile : ScriptableObject {

    public int terrainType;
    public float damage;
    public float speed;
    public Tile tile;

    public void Init(int terrainType, float damage, float speed, Tile tile)
    {
        this.terrainType = terrainType;
        this.damage = damage;
        this.speed = speed;
        this.tile = tile;
    }



}