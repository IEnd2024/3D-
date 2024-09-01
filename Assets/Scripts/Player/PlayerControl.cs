using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    //������
    #region
    Vector2 moveInput;  //��ȡ���WASD����������
    Vector2 lookInput;  //��ȡ���������ת����
    bool    isAiming;   //�������Ƿ����Ҽ�������׼

    Vector3 movement=Vector3.zero;   //���ڼ�������ƶ�����ά����

    Transform cameraTransform;    //��¼���transform
    Transform playerTransform;    //��¼���transform

    //״̬���л�����ж�Ӧ�����������Ĺ�ϣֵ����
    private int vNormalSpeedHash;   //��ֱ����״̬�ٶ�
    private int vAimingSpeedHash;   //��ֱ��׼״̬�ٶ� 
    private int tSpeedHash;   //ת���ٶ�
    private int hSpeedHash;   //ˮƽ�ٶ�

    //״̬���л�������������ٽ�ֵ
    private float vThresholdForNormal;   //��ֱ����״̬�ٶ��ٽ� 
    private float vThresholdForAiming;   //��ֱ��׼״̬�ٶ��ٽ� 
    //private float tThreshold;            //ת���ٶ��ٽ�
    private float hThreshold;            //ˮƽ�ٶ��ٽ�

    private Animator animator; //״̬������

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
    public enum PlayerState    //���״̬��ö��
    {
        normal,
        aiming
    }
    public PlayerState status=PlayerState.normal; //��ҳ�ʼ״̬Ϊ����״̬

    public int hp=3;//���Ѫ�����
    #endregion

    //�������ں���
    #region
    void Start()
    {
        cameraTransform=Camera.main.transform;    //��ȡ�����
        playerTransform=this.transform;           //��ȡ��ҵ�transform

        animator = this.GetComponent<Animator>();   //��ȡ�������ϵ�״̬�����

        //���״̬���л������Ӧ�����������Ĺ�ϣֵ����
        //ʹ�ù�ϣֵ��ԭ�򣺷�����ʺͿ��ƣ��Լ��������ܵĸ���
        vNormalSpeedHash = Animator.StringToHash("vertical normal speed");
        vAimingSpeedHash = Animator.StringToHash("vertical aiming speed");
        tSpeedHash = Animator.StringToHash("turn speed");
        hSpeedHash = Animator.StringToHash("horizontal speed");

        //��ʼ��״̬���л�������������ٽ�ֵ
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

    //���������ļ�
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

    //��ҵ��ƶ�����
    public void PlayerMove()
    {
        animator.SetBool("isAiming", isAiming);
        //PlayerMove();   
    }


    //�ƶ�����
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
            //����
            Debug.Log("��������");
        }
        else
        {
            //������Ч
        }
    }



}
