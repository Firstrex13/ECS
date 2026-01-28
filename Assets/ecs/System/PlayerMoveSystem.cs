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
            movableComponent.transform.position += inputComponent.direction * (Time.deltaTime * movableComponent.moveSpeed);

            if (movableComponent.isMoving = inputComponent.direction.sqrMagnitude > 0)
            {
                movableComponent.transform.forward = inputComponent.direction;
            }
        }
    }
}
