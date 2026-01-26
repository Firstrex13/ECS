using Leopotam.Ecs;
using UnityEngine;

public class PlayerMoveSystem : IEcsRunSystem
{
    private readonly EcsFilter<MovableComponent, InputEventComponent> _playerMoveFilter;
    public void Run()
    {
        foreach (var entity in _playerMoveFilter)
        {
            ref var inputComponent = ref _playerMoveFilter.Get2(entity);

            ref var movableComponent = ref _playerMoveFilter.Get1(entity);
            movableComponent.Transform.position += inputComponent.Direction * (Time.deltaTime * movableComponent.MoveSpeed);
            movableComponent.Transform.forward = inputComponent.Direction;
            movableComponent.IsMoving = inputComponent.Direction.sqrMagnitude > 0;
        }
    }
}
