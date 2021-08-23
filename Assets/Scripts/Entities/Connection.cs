using UnityEngine;
using Color = UnityEngine.Color;

public class Connection {
    readonly Pool _connectionPool;
    readonly ObjectAdapter _adapter;
    public Rectangle Target1 { get; }
    public Rectangle Target2 { get; }

    Vector2 Position {
        get => _adapter.Position;
        set => _adapter.Position = value;
    }

    public Color Color {
        get => _adapter.Color;
        set => _adapter.Color = value;
    }
    
    Vector2 Right {
        get => _adapter.Right;
        set => _adapter.Right = value;
    }

    Vector2 Size {
        get => _adapter.Size;
        set => _adapter.Size = value;
    }
    
    public Connection(Pool connectionPool, Rectangle target1, Rectangle target2) {
        Target1 = target1;
        Target2 = target2;
        
        _connectionPool = connectionPool;
        _adapter = connectionPool.Get<ObjectAdapter>();

        _adapter.UsedBy = this;
        Color = Color.Lerp(target1.Color, target2.Color, 0.5f);
        
        UpdatePosition();
    }
    
    public void UpdatePosition() {
        var pos1 = Target1.Position;
        var pos2 = Target2.Position;
        var distanceVector = pos2 - pos1;
        
        Position = (pos1 + pos2) / 2;
        Right = distanceVector.normalized;
        Size = new Vector2(distanceVector.magnitude, Size.y);
    }

    public void CleanUp() {
        _adapter.UsedBy = null;
        _connectionPool.Release(_adapter.gameObject);
    }
}
