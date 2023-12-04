using Unity.VisualScripting;

public class SnakeHealthModule : BaseModule<SnakeController>
{
    private float _currentHealth;
    
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

    public void OnDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0f)
        {
            Controller.Detach(Controller.Index);
        }
    }
}