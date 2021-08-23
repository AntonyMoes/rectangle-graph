using UnityEngine;

public static class Extensions {
    public static Color WithAlpha(this Color color, float alpha) {
        var newColor = color;
        newColor.a = alpha;
        return newColor;
    }
}
