using System;
using System.Collections.Generic;

public class SnakeController : ModuleController
{
    private List<SnakeSegment> _segments;
    public List<SnakeSegment> Segments => _segments;

    private void Awake()
    {
        _segments = new List<SnakeSegment>();
        
        AddModule(new SnakeMovementModule(this));
    }
}