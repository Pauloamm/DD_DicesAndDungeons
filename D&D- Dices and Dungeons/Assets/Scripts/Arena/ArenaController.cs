using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Material))]
public class ArenaController : MonoBehaviour
{
    public static ArenaController Instance { get; private set; }
    private PlayerController _playerController;

    [SerializeField]
    private int _currentArenaLevel;

    [SerializeField]
    private int _currentWave;

    private Vector3[] _wavesRotations;

    [SerializeField]
    private float _waveTransitionSpeed;

    public bool LoadingNewWave { get; private set; }
    private float _initialArenaAngle;
    public float ArenaRotationLerp { get; private set; }

    [SerializeField]
    private Material[] _arenaLevelsMaterials;

    [SerializeField]
    private Renderer _arenaRenderer;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _playerController = PlayerController.Instance;

        _wavesRotations = new Vector3[6];
        _wavesRotations[0] = new Vector3(0, 0, 0);
        _wavesRotations[1] = new Vector3(0, 0, 90);
        _wavesRotations[2] = new Vector3(-90, 0, 0);
        _wavesRotations[3] = new Vector3(90, 0, 0);
        _wavesRotations[4] = new Vector3(0, 0, -90);
        _wavesRotations[5] = new Vector3(0, 0, -180);

        _currentWave = _currentWave > 0 ? _currentWave : 1;
        _currentArenaLevel = _currentArenaLevel > 0 ? _currentArenaLevel : 1;

        transform.rotation = Quaternion.Euler(_wavesRotations[_currentWave - 1]);
        _arenaRenderer.material.color = _arenaLevelsMaterials[_currentArenaLevel - 1].color;

        _waveTransitionSpeed = _waveTransitionSpeed > 0 ? _waveTransitionSpeed : 25.0f;
    }

    private void Update()
    {
        if (LoadingNewWave)
        {
            RotateArena();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && _playerController.PlayerReady)
        {
            if (_currentWave < _wavesRotations.Length)
            {
                _currentWave++;
                LoadNextWave();
            }
            else if (_currentArenaLevel < _arenaLevelsMaterials.Length)
            {
                _currentWave = 1;
                LoadNextWave();
                LoadNextArenaLevel();
            }
            else
            {
                Debug.Log("WIN !!!");
            }
        }
    }

    private void LoadNextWave()
    {
        _initialArenaAngle = Quaternion.Angle(transform.rotation, Quaternion.Euler(_wavesRotations[_currentWave - 1]));
        LoadingNewWave = true;
    }

    private void RotateArena()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(_wavesRotations[_currentWave - 1]), Time.deltaTime * _waveTransitionSpeed);

        float currentArenaAngle = Quaternion.Angle(transform.rotation, Quaternion.Euler(_wavesRotations[_currentWave - 1]));
        LoadingNewWave = currentArenaAngle > 0;

        ArenaRotationLerp = Mathf.InverseLerp(Mathf.Abs(_initialArenaAngle), 0, currentArenaAngle);
    }

    private void LoadNextArenaLevel()
    {
        _currentArenaLevel++;

        _arenaRenderer.material.color = _arenaLevelsMaterials[_currentArenaLevel - 1].color;
    }
}
