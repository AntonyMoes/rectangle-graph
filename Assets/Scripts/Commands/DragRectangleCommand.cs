using System.Linq;
using UnityEngine;

namespace Commands {
    public class DragRectangleCommand : ICommand {
        readonly Rectangle _rectangle;
        readonly Vector2 _startPosition;
        readonly Pool _connectionPool;
        readonly EnvironmentProbe _environmentProbe;

        const float SeeThroughAlpha = 0.3f;

        Vector2 _dragDelta;
        bool _hasMoved;

        public DragRectangleCommand(Rectangle rectangle, Pool connectionPool, EnvironmentProbe environmentProbe) {
            _rectangle = rectangle;
            _startPosition = rectangle.Position;
            _connectionPool = connectionPool;
            _environmentProbe = environmentProbe;
        }
        
        public void OnButtonDown(Vector2 position) {
            _dragDelta = position - _startPosition;
        }

        public void OnButtonUp(Vector2 position) {
            _rectangle.Color = _rectangle.Color.WithAlpha(1);
            foreach (var connection in _rectangle.Connections) {
                connection.Color = connection.Color.WithAlpha(1);
            }

            if (_environmentProbe.CanFitRectangle(_rectangle.Position, Rectangle.Size, _rectangle)) {
               return; 
            }

            if (_environmentProbe.GetRectangleBesidesPassed(position, _rectangle) is Rectangle other) {
                var noSuchConnection = _rectangle.Connections.All(conn => conn.Target1 != other && conn.Target2 != other);
                if (noSuchConnection) {
                    var connection = new Connection(_connectionPool, _rectangle, other);
                    _rectangle.Connections.Add(connection);
                    other.Connections.Add(connection);
                }
            }
            
            _rectangle.Position = _startPosition;
        }

        public void OnDrag(Vector2 position) {
            _rectangle.Position = position - _dragDelta;

            if (_hasMoved || _rectangle.Position == _startPosition) {
                return;
            }

            _hasMoved = true;
                
            _rectangle.Color = _rectangle.Color.WithAlpha(SeeThroughAlpha);
            foreach (var connection in _rectangle.Connections) {
                connection.Color = connection.Color.WithAlpha(SeeThroughAlpha);
            }
        }
    }
}
