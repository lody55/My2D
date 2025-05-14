using UnityEngine;
namespace My2D
{
    // Bool�� Ÿ�� �Ķ���� ������ �����ϴ� Ŭ����
    // ����(���¸ӽ�)�� �� ���� ���� �� ���� �������ش�
    public class SetBoolBehaviour : StateMachineBehaviour
    {
        #region Variables
        //���� ������ �Ķ���� �̸�
        public string boolName;

        //�۵��ϴ� ����, ���¸ӽ� üũ
        public bool updateOnState;
        public bool updateOnStateMachine;

        //�� �� ���� ���� �� ����
        public bool valueEnter;
        public bool valueExit;
        #endregion
        // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(updateOnState)
            {
                animator.SetBool(boolName, valueEnter);
                //Debug.Log("����");
            }
        }

        // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
        //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateExit is called before OnStateExit is called on any state inside this state machine
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(updateOnState)
            {
                animator.SetBool(boolName, valueExit);
            }
        }

        // OnStateMove is called before OnStateMove is called on any state inside this state machine
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateIK is called before OnStateIK is called on any state inside this state machine
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateMachineEnter is called when entering a state machine via its Entry Node
        override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            if(updateOnStateMachine)
            {
                animator.SetBool(boolName, valueEnter);
                //Debug.Log("����");
            }
        }

        // OnStateMachineExit is called when exiting a state machine via its Exit Node
        override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        {
            if (updateOnStateMachine)
            {
                animator.SetBool(boolName, valueExit);
            }
        }
    }
}