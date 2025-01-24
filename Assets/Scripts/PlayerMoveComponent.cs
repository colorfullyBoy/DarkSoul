using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using UnityEditor;

public class PlayerMoveComponent : MonoBehaviour
{
    Transform cameraTrans;
    Vector3 moveDirection;
    /**自己的那个输入类*/
    InputManager inputHandler;
    [HideInInspector]
    public AnimationHandler animatorHandler;
    [HideInInspector]
    public Transform myTrans;
    //new + 类名是用来声明隔离基类和子类的同名变量的，可能之后这个脚本会被继承
    [HideInInspector]

    public new Rigidbody rigidbody;
    [HideInInspector]

    public GameObject normalCamera;
    [Header("Stats")]
    [SerializeField]
    private float movementSpeed = 5;
    [SerializeField]
    private float rotationSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        this.animatorHandler = this.GetComponent<AnimationHandler>();
        this.rigidbody = GetComponent<Rigidbody>();
        this.inputHandler = this.GetComponent<InputManager>();
        this.cameraTrans = Camera.main.transform;
        myTrans = this.transform;
        animatorHandler.init();
    }

    #region CharMove
    Vector3 normalVector;
    Vector3 tragetPosition;
    //根据相机处理玩家旋转
    private void handleRotation(float dt){
        Vector3 targetDir = Vector3.zero;
        float moveOverride = inputHandler.moveAmount;
        targetDir = cameraTrans.forward * inputHandler.vertical;
        targetDir += cameraTrans.right * inputHandler.horizontal;
        targetDir.Normalize();
        targetDir.y = 0;
        if (targetDir == Vector3.zero)
        {
            targetDir = myTrans.forward;
        }
        float rs = rotationSpeed;
        Quaternion trans = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(myTrans.rotation, trans, rs * dt);
        myTrans.rotation = targetRotation;
    }

    #endregion

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        inputHandler.tickInput(dt);
        this.playerMove();
        this.playerRotate();
        
    }

    private void playerMove(){
        moveDirection = cameraTrans.forward * inputHandler.vertical;
        moveDirection += cameraTrans.right * inputHandler.horizontal;
        moveDirection.Normalize();
        float speed = movementSpeed;
        moveDirection *= speed;
        moveDirection.y = rigidbody.velocity.y;
        Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
        rigidbody.velocity = projectedVelocity;
        animatorHandler.updateAnimatorValues(inputHandler.moveAmount, 0);
    }

    private void playerRotate(){
        if (this.animatorHandler == null || !this.animatorHandler.canRotate){
            return;   
        }
        float dt = Time.deltaTime;
        handleRotation(dt);
    }
}
