using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    private ArenaController _arenaController;

    private Vector3 _playerReadyPos;
    private Quaternion _playerReadyRot;
    public bool PlayerReady { get; private set; }

    private Vector3 _playerWaveTransitionInitialPos;
    private Vector3 _playerWaveTransitionTargetPos;
    private Quaternion _playerWaveTransitionInitialRot;
    private Quaternion _playerWaveTransitionTargetRot;

    private Rigidbody rb;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _arenaController = ArenaController.Instance;

        float playerReadyYPos = (_arenaController.gameObject.GetComponentInChildren<Collider>().bounds.extents.y + GetComponentInChildren<Collider>().bounds.extents.y);
        _playerReadyPos = _arenaController.gameObject.transform.position + playerReadyYPos * 1.0f * Vector3.up;
        _playerWaveTransitionTargetPos = _playerReadyPos + (playerReadyYPos * 2.5f) * Vector3.up;
        transform.position = _playerWaveTransitionTargetPos;

        _playerReadyRot = Quaternion.identity;
        _playerWaveTransitionTargetRot = _playerReadyRot * Quaternion.Euler(new Vector3(90, 0, 0));
        transform.rotation = _playerWaveTransitionTargetRot;

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_arenaController.LoadingNewWave)
        {
            PlayerWaveTransition();
        }
        else if (!PlayerReady)
        {
            PlayerReturningToReadyPos();
        }
        else
        {
            _playerWaveTransitionInitialPos = transform.position;
            _playerWaveTransitionInitialRot = transform.rotation;
        }
    }

    private void PlayerWaveTransition()
    {
        PlayerReady = false;

        rb.useGravity = false;
        rb.isKinematic = true;

        float lerp = _arenaController.ArenaRotationLerp * 3;
        transform.position = Vector3.Lerp(_playerWaveTransitionInitialPos, _playerWaveTransitionTargetPos, lerp);
        transform.rotation = Quaternion.Lerp(_playerWaveTransitionInitialRot, _playerWaveTransitionTargetRot, lerp);
    }

    private void PlayerReturningToReadyPos()
    {
        rb.useGravity = true;
        rb.isKinematic = false;

        float lerp = Mathf.InverseLerp(_playerWaveTransitionTargetPos.y, _playerReadyPos.y, transform.position.y);
        transform.position = new Vector3(Mathf.Lerp(_playerWaveTransitionTargetPos.x, _playerReadyPos.x, lerp), transform.position.y, Mathf.Lerp(_playerWaveTransitionTargetPos.z, _playerReadyPos.z, lerp));
        transform.rotation = Quaternion.Lerp(_playerWaveTransitionTargetRot, Quaternion.Euler(0, _playerReadyRot.eulerAngles.y, 0), lerp);

        PlayerReady = lerp >= 1;
    }
}
