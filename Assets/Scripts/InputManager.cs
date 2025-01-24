using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

public class InputManager : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float moveAmount;
    public float mouseX;
    public float mouseY;

    PlayerControlers inputActions;
    CameraHandler cameraHandler;

    Vector2 movementInput;
    Vector2 cameraInput;
    
    private void Awake() {
        cameraHandler = CameraHandler.instance;
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;
        if (cameraHandler == null)
        {
            return;
        }
        cameraHandler.followTarget(dt);
        cameraHandler.handlerCameraRotation(dt, mouseX, mouseY);
    }

    private void OnEnable() {
        if (inputActions == null)
        {
            this.initPlayerContorlers();
        }
        inputActions.Enable();
    }

    private void OnDisable() {
        if (inputActions == null)
        {
            return;   
        }
        inputActions.Disable();
    }

    public void tickInput(float dt){
        moveInput(dt);
    }

    private void moveInput(float dt){
        horizontal = movementInput.x;
        vertical = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        mouseX  = cameraInput.x;
        mouseY = cameraInput.y;
    }

    /**初始化玩家控制器，用变量去接受inputcontorler里面的二维向量*/
    private void initPlayerContorlers(){
        this.inputActions = new PlayerControlers();
        this.inputActions.PlayerMovement.MoveMent.performed += (inputActions)=>{
            this.movementInput = inputActions.ReadValue<Vector2>();
        };
        this.inputActions.PlayerMovement.Camera.performed += (InputAction)=>{
            this.cameraInput = InputAction.ReadValue<Vector2>();
        };
    }
}
