using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.UI.GridLayoutGroup;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class ChessMoveAbility : ChessAbility
{
    public bool IsJumping => !_characterController.isGrounded;

    // ��ǥ: [W],[A],[S],[D] �� ����Ű�� ������ ĳ���͸� �� �������� �̵���Ű�� �ʹ�.
    private CharacterController _characterController;
    

    private float _gravity = -9.8f;
    private float _yVelocity = 0f;

    protected override void Awake()    //characterAbility���� �̹� ������̾ override�� ���༭ ������
    {
        base.Awake();   // characterAbility�� �ִ� awake�� awake�� �߰�

        _characterController = GetComponent<CharacterController>();
        
    }


    private void Update()
    {
        

        // ����
        // 1. ������� Ű���� �Է��� �޴´�.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // 2. 'ĳ���Ͱ� �ٶ󺸴� ����'�� �������� ������ �����Ѵ�.
        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();
        dir = Camera.main.transform.TransformDirection(dir);

        // Move�ִϸ��̼�
        //_animator.SetFloat("Move", dir.magnitude);

        // �߷� ����
        _yVelocity += _gravity * Time.deltaTime;
        dir.y = _yVelocity;

        // �޸���
        float moveSpeed = _owner.Stat.MoveSpeed;
        if (Input.GetKey(KeyCode.LeftShift) && _owner.Stat.Stamina > 0)
        {
            moveSpeed = _owner.Stat.RunSpeed;
            _owner.Stat.Stamina -= Time.deltaTime * _owner.Stat.RunConsumeStamina;
        }
        else
        {
            _owner.Stat.Stamina += Time.deltaTime * _owner.Stat.RecoveryStamina;
            if (_owner.Stat.Stamina >= _owner.Stat.MaxStamina)
            {
                _owner.Stat.Stamina = _owner.Stat.MaxStamina;
            }
        }


        // 4. �̵��ӵ��� ���� �� �������� �̵��Ѵ�.
        _characterController.Move(dir * (moveSpeed * Time.deltaTime));

        // 5. �����ϱ�
        bool haveJumpStamina = _owner.Stat.Stamina >= _owner.Stat.JumpConsumeStamina;
        if (haveJumpStamina && Input.GetKeyDown(KeyCode.Space) && _characterController.isGrounded)
        {
           // _animator.SetTrigger("Jump");
            _owner.Stat.Stamina -= _owner.Stat.JumpConsumeStamina;
            _yVelocity = _owner.Stat.JumpPower;
        }

    }

    public void Teleport(Vector3 position)
    {
        _characterController.enabled = false; //

        transform.position = position;

        _characterController.enabled = true;
    }
}