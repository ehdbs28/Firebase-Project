using Unity.VisualScripting;

public class SnakeHealthModule : BaseModule<SnakeController>
{
    private int _currentHealth;
    
    public SnakeHealthModule(SnakeController controller) : base(controller)
    {
        _currentHealth = Controller.Data.maxHealth;
    }

    public override void UpdateModule()
    {
    }

    public override void FixedUpdateModule()
    {
    }

    public void OnDamage()
    {
        _currentHealth -= 1;
        if (_currentHealth <= 0)
        {
            Controller.Detach(Controller.Index);
        }
    }
}