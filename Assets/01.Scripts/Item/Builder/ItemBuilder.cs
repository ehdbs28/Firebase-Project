public class ItemBuilder
{
    private readonly BuildArea _buildArea;

    public ItemBuilder(BuildArea buildArea)
    {
        _buildArea = buildArea;
    }
    
    public void SpawnItem()
    {
        var item = PoolManager.Instance.Pop("Item") as Item;
        item.transform.position = _buildArea.GetAreaPosition();
    }
}