using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehavior : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;

    [Header("CharacterController")]
    [SerializeField] bool useCharacterController;
    CharacterController characterController;
    [SerializeField] float slopeLimit = 45f;
    [SerializeField] float stepOffset = 0.3f;
    [SerializeField] float skinWidth = 0.08f;
    [SerializeField] float radius = 0.5f;
    [SerializeField] float height = 2;

    [Header("Rigidbody")]
    [SerializeField] bool useRigidbody;
    Rigidbody rigidbody;

    Vector3 velocity;
    Vector3 lookVector;
    Quaternion lookDir;

    bool isMove;

    // Start is called before the first frame update
    void Start()
    {
        if (useCharacterController ^ useRigidbody == false)
        {
            print("[MovementBehavior] Error : Only one component can be used.");
            gameObject.SetActive(false);
            return;
        }

        if (useCharacterController)
        {
            gameObject.AddComponent<CharacterController>();
            characterController = GetComponent<CharacterController>();
            characterController.center = transform.GetChild(0).position;

            characterController.slopeLimit = slopeLimit;
            characterController.stepOffset = stepOffset;
            characterController.skinWidth = skinWidth;
            characterController.radius = radius;
            characterController.height = height;
        }
        if (useRigidbody)
        {
            gameObject.AddComponent<Rigidbody>();
            rigidbody = GetComponent<Rigidbody>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        MoveByCharacterController();
    }

    private void FixedUpdate()
    {
        MoveByRigidbody();
    }

    // Third Person Point of view
    public void MoveTPS(Vector3 dir)
    {
        isMove = true;

        velocity = dir * moveSpeed;

        lookDir = Quaternion.LookRotation(dir);

        transform.rotation = lookDir;
    }

    // First Person Point of view
    public void MoveFPS(Vector3 dir)
    {
        isMove = true;

        velocity = (transform.right * dir.x + transform.forward * dir.z) * moveSpeed;

        int sign = (dir.z >= 0) ? 1 : -1;
        lookVector = new Vector3(0, dir.x * sign, 0);
        lookDir = Quaternion.Euler(lookVector * rotateSpeed * Time.deltaTime);

        transform.rotation *= lookDir;
    }

    void MoveByCharacterController()
    {
        if (useCharacterController == false) return;
        if (isMove == false) return;

        // 바닥 고려, Time.deltaTime 사용하지 않음
        characterController.SimpleMove(velocity);
        // Vector의 y축 입력에 따라 높이가 변동됨 바닥을 고려하지 않음
        // characterController.Move(velocity * Time.deltaTime);


        velocity = Vector3.zero;
        lookDir = Quaternion.identity;

        isMove = false;
    }

    void MoveByRigidbody()
    {
        if (useRigidbody == false) return;
        if (isMove == false) return;

        rigidbody.MovePosition(rigidbody.position + velocity * Time.deltaTime);

        rigidbody.MoveRotation(rigidbody.rotation * lookDir);

        velocity = Vector3.zero;
        lookDir = Quaternion.identity;

        isMove = false;
    }
}
