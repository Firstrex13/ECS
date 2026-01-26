using Leopotam.Ecs;
using UnityEngine;

public class PlayerInputSystem : IEcsRunSystem
{
    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";

    private readonly EcsFilter<InputEventComponent> _inputEventFilter;
    public void Run()
    {
        var horizontal = Input.GetAxisRaw(HorizontalAxis);
        var vertical = Input.GetAxisRaw(VerticalAxis);

        foreach (var entity in _inputEventFilter)
        {
            ref var inputEvent = ref _inputEventFilter.Get1(entity);
            inputEvent.Direction = new Vector3(horizontal, 0, vertical);
        }
    }
}
