using UnityEngine;

namespace Commands {
    public class CreateRectangleCommand : ICommand {
        readonly Pool _rectanglePool;
        readonly EnvironmentProbe _environmentProbe;

        public CreateRectangleCommand(Pool rectanglePool, EnvironmentProbe environmentProbe) {
            _rectanglePool = rectanglePool;
            _environmentProbe = environmentProbe;
        }
        
        public void OnButtonDown(Vector2 position) {
            if (!_environmentProbe.CanFitRectangle(position, Rectangle.Size)) {
                return;
            }

            var newRectangle = _rectanglePool.Get(); 
            newRectangle.transform.position = position;
        }

        public void OnButtonUp(Vector2 position) { }
        public void OnDrag(Vector2 position) { }
    }
}
