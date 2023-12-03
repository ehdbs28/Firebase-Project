using UnityEngine;

public class SnakeMovementModule : BaseModule<SnakeController>
{
    public SnakeMovementModule(SnakeController controller) : base(controller)
    {
    }

    public override void UpdateModule()
    {
        if (Controller.IsHead)
        {
            Controller.transform.Translate(Controller.transform.up * (Controller.Data.movementSpeed * Time.deltaTime), Space.World); ;
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