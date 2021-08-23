using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rectangle : MonoBehaviour {
    public readonly HashSet<Connection> Connections = new HashSet<Connection>();

    public static readonly Vector2 Size = new Vector2(2, 1);

    public Vector2 Position {
        get => transform.position;
        set {
            transform.position = new Vector3(value.x, value.y, transform.position.z);
            foreach (var connection in Connections) {
                connection.UpdatePosition();
            }
        }
    }

    void Start() {
        transform.localScale = new Vector3(Size.x, Size.y, transform.localScale.z);
    }

    void OnEnable() {
        GetComponent<SpriteRenderer>().color = Random.ColorHSV();
    }
}
