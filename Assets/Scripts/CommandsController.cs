using Commands;
using Entities;
using UnityEngine;
using Utils;

public class CommandsController {
    readonly Pool _rectanglePool;
    readonly Pool _connectionPool;
    readonly EnvironmentProbe _environmentProbe;

    const float DoubleClickTime = 0.4f;
    const float DoubleClickPositionDelta = 0.1f;
    
    float _timeSinceLastClick = DoubleClickTime;
    Vector2 _lastClickPosition;
    
    ICommand _currentCommand;

    public CommandsController(Pool rectanglePool, Pool connectionPool, EnvironmentProbe environmentProbe) {
        _rectanglePool = rectanglePool;
        _connectionPool = connectionPool;
        _environmentProbe = environmentProbe;
    }

    public void OnButtonDown(Vector2 position, Either<Rectangle, Connection> target) {
        if (target == null) {
            _currentCommand = new CreateRectangleCommand(_rectanglePool, _environmentProbe);
        } else {
            if (_timeSinceLastClick < DoubleClickTime &&
                (position - _lastClickPosition).magnitude < DoubleClickPositionDelta) {
                _currentCommand = new DeleteCommand(target);
                _timeSinceLastClick = DoubleClickTime;
            } else {
                target.Get(
                    rectangle => {
                        _currentCommand = new DragRectangleCommand(rectangle, _connectionPool, _environmentProbe);
                    },
                    _ => { });
                _timeSinceLastClick = 0;
            }
        }

        _lastClickPosition = position;
        _currentCommand?.OnButtonDown(position);
    }

    public void OnDrag(Vector2 position) {
        _currentCommand?.OnDrag(position);
    }

    public void OnButtonUp(Vector2 position) {
        _currentCommand?.OnButtonUp(position);
        _currentCommand = null;
    }

    public void UpdateTime(float deltaTime) {
        _timeSinceLastClick = Mathf.Min(DoubleClickTime, _timeSinceLastClick + deltaTime);
    }
}
