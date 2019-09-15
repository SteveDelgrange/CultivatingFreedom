using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] protected GameObject _playerPrefab;
    [SerializeField] protected WorldBehaviour _world;

    protected Transform _playerTransform;

    private void Start()
    {
        _world.Initialize();
        InstantiatePlayer();
    }

    protected void InstantiatePlayer()
    {
        if(_playerTransform != null) {
            Destroy(_playerTransform.gameObject);
        }
        _playerTransform = Instantiate(_playerPrefab, _world.transform.position - _world.transform.forward * 2, _world.transform.rotation).transform;
    }
}
