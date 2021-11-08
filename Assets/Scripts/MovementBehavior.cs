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
            print("[Error] Only one component can be used.");
            gameObject.SetActive(false);
            return;
        }

        if (useCharacterController)
        {
            gameObject.AddComponent<CharacterController>();
            characterController = GetComponent<CharacterController>();
            characterController.center = transform.GetChild(0).position;
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

    public void Move(Vector3 dir)
    {
        isMove = true;

        velocity = dir * moveSpeed;

        lookDir = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookDir, rotateSpeed * Time.deltaTime);
    }

    // First Person Point of view
    public void MoveFPS(Vector3 dir)
    {
        isMove = true;

        velocity = transform.forward * dir.z * moveSpeed;

        int sign = dir.z >= 0 ? 1 : -1;
        lookVector = new Vector3(0, dir.x * sign, 0);
    }

    void MoveByCharacterController()
    {
        if (useCharacterController == false) return;
        if (isMove == false) return;

        print(isMove);

        characterController.SimpleMove(velocity);

        transform.rotation *= lookDir;

        velocity = Vector3.zero;
        lookVector = Vector3.zero;

        isMove = false;
    }

    void MoveByRigidbody()
    {
        if (useRigidbody == false) return;
        if (isMove == false) return;

        rigidbody.MovePosition(rigidbody.position + velocity * Time.deltaTime);

        lookDir = Quaternion.Euler(lookVector * rotateSpeed * Time.deltaTime);
        rigidbody.MoveRotation(rigidbody.rotation * lookDir);

        velocity = Vector3.zero;
        lookVector = Vector3.zero;

        isMove = false;
    }
}
