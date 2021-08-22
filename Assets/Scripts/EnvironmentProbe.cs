
using UnityEngine;

public class EnvironmentProbe {
    readonly LayerMask _rectangleMask;

    public EnvironmentProbe(LayerMask rectangleMask) {
        _rectangleMask = rectangleMask;
    }
    
    public bool CanFitRectangle(Vector2 position, Vector2 size) {
        var hit = Physics2D.BoxCast(position, size, 0, Vector2.zero, 0, _rectangleMask);
        return !hit;
    }
}
