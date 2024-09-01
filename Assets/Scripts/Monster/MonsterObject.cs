using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterObject : MonoBehaviour
{
    //�������
    private Animator animator;
    //λ�����Ѱ·���
    private NavMeshAgent agent;

    //��������
    
    private int id;            //��ǹ���id
    private int atk=1;           //������
    private int moveSpeed;     //�ƶ��ٶ�
    private int roundSpeed;    //ת���ٶ�
    private int hpmax;         //Ѫ��
    private float atkOffset;   //�������


    //��ǰѪ��
    private int hp;
    //�����Ƿ�����
    public bool isDead = false;
    //
    private float frontTime;

    public GameObject player;
    public GameObject bornPosition;

    private void Awake()
    {
        agent=this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
    }
    //private void InitInfo(MonsterInfo info)
    //{
    //    InfoMonster = info;
    //    //״̬������
    //    animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(info.animator);
    //    //Ҫ��ĵ�ǰѪ��
    //    hp = info.hp;
    //    //�ٶȺͼ��ٶȸ�ֵ֮���Ը�ֵһ����ϣ��û�����Եļ����˶�����һ�������˶���ʼ��
    //    agent.speed = agent.acceleration = info.moveSpeed;
    //    //��ת�ٶ�
    //    agent.angularSpeed = info.roundSpeed;

    //}
    public void Wound(int dmg)
    {
        hp-=dmg;
        animator.SetTrigger("Wound");
        if (hp <= 0)
        {
            //����
            Dead();
        }
        else
        {
            //������Ч
        }
    }

    public void Dead()
    {
        isDead = true;
        //ֹͣ�ƶ�
        agent.isStopped = true;//������������
        animator.SetBool("Dead", true);
        //������Ч

    }

    //��������������Ϻ����õ��¼�����
    public void DeadEvent()
    {
        //��������������Ϻ��Ƴ�����
        //֮�����˹ؿ���������������
    }
    //�����������ƶ�
    //�ƶ���Ѱ·���



    // Update is called once per frame
    void Update()
    {
        ////���ʲôʱ��ͣ��������
        //if (isDead)
        //{
        //    return;
        //}
        ////�����ٶ���������������ʲô
        //animator.SetBool("Run", agent.velocity != Vector3.zero);
        ////����Ŀ���ﵽ�ƶ�����ʱ�͹���
        ////if (Vector3.Distance(this.transform.position, /*Ŀ��*/)< 5 && Time.time - frontTime >= monsterInfo.atkOffset)
        ////{
        ////    //��¼��ι���ʱ��ʱ��
        ////    frontTime = Time.time;
        ////    animator.SetTrigger("Atk");
        ////}
        //StartCoroutine(FindPlayer());
        //print(Vector3.Distance(this.transform.position, player.transform.position));
        //agent.velocity=new Vector3(0,0,0);
        Monster_1();
    }

    public void AktEvent()
    {
        Debug.Log("1");
        Collider[] collider = Physics.OverlapSphere(this.transform.position + this.transform.forward + this.transform.up,3);
        Debug.Log("2");
        for (int i = 0; i < collider.Length; i++)
        {
            if (collider[i].tag == "Player")
            {
                collider[i].gameObject.GetComponent<PlayerControl>().Wound(atk);
                Debug.Log("3");
            }
            

        }

    }

    public void Move_Monster(GameObject target)
    {
        agent.SetDestination(target.transform.position);
        animator.SetBool("Run", agent.velocity!=Vector3.zero);
    }

    public void Attack_Monster( )
    {
        float time=atkOffset;
        if (Time.time - frontTime >=atkOffset)
        {
             frontTime = Time.time;
            animator.SetTrigger("Atk");
        }
    }
    public void Monster_1()
    {
        Vector3 playerPosition = player.transform.position;
        if (this.isDead == false)
        {
            float distance = Vector3.Distance(this.transform.position, playerPosition);
            //print(distance);
            if (distance <= 7f)
            {
                //����;
                //agent.velocity = new Vector3(0, 0, 0);
                Attack_Monster();

            }
            else if (distance <= 30)
            {
                //׷��;
                Move_Monster(player);
            }
            else
            {
                //����
                Move_Monster(bornPosition);
            }
        }
    }
    IEnumerator FindPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.45f);
            Vector3 playerPosition = player.transform.position;
            if(this.isDead==false)
            {
                float distance=Vector3.Distance(this.transform.position,playerPosition);
                if (distance<=2.3f)
                {
                    //����;
                    agent.velocity = new Vector3(0,0,0);
                    Attack_Monster();

                }
                else if(distance<=8)
                {
                    //׷��;
                    Move_Monster(player);
                }
                else
                {
                    //����
                    Move_Monster(bornPosition);
                }
            }
        }

    }
}
