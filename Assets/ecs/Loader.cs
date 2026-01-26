using Leopotam.Ecs;
using UnityEngine;

public class Loader : MonoBehaviour
{
    [SerializeField] private UnitInitData _playerData;
    [SerializeField] private UnitInitData _enemyData;
    [SerializeField] private Transform _spawnPoint;

    private EcsWorld _ecsWorld;
    private EcsSystems _ecsSystems;

    private void Start()
    {
        _ecsWorld = new EcsWorld();
        _ecsSystems = new EcsSystems(_ecsWorld);

        _ecsSystems.Add(new GameInitSystem(_playerData, _enemyData, _spawnPoint));
        _ecsSystems.Add(new PlayerInputSystem());
        _ecsSystems.Add(new PlayerMoveSystem());
        _ecsSystems.Add(new FollowSystem());
        _ecsSystems.Add(new AnimatedCharacterSystem());

        _ecsSystems.Init();
    }

    private void Update()
    {
        _ecsSystems?.Run();
    }

    private void OnDestroy()
    {
        _ecsSystems.Destroy();
        _ecsWorld.Destroy();
    }
}
