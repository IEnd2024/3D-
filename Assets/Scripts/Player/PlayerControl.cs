using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    //变量区
    #region
    Vector2 moveInput;  //获取玩家WASD的输入数据
    Vector2 lookInput;  //获取玩家鼠标的旋转数据
    bool    isAiming;   //检测玩家是否按下右键进行瞄准

    Vector3 movement=Vector3.zero;   //用于计算玩家移动的三维向量

    Transform cameraTransform;    //记录相机transform
    Transform playerTransform;    //记录玩家transform

    //状态机中混合树中对应的三个参数的哈希值变量
    private int vNormalSpeedHash;   //竖直正常状态速度
    private int vAimingSpeedHash;   //竖直瞄准状态速度 
    private int tSpeedHash;   //转弯速度
    private int hSpeedHash;   //水平速度

    //状态机中混合树各参数的临界值
    private float vThresholdForNormal;   //竖直正常状态速度临界 
    private float vThresholdForAiming;   //竖直瞄准状态速度临界 
    //private float tThreshold;            //转弯速度临界
    private float hThreshold;            //水平速度临界

    private Animator animator; //状态机变量

    private float mousex;
    private float mousey;
    public float mouseSensitivity;
    private float rotation;
    public float aimingSpeed;
    public GameObject aimPointV;
    public GameObject aimPointH;
    


    public Vector2 ScreenCenterPoint;
    public LayerMask aimColliderLayerMask=new LayerMask();
    public GameObject test;
    public ParticleSystem ps;
    public ParticleSystem ps2;
    private Vector3 mouseworldPosition;
    public GameObject shootPoint;
    public enum PlayerState    //玩家状态的枚举
    {
        normal,
        aiming
    }
    public PlayerState status=PlayerState.normal; //玩家初始状态为正常状态

    public int hp=3;//玩家血量相关
    #endregion

    //生命周期函数
    #region
    void Start()
    {
        cameraTransform=Camera.main.transform;    //获取主相机
        playerTransform=this.transform;           //获取玩家的transform

        animator = this.GetComponent<Animator>();   //获取主角身上的状态机组件

        //获得状态机中混合树对应的三个参数的哈希值变量
        //使用哈希值的原因：方便访问和控制，以及后续可能的改名
        vNormalSpeedHash = Animator.StringToHash("vertical normal speed");
        vAimingSpeedHash = Animator.StringToHash("vertical aiming speed");
        tSpeedHash = Animator.StringToHash("turn speed");
        hSpeedHash = Animator.StringToHash("horizontal speed");

        //初始化状态机中混合树各参数的临界值
        vThresholdForNormal = 5.66f;
        //tThreshold = 2.46f;
        vThresholdForAiming = 4.0f;
        hThreshold = 4.0f;
        ps.Stop();

        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
    {
        //isAiming = true;
        PlayerMove();
        if (Input.GetMouseButton(0))
        {
            print("1");
            ps.Play();
            Attack();
        }
        //Attack();



    }
    #endregion

    //输入配置文件
    #region 
    public void GetMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    public void GetIsaiming(InputAction.CallbackContext context)
    {
        isAiming = context.ReadValueAsButton();
    }
    public void GetLookInput(InputAction.CallbackContext context) 
    {
        lookInput = context.ReadValue<Vector2>();
    }
    #endregion

    //玩家的移动方法
    public void PlayerMove()
    {
        animator.SetBool("isAiming", isAiming);
        //PlayerMove();   
    }


    //移动方法
    #region
    //public void PlayerMove()
    //{
    //    RotateCamera();
    //    SetAnimatorParameterForAiming();
    //}
    public void RotateCamera()
    {
        mousex = lookInput.x * Time.deltaTime * mouseSensitivity;
        mousey = lookInput.y * Time.deltaTime * mouseSensitivity;
        //print("x:" + mousex + " " + "y:" + mousey);

        this.transform.Rotate(Vector3.up * mousex);

        rotation -= mousey;
        rotation = Mathf.Clamp(rotation, -30f, 50f);
        aimPointV.transform.localRotation = Quaternion.Euler(rotation, 0, 0);
    }
    //public void RotateCamera2()
    //{
    //    mousex = lookInput.x * Time.deltaTime * mouseSensitivity;
    //    mousey = lookInput.y * Time.deltaTime * mouseSensitivity;
    //    //print("x:" + mousex + " " + "y:" + mousey);
    //    aimPointH.transform.Rotate(Vector3.up * mousex);
    //    //this.transform.forward = Vector3.Lerp(this.transform.forward, new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z),0f);

    //    rotation -= mousey;
    //    rotation = Mathf.Clamp(rotation, -30f, 50f);   
    //    aimPointV.transform.localRotation = Quaternion.Euler(rotation, 0, 0);
    //}
    public void SetAnimatorParameterForAiming()
    {
        animator.SetFloat(vAimingSpeedHash, vThresholdForAiming * Input.GetAxis("Vertical") * aimingSpeed, 0.1f, Time.deltaTime);
        animator.SetFloat(hSpeedHash, hThreshold * Input.GetAxis("Horizontal") * aimingSpeed, 0.1f, Time.deltaTime);
    }

    #endregion
    private void SwitchPlayerState()
    {
        ;
    }
    public void Attack()
    {
        ScreenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(ScreenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f, aimColliderLayerMask))
        {
            mouseworldPosition = hit.point;
        }
        GameObject.Instantiate(ps2, mouseworldPosition, hit.transform.rotation);
    }

    public void Wound(int dmg)
    {
        hp -= dmg;
        animator.SetTrigger("Wound");
        if (hp <= 0)
        {
            //死亡
            Debug.Log("主角死亡");
        }
        else
        {
            //播放音效
        }
    }



}
