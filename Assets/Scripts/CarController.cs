using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] Transform _carDir;

    [Header("Suspensions")]
    [SerializeField] Transform _rFSuspension;
    [SerializeField] Transform _lFSuspension;
    [SerializeField] Transform _rBSuspension;
    [SerializeField] Transform _lBSuspension;

    [SerializeField] Rigidbody Rb;

    [SerializeField] float _suspensionHeight = 1f;

    [SerializeField] float _forceScale = 10f;

    [SerializeField] float _Speed = 100f;
    [SerializeField] float _rotationSpeed = 50f;

    private float h, v;

    private void Update()
    {

        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

    }

    void FixedUpdate()
    {
        Suspension(_rFSuspension);
        Suspension(_rBSuspension);

        Suspension(_lFSuspension);
        Suspension(_lBSuspension);

        Rb.AddForce(_carDir.forward * _Speed * v);



        Rb.AddTorque(_carDir.up * _rotationSpeed * h);
    }
    private void Suspension(Transform suspensionPos)
    {


        if (Physics.Raycast(suspensionPos.position, Vector3.down, out RaycastHit hitInfo, _suspensionHeight))
        {
            if (!hitInfo.transform.CompareTag("Ground")) return;
            Debug.DrawRay(suspensionPos.position, Vector3.down, Color.red);

            float upForce = _suspensionHeight - hitInfo.distance;
            if(!GameManager.Instance.IsGameOver()) Rb.AddForce(suspensionPos.up * upForce * _forceScale);


            Debug.DrawRay(suspensionPos.position, suspensionPos.up * upForce, Color.blue);

        }

    }
}
