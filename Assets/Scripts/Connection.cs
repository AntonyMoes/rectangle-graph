using UnityEngine;

public class Connection : MonoBehaviour {
    Rectangle _target1;
    Rectangle _target2;
    
    public void Setup(Rectangle target1, Rectangle target2) {
        _target1 = target1;
        _target2 = target2;
    }
    
    public void UpdatePosition() {
        var pos1 = _target1.transform.position;
        var pos2 = _target2.transform.position;
        var distanceVector = pos2 - pos1;
        
        transform.position = (pos1 + pos2) / 2;
        transform.right = distanceVector.normalized;
        
        var scale = transform.localScale;
        scale.x = distanceVector.magnitude;
        transform.localScale = scale;
    }
}
