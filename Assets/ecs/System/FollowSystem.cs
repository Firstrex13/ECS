using Leopotam.Ecs;
using UnityEngine;

public class FollowSystem : IEcsRunSystem
{
    private readonly EcsFilter<FollowComponent, MovableComponent> _enemyFollowFilter;
    private readonly float _stopDistance = 2f;

    public void Run()
    {
        foreach (var entity in _enemyFollowFilter)
        {
            ref var followComponent = ref _enemyFollowFilter.Get1(entity);
            ref var movableComponent = ref _enemyFollowFilter.Get2(entity);

            if (followComponent.target == null)
            {
                continue;
            }

            var direction = (followComponent.target.position - movableComponent.transform.position).normalized;
            var distance = Vector3.Distance(followComponent.target.position, movableComponent.transform.position);
            var isMoving = distance > _stopDistance;

            if (isMoving)
            {
                movableComponent.transform.position += direction * (Time.deltaTime * movableComponent.moveSpeed);

                movableComponent.transform.forward = direction;
            }

            movableComponent.isMoving = isMoving;
            direction.y = 0;
        }
    }
}
