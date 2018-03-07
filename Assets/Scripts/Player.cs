using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int PlayerNumber = 0;
    public AnimatorController BaseAnimatorController;
    public CharacterClass Class;
    public Weapon EquipedWeapon;


    private float _speed;
    private bool _canMove;
    private Vector2 _movementDirection;
    private Rigidbody2D _rbody;
    private Animator _animator;


    // Use this for initialization
    void Start()
    {
        _canMove = true;
        _rbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _animator.runtimeAnimatorController = Class.RuntimeAnimatorController;
        UpdateWeapon();
    }

    private void UpdateWeapon()
    {
        if (EquipedWeapon == null)
        {
            return;
        }
        AnimatorOverrideController aoc = new AnimatorOverrideController(_animator.runtimeAnimatorController);
        var anims = new List<KeyValuePair<AnimationClip, AnimationClip>>();
        for (int i = 0; i < _animator.runtimeAnimatorController.animationClips.Length; i++)
        {
            string stateName = BaseAnimatorController.layers[0].stateMachine.states[i].state.name;
            if (stateName.Equals("attack_up"))
            {
                anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(aoc.animationClips[i], EquipedWeapon.Attack_Up));
            }
            else if (stateName.Equals("attack_down"))
            {
                anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(aoc.animationClips[i], EquipedWeapon.Attack_Down));
            }
            else if (stateName.Equals("attack_left"))
            {
                anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(aoc.animationClips[i], EquipedWeapon.Attack_Left));
            }
            else if (stateName.Equals("attack_right"))
            {
                anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(aoc.animationClips[i], EquipedWeapon.Attack_Right));
            }
        }
        aoc.ApplyOverrides(anims);
        _animator.runtimeAnimatorController = aoc;
      
    }

    void FixedUpdate()
    {
        _rbody.velocity = _movementDirection;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _animator.SetBool("IsAttacking", true);
            _canMove = false;
        }

        if (Input.GetKey(KeyCode.E))
        {
            var assetBundleName = AssetDatabase.GetAllAssetBundleNames();
            foreach (var assetPathAndName in AssetDatabase.GetAssetPathsFromAssetBundle(assetBundleName[0]))
            {
                EquipedWeapon = AssetDatabase.LoadAssetAtPath<Weapon>(assetPathAndName);
            }
                UpdateWeapon();
        }

        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("attack") &&
            _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            _canMove = true;
            _animator.SetBool("IsAttacking", false);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _speed = Class.RunSpeed;
        }
        else
        {
            _speed = Class.WalkSpeed;
        }
        float h = Input.GetAxisRaw("Horizontal" + PlayerNumber);
        float v = Input.GetAxisRaw("Vertical" + PlayerNumber);
        if (_canMove)
        {
            _movementDirection = new Vector2(h, v).normalized * _speed;
            SetDirection(h, v);
        }
        else
        {
            _movementDirection = Vector2.zero;
        }
        if (_movementDirection.magnitude > 0)
        {
            _animator.SetBool("IsIdle", false);
        }
        else
        {
            _animator.SetBool("IsIdle", true);
        }


    }

    private void SetDirection(float h, float v)
    {
        if (h < 0)
        {
            _animator.SetBool("IsFacingRight", false);
            _animator.SetInteger("LastDirection", 2);
        }
        else if (h > 0)
        {
            _animator.SetBool("IsFacingRight", true);
            _animator.SetInteger("LastDirection", 0);
        }
        if (v < 0)
        {
            _animator.SetBool("IsFacingDown", true);
            _animator.SetInteger("LastDirection", 1);
        }
        else if (v > 0)
        {
            _animator.SetBool("IsFacingDown", false);
            _animator.SetInteger("LastDirection", 3);
        }
    }

    public float GetSpeed()
    {
        return _speed;
    }
}
