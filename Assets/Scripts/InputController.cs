using UnityEngine;

public class InputController : MonoBehaviour {
    [SerializeField] new Camera camera;
    [SerializeField] Pool rectanglePool; 
    [SerializeField] Pool connectionPool;
    [SerializeField] LayerMask rectangleMask;

    CommandsController _commandsController;

    void Start() {
        var environmentProbe = new EnvironmentProbe(rectangleMask);
        _commandsController = new CommandsController(rectanglePool, connectionPool, environmentProbe);
    }

    void Update() {
        _commandsController.UpdateTime(Time.deltaTime);
        
        var position = camera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0)) {
            var hit = Physics2D.GetRayIntersection(camera.ScreenPointToRay(Input.mousePosition)); // TODO: return rectangle if only no connections were hit
            _commandsController.OnButtonDown(position, hit.transform?.gameObject);
            return;
        }
        
        if (Input.GetMouseButtonUp(0)) {
            _commandsController.OnButtonUp();
            return;
        }

        if (Input.GetMouseButton(0)) {
            _commandsController.OnDrag(position);
        }
    }
}
