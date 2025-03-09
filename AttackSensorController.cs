using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSensorController : MonoBehaviour
{
    // private Rigidbody2D rd;                  // Rigidbody2D �̎Q��
    // public GameObject target;               // ��]�̒��S�I�u�W�F�N�g
    // public Collider2D attackcollider;             // �U���p�R���C�_�[
    // public Transform player;                // �v���C���[��Transform
    // public Vector3 rotationAxis = new Vector3(0, 0, 1); // ��]�� (��: Z��)
    // public float rotationAngle = 90f;       // ��]����p�x
    // public float duration = 0.45f;          // ��]�ɂ����鎞��
    // public float delayBeforeRotation = 0.03f; // ��]���J�n����܂ł̒x������

    // private Quaternion startRotation;       // �����̉�]
    // private Quaternion targetRotation;      // �ڕW�̉�]
    // private float elapsedTime = 0f;         // �o�ߎ���
    // private bool isRotating = false;        // ��]�����ǂ���

    public GameObject atarihantei;
    private Collider2D attackcollider;

    void Start()
    {
        // rd = GetComponent<Rigidbody2D>();
        attackcollider = atarihantei.GetComponent<Collider2D>();

        if (attackcollider != null)
        {
            attackcollider.enabled = false;       // �R���C�_�[�𖳌���
        }
        // startRotation = transform.rotation; // �����̉�]���L�^
    }

    void Update()
    {
        // // ��]���̏���
        // if (isRotating)
        // {
        //     elapsedTime += Time.deltaTime;
        //     float t = elapsedTime / duration;
        //     transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);

        //     // ��]���I���������~���A���������Z�b�g
        //     if (elapsedTime >= duration)
        //     {
        //         transform.rotation = startRotation; // �����̉�]�Ƀ��Z�b�g
        //         isRotating = false;
        //     }
        // }

        // // Z�L�[�ŉ�]���J�n (0.03�b��Ɏ��s)
        // if (Input.GetKeyDown(KeyCode.Z) && !isRotating)
        // {
        //     Invoke(nameof(StartRotationWithDelay), delayBeforeRotation);
        // }

        // Z�L�[�ŃR���C�_�[��؂�ւ�
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (attackcollider != null)
            {
                attackcollider.enabled = true;
            }
        }
        else
        {
            if (attackcollider != null)
            {
                attackcollider.enabled = false;
            }
        }
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     Destroy(gameObject); // �U���I�u�W�F�N�g�̔j��
    // }

    // private void StartRotationWithDelay()
    // {
    //     if (target != null && player != null)
    //     {
    //         // �v���C���[�̌����Ɋ�Â��ĉ�]������ύX
    //         Vector3 playerDirection = player.transform.localScale.x > 0 ? Vector3.forward : Vector3.back;
    //         rotationAxis = playerDirection;

    //         StartRotation();
    //     }
    // }

    // private void StartRotation()
    // {
    //     isRotating = true;
    //     elapsedTime = 0f;
    //     startRotation = transform.rotation; // ���݂̉�]��������]�Ƃ��ċL�^

    //     // �ڕW��]���v�Z
    //     Quaternion rotation = Quaternion.AngleAxis(rotationAngle, rotationAxis);
    //     targetRotation = startRotation * rotation;
    // }
}

