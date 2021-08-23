using System.Linq;
using UnityEngine;

namespace Commands {
    public class DragRectangleCommand : ICommand {
        readonly Rectangle _rectangle;
        readonly SpriteRenderer _renderer;
        readonly Vector2 _startPosition;
        readonly Pool _connectionPool;
        readonly EnvironmentProbe _environmentProbe;

        const float SeeThroughAlpha = 0.3f;

        Vector2 _dragDelta;
        bool _hasMoved;

        public DragRectangleCommand(Rectangle rectangle, Pool connectionPool, EnvironmentProbe environmentProbe) {
            _rectangle = rectangle;
            _renderer = _rectangle.GetComponent<SpriteRenderer>();
            _startPosition = rectangle.Position;
            _connectionPool = connectionPool;
            _environmentProbe = environmentProbe;
        }
        
        public void OnButtonDown(Vector2 position) {
            _dragDelta = position - _startPosition;
        }

        public void OnButtonUp(Vector2 position) {
            _renderer.color = _renderer.color.WithAlpha(1);
            foreach (var connection in _rectangle.Connections) {
                var connectionRenderer = connection.GetComponent<SpriteRenderer>();
                connectionRenderer.color = connectionRenderer.color.WithAlpha(1);
            }

            if (_environmentProbe.CanFitRectangle(_rectangle.transform.position, Rectangle.Size, _rectangle)) {
               return; 
            }

            if (_environmentProbe.GetRectangleBesidesPassed(position, _rectangle) is Rectangle other) {
                var noSuchConnection = _rectangle.Connections.All(conn => conn.target1 != other && conn.target2 != other);
                if (noSuchConnection) {
                    var connection = _connectionPool.Get().GetComponent<Connection>();
                    connection.Setup(_rectangle, other);
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
                
            _renderer.color = _renderer.color.WithAlpha(SeeThroughAlpha);
            foreach (var connection in _rectangle.Connections) {
                var connectionRenderer = connection.GetComponent<SpriteRenderer>();
                connectionRenderer.color = connectionRenderer.color.WithAlpha(SeeThroughAlpha);
            }
        }
    }
}
