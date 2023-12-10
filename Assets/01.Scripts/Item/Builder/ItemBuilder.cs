public class ItemBuilder
{
    private readonly BuildArea _buildArea;

    private Item _generatedItem;

    public ItemBuilder(BuildArea buildArea)
    {
        _buildArea = buildArea;
    }
    
    public void SpawnItem()
    {
        RemoveItem();
        _generatedItem = PoolManager.Instance.Pop("Item") as Item;
        _generatedItem.transform.position = _buildArea.GetAreaPosition();
    }

    public void RemoveItem()
    {
        if (_generatedItem is not null)
        {
            PoolManager.Instance.Push(_generatedItem);
        }
    }
}