using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 720f; // C° per second
    [SerializeField] private Transform cameraTransform;

    [SerializeField] private float pushForceOffsett = 2.8f;
    private CharacterController _controller;
    private float _verticalVelocity;
    private float _gravity = -9.81f;
    private float _verticalRotation = 0f;
     
     private void Awake()
     {
         _controller = GetComponent<CharacterController>();
         Cursor.lockState = CursorLockMode.Locked;
         Cursor.visible = false;
     }
 
     private void Update()
     {
         HandleMovement();
         HandleRotation();
     }
 
     private void HandleMovement()
     {
         float moveX = Input.GetAxis("Horizontal");
         float moveZ = Input.GetAxis("Vertical");
 
         Vector3 move = transform.right * moveX + transform.forward * moveZ;
         move *= moveSpeed;
 
         // manual gravity
         if (_controller.isGrounded)
             _verticalVelocity = -0.15f; // offset to be sure is on the surface
         else
             _verticalVelocity += _gravity * Time.deltaTime;
 
         move.y = _verticalVelocity;
         _controller.Move(move * Time.deltaTime);
     }
 
     private void HandleRotation()
     {
         float mouseX = Input.GetAxis("Mouse X");
         float mouseY = Input.GetAxis("Mouse Y");

         // HOrizontal rotation
         Vector3 rotation = new Vector3(0f, mouseX * rotationSpeed * Time.deltaTime, 0f);
         transform.Rotate(rotation);

         // vertical rotation
         _verticalRotation -= mouseY * rotationSpeed * Time.deltaTime;
         _verticalRotation = Mathf.Clamp(_verticalRotation, -80f, 80f);

         cameraTransform.localEulerAngles = new Vector3(_verticalRotation, 0f, 0f);
     }
     
     
     private void HandlePush()
     {
         if (!_controller) return;

         RaycastHit hit;

         Vector3 rayOrigin = transform.position + Vector3.up * 0.5f;
         Vector3 direction = transform.forward;
         float rayDistance = 0.35f; 

         //debug
         Debug.DrawRay(rayOrigin, direction * rayDistance, Color.red);

         if (Physics.Raycast(rayOrigin, direction, out hit, rayDistance))
         {
             Rigidbody body = hit.collider.attachedRigidbody;

             if (body != null && !body.isKinematic)
             {
                 Vector3 pushDir = new Vector3(hit.normal.x, 0, hit.normal.z) * -1f;
                 
                 body.AddForce(pushDir *  pushForceOffsett, ForceMode.Impulse);
             }
         }
     }
     
     private void FixedUpdate()
     {
         HandlePush(); // Push of objects in front of the player
     }
}
