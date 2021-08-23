using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rectangle {
    public static readonly Vector2 Size = new Vector2(2, 1);
    
    readonly Pool _rectanglePool;
    readonly ObjectAdapter _adapter;
    public readonly HashSet<Connection> Connections = new HashSet<Connection>();

    public Vector2 Position {
        get => _adapter.Position;
        set {
            _adapter.Position = value;
            foreach (var connection in Connections) {
                connection.UpdatePosition();
            }
        }
    }

    public Color Color {
        get => _adapter.Color;
        set => _adapter.Color = value;
    }

    public Rectangle(Pool rectanglePool) {
        _rectanglePool = rectanglePool;
        _adapter = rectanglePool.Get<ObjectAdapter>();

        _adapter.UsedBy = this;
        _adapter.Size = Size;
        _adapter.Color = Random.ColorHSV();
    }

    public void CleanUp() {
        _adapter.UsedBy = null;
        _rectanglePool.Release(_adapter.gameObject);
    }
}
