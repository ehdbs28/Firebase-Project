using UnityEngine;

public class SnakeMovementModule : BaseModule<SnakeController>
{
    public SnakeMovementModule(SnakeController controller) : base(controller)
    {
    }

    public override void UpdateModule()
    {
        Controller.transform.Translate(Controller.transform.up * (Controller.Data.movementSpeed * Time.deltaTime), Space.World); ;
    }

    public override void FixedUpdateModule()
    {
        
    }
}