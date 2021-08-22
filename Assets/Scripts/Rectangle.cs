using System.Collections.Generic;
using UnityEngine;

public class Rectangle : MonoBehaviour {
    public readonly HashSet<Connection> Connections = new HashSet<Connection>();

    Vector3 _lastPosition;

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
