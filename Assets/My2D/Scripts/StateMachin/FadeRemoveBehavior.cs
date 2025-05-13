using UnityEditor.SceneManagement;
using UnityEngine;
namespace My2D
{
    //1.Fadeoutȿ�� ���� �� ��� �ð� ����(���� �ð�)
    //2.Fadeoutȿ���� ������ ����� �� ������Ʈ ų

    
    public class FadeRemoveBehavior : StateMachineBehaviour
    {
        #region Variables
        //����
        private SpriteRenderer spriteRenderer;
        private GameObject removeObject;    //ȿ�� �� ų�� ������Ʈ

        //�����ð� Ÿ�̸�
        public float delayTime = 1f;
        private float delayCountdown = 0f;

        //���̵� ȿ�� Ÿ�̸�
        public float fadeTime = 1f;
        private float fadeCountdown = 0f;

        private Color startColor;
        #endregion
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //���� �ʱ�ȭ
            spriteRenderer = animator.GetComponent<SpriteRenderer>();
            removeObject = animator.transform.parent.gameObject;

            //�ʱ�ȭ
            startColor = spriteRenderer.color;

        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //�����ð� Ÿ�̸�
            if(delayCountdown < delayTime)
            {
                delayCountdown += Time.deltaTime;
            }
            else //�����ð��� ����
            {
                //fadeout Ÿ�̸� 
                fadeCountdown += Time.deltaTime;    // 0 -> 1 : (1 -fadeCountdown)
                //startColor.a => 1-> 0
                float newAlpha = startColor.a * (1 - fadeCountdown / fadeTime);
                spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
                if(fadeCountdown >= fadeTime)
                {
                    Destroy(removeObject);
                }
            }
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }
}