using Entities;
using UnityEngine;
using Utils;

public class EnvironmentProbe {
    readonly LayerMask _rectangleMask;
    readonly Collider2D[] _overlapped = new Collider2D[2];
    readonly RaycastHit2D[] _hits = new RaycastHit2D[2];

    public EnvironmentProbe(LayerMask rectangleMask) {
        _rectangleMask = rectangleMask;
    }
    
    public bool CanFitRectangle(Vector2 position, Vector2 size, Rectangle rectangleToIgnore = null) {
        var hitCount = Physics2D.BoxCastNonAlloc(position, size, 0, Vector2.zero,  _hits, 0, _rectangleMask);
        if (hitCount == 0) {
            return true;
        }

        if (rectangleToIgnore != null && hitCount == 1 && _hits[0].transform.GetComponent<ObjectAdapter>().UsedBy == rectangleToIgnore) {
            return true;
        }

        return false;
    }

    public Rectangle GetRectangleBesidesPassed(Vector2 position, Rectangle passed) {
        var overlappedCount = Physics2D.OverlapPointNonAlloc(position, _overlapped, _rectangleMask);
        for (var i = 0; i < overlappedCount; i++) {
            if (!_overlapped[i].TryGetComponent(out ObjectAdapter adapter) ||
                !(adapter.UsedBy is Rectangle rectangle)) {
                continue;
            }

            if (rectangle != passed) {
                return rectangle;
            }
        }

        return null;
    }

    /**
     * This method returns a rectangle only if no connections were overlapped
     * (because connections are rendered over rectangles)
     */
    public GameObject GetTarget(Vector2 position) {
        var overlappedCount = Physics2D.OverlapPointNonAlloc(position, _overlapped);
        if (overlappedCount == 0) {
            return null;
        }

        GameObject rectangle = null;
        for (var i = 0; i < overlappedCount; i++) {
            if (!_overlapped[i].TryGetComponent(out ObjectAdapter adapter)) {
                continue;
            }

            if (adapter.UsedBy is Connection) {
                return _overlapped[i].gameObject;
            }

            rectangle = _overlapped[i].gameObject;
        } 

        return rectangle;
    }
}
