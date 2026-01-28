using Leopotam.Ecs;
using UnityEngine;

public class GameInitSystem : IEcsInitSystem
{
    private readonly EcsWorld _ecsWorld;

    private readonly UnitInitData _playerInitData;
    private readonly UnitInitData _enemyInitData;
    private readonly Transform _spawnPoint;

    public GameInitSystem(UnitInitData playerInitData, UnitInitData enemyInitData, Transform spawnPoint)
    {
        _playerInitData = playerInitData;
        _enemyInitData = enemyInitData;
        _spawnPoint = spawnPoint;
    }

    public void Init()
    {
        var unitActor = Object.Instantiate(_playerInitData.UnitPrefab, _spawnPoint.position, Quaternion.identity);

        var player = _ecsWorld.NewEntity();
        player.Get<PlayerComponent>();
        player.Get<InputEventComponent>();

        ref var movableComponent = ref player.Get<MovableComponent>();
        movableComponent.moveSpeed = _playerInitData.DefaultSpeed;
        movableComponent.transform = unitActor.transform;

        ref var animationsComponent = ref player.Get<AnimatedCharacterComponent>();
        animationsComponent.animator = unitActor.Animator;

        var enemySpawnPosition = _spawnPoint.position + Vector3.one * Random.Range(-2f, 2f);
        enemySpawnPosition.y = 0;
        CreateEnemy(enemySpawnPosition, unitActor.transform);
    }

    private void CreateEnemy(Vector3 atPosition, Transform target)
    {
        var unitActor = Object.Instantiate(_enemyInitData.UnitPrefab, atPosition, Quaternion.identity);

        var enemy = _ecsWorld.NewEntity();

        ref var enemyMovableComponent = ref enemy.Get<MovableComponent>();
        enemyMovableComponent.moveSpeed = _enemyInitData.DefaultSpeed;
        enemyMovableComponent.transform = unitActor.transform;

        ref var enemyAnimationsComponent = ref enemy.Get<AnimatedCharacterComponent>();
        enemyAnimationsComponent.animator = unitActor.Animator;

        ref var followTarget = ref enemy.Get<FollowComponent>();
        followTarget.target = target;
    }
}
