using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpener : MonoBehaviour
{

    public GameObject Button;

    private GameObject _player;
    private GameObject _gate;
    private float _playerX;
    private float _playerY;
    private double _thisX;
    private double _thisY;
    private double _openY;
    private double _closeY;
    private bool _isOpen;

    // Use this for initialization
    void Start ()
	{
	    _player = GameObject.FindGameObjectWithTag("Player");
	    _gate = transform.gameObject;
	    _thisX = this.transform.position.x;
	    _thisY = this.transform.position.y;
	    _openY = _thisY + 0.85;
	    _closeY = _thisY;
        _isOpen = false;
        

    }

    // Update is called once per frame
    void Update ()
    {
        _playerX = _player.transform.position.x;
        _playerY = _player.transform.position.y;
        _thisX = this.transform.position.x;
        _thisY = this.transform.position.y - 0.05;
        if (_closeY - 1 < _playerY && _closeY + 1 > _playerY && _thisX - 0.5 < _playerX && _thisX + 0.5 > _playerX)
        {
            if (!_isOpen)
            {
                _gate.transform.Translate(Vector3.up * Time.deltaTime*_player.GetComponent<Player>().GetSpeed());
            }
        }
        else
        {
            if (_thisY >= _closeY)
            {
                _gate.transform.Translate(Vector3.down * Time.deltaTime);
            }
        }
        if (_thisY >= _openY)
        {
            _isOpen = true;
        }
        else
        {
            _isOpen = false;
        }
	}
}
