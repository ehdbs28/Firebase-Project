using Unity.VisualScripting;
using UnityEngine;

public class SnakeMovementModule : BaseModule<SnakeController>
{
    private readonly Rigidbody2D _rigidbody;
    
    public SnakeMovementModule(SnakeController controller) : base(controller)
    {
        if (controller.IsHead)
        {
            _rigidbody = controller.GetComponent<Rigidbody2D>();
        }
    }

    public override void UpdateModule()
    {
        if (Controller.IsHead)
        {
            _rigidbody.velocity = Controller.transform.up * Controller.Data.movementSpeed;
            Controller.PositionHistory.Insert(0, Controller.transform.position);
            if (Controller.PositionHistory.Count > Controller.Data.segmentOffset * Controller.PartsCnt)
            {
                Controller.PositionHistory.RemoveAt(Controller.PositionHistory.Count - 1);
            }
        }
        else
        {
            Controller.transform.position =
                Controller.Head.PositionHistory[Mathf.Min(Controller.Index * Controller.Data.segmentOffset, Controller.Head.PositionHistory.Count - 1)];
        }
    }

    public override void FixedUpdateModule()
    {
    }
}