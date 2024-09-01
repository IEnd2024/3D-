using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterObject : MonoBehaviour
{
    //动画相关
    private Animator animator;
    //位移相关寻路组件
    private NavMeshAgent agent;

    //基础数据
    
    private int id;            //标记怪物id
    private int atk=1;           //攻击力
    private int moveSpeed;     //移动速度
    private int roundSpeed;    //转向速度
    private int hpmax;         //血量
    private float atkOffset;   //攻击间隔


    //当前血量
    private int hp;
    //怪物是否死亡
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
    //    //状态机加载
    //    animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(info.animator);
    //    //要变的当前血量
    //    hp = info.hp;
    //    //速度和加速度赋值之所以赋值一样是希望没有明显的加速运动而是一个匀速运动初始化
    //    agent.speed = agent.acceleration = info.moveSpeed;
    //    //旋转速度
    //    agent.angularSpeed = info.roundSpeed;

    //}
    public void Wound(int dmg)
    {
        hp-=dmg;
        animator.SetTrigger("Wound");
        if (hp <= 0)
        {
            //死亡
            Dead();
        }
        else
        {
            //播放音效
        }
    }

    public void Dead()
    {
        isDead = true;
        //停止移动
        agent.isStopped = true;//播放死亡动画
        animator.SetBool("Dead", true);
        //播放音效

    }

    //死亡动画播放完毕后会调用的事件方法
    public void DeadEvent()
    {
        //死亡动画播放完毕后移除对象
        //之后有了关卡管理器再来处理
    }
    //出生过后再移动
    //移动—寻路组件



    // Update is called once per frame
    void Update()
    {
        ////检测什么时候停下来攻击
        //if (isDead)
        //{
        //    return;
        //}
        ////根据速度来决定动画播放什么
        //animator.SetBool("Run", agent.velocity != Vector3.zero);
        ////检测和目标点达到移动条件时就攻击
        ////if (Vector3.Distance(this.transform.position, /*目标*/)< 5 && Time.time - frontTime >= monsterInfo.atkOffset)
        ////{
        ////    //记录这次攻击时的时间
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
                //攻击;
                //agent.velocity = new Vector3(0, 0, 0);
                Attack_Monster();

            }
            else if (distance <= 30)
            {
                //追击;
                Move_Monster(player);
            }
            else
            {
                //返回
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
                    //攻击;
                    agent.velocity = new Vector3(0,0,0);
                    Attack_Monster();

                }
                else if(distance<=8)
                {
                    //追击;
                    Move_Monster(player);
                }
                else
                {
                    //返回
                    Move_Monster(bornPosition);
                }
            }
        }

    }
}
