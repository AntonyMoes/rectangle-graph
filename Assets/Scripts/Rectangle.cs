using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rectangle : MonoBehaviour {
    public readonly HashSet<Connection> Connections = new HashSet<Connection>();

    public static readonly Vector2 Size = new Vector2(2, 1);
    
    Vector3 _lastPosition;

    void Start() {
        transform.localScale = new Vector3(Size.x, Size.y, transform.localScale.z);
    }

    void OnEnable() {
        GetComponent<SpriteRenderer>().color = Random.ColorHSV();
    }

    void Update() {
        var newPosition = transform.position;
        if (newPosition != _lastPosition) {
            foreach (var connection in Connections) {
                connection.UpdatePosition();
            }
        }

        _lastPosition = newPosition;
    }
}
