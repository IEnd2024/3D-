using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class eplaycontroller : MonoBehaviour
{
    //������
    #region
    Vector2 moveInput;  //��ȡ���WASD����������
    Vector2 lookInput;  //��ȡ���������ת����
    bool isAiming;   //�������Ƿ����Ҽ�������׼

    Vector3 movement = Vector3.zero;   //���ڼ�������ƶ�����ά����

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
    public LayerMask aimColliderLayerMask = new LayerMask();
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
    public PlayerState status = PlayerState.normal; //��ҳ�ʼ״̬Ϊ����״̬
    #endregion

    //�������ں���
    #region
    void Start()
    {
        cameraTransform = Camera.main.transform;    //��ȡ�����
        playerTransform = this.transform;           //��ȡ��ҵ�transform

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
        if (isAiming == false)
        {
            //ps.Stop();
            //NormalMove();
            AimingMove();
        }
        else if (isAiming == true)
        {
            AimingMove();
            //ps.Play();
        }
    }

    //��ͨ״̬���ƶ�����
    #region
    public void NormalMove()
    {
        CaculateInputIndrection();
        SetAnimatorParameterForNormal();
    }

    //������ҵ��ƶ�����movement(�ڵ����˳���������Ҫ��������ĳ�����Ϊ����泯���ƶ�����)
    private void CaculateInputIndrection()
    {
        //��ȡ�������ǰ���泯��(z��)��׼����(��xozƽ����)
        Vector3 cameraForwardDirection = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z).normalized;
        //����ƶ���������=�������ˮƽ����(x��)*�����������ˮƽֵ(ad)+������泯��*�������������ֱֵ(ws)
        movement = cameraTransform.right * moveInput.x + cameraForwardDirection * moveInput.y;
        //����������ת��Ϊ��ҵı�������
        movement = playerTransform.InverseTransformDirection(movement);
    }

    //���ƶ���״̬���еĸ��ֲ���-player�ĺ����ƶ��߼�
    private void SetAnimatorParameterForNormal()
    {
        //��ֱ�������ҵ��ƶ��߼�
        //�������е�����״̬����ֱ�ٶ���0.1s�ڵ���Ŀ��ֵ
        //Ŀ��ֵ��������ٽ��ٶ�*����ƶ�����(ʹĿ��ֵ��������Ƿ�������ٽ�ֵ��0֮�����)
        animator.SetFloat(vNormalSpeedHash, vThresholdForNormal * movement.magnitude, 0.1f, Time.deltaTime);

        //ˮƽ���������ƶ��߼�
        //������ƶ������ļн�ֵ
        float rad = Mathf.Atan2(movement.x, movement.z);
        //�������е�����״̬��ת���ٶ���0.1s�ڵ��������н�
        animator.SetFloat(tSpeedHash, rad, 0.1f, Time.deltaTime);
        //ֱ����ת��ҵ�transform��Ŀ������ת���ٶ�
        playerTransform.Rotate(0, rad * 180 * Time.deltaTime, 0f);
    }
    #endregion

    //��׼״̬���ƶ�����
    #region
    public void AimingMove()
    {
        RotateCamera();
        SetAnimatorParameterForAiming();
    }
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
}
