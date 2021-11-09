using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    [SerializeField]
    float roateSpeed = 150;
    [SerializeField]
    bool canRotateHorizontal, canRotateVertical;
    [SerializeField]
    [Range(0, 60)]
    float verticalRange;

    float mx, my;

    // Start is called before the first frame update
    void Start()
    {
        mx = transform.localEulerAngles.x;
        my = transform.localEulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        if (canRotateHorizontal)
        {
            my += h * roateSpeed * Time.deltaTime;
        }
        if (canRotateVertical)
        {
            mx += v * roateSpeed * Time.deltaTime;
        }
        mx = Mathf.Clamp(mx, -verticalRange, verticalRange);

        transform.localEulerAngles = new Vector3(-mx, my, 0);
    }
}
