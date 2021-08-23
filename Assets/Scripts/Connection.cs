using UnityEngine;

public class Connection : MonoBehaviour {
    public Rectangle target1;
    public Rectangle target2;
    
    public void Setup(Rectangle target1, Rectangle target2) {
        this.target1 = target1;
        this.target2 = target2;
        GetComponent<SpriteRenderer>().color = Color.Lerp(target1.GetComponent<SpriteRenderer>().color,
            target2.GetComponent<SpriteRenderer>().color, 0.5f);
        
        UpdatePosition();
    }
    
    public void UpdatePosition() {
        var pos1 = target1.transform.position;
        var pos2 = target2.transform.position;
        var distanceVector = pos2 - pos1;
        
        transform.position = (pos1 + pos2) / 2;
        transform.right = distanceVector.normalized;
        
        var scale = transform.localScale;
        scale.x = distanceVector.magnitude;
        transform.localScale = scale;
    }
}
