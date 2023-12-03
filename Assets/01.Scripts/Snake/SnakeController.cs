using System.Collections.Generic;
using UnityEngine;

public class SnakeController : ModuleController
{
    [SerializeField] private InputControl _inputControl;
    public InputControl InputControl => _inputControl;

    [SerializeField] private PlayerData _data;
    public PlayerData Data => _data;
    
    private List<SnakeSegment> _segments;
    public List<SnakeSegment> Segments => _segments;

    private void Awake()
    {
        _segments = new List<SnakeSegment>();
        
        AddModule(new SnakeMovementModule(this));
        AddModule(new SnakeRotateModule(this));
    }
}