using UnityEngine;

namespace Commands {
    public interface ICommand {
        void OnButtonDown(Vector2 position);
        void OnButtonUp();
        void OnDrag(Vector2 position);
    }
}
