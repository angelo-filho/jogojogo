using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform gunCannel;
    private PlayerInput playerInput;
    private Vector2 mousePos;
    private Camera camera;

    private void Awake() {
        playerInput = new PlayerInput();

        playerInput.Player.Aim.performed += ctx => mousePos = ctx.ReadValue<Vector2>();
        playerInput.Player.Shoot.performed += _ => Shoot();

        camera = Camera.main;
    }

    private void OnEnable() {
        playerInput.Enable();
    }

    private void OnDisable() {
        playerInput.Disable();
    }

    private void Update()
    {
        AimMouse();
    }

    private void AimMouse() 
    {
        var mouseWorldPos = camera.ScreenToWorldPoint(mousePos);

        var difference = mouseWorldPos - transform.position;

        var angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0f, 0f, angle);
    }

    private void Shoot() 
    {
        Instantiate(bullet, gunCannel.position, transform.rotation);
    }
}
