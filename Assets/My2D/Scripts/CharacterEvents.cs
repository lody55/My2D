using UnityEngine;
using UnityEngine.Events;

namespace My2D
{
    //ĳ���Ϳ��� ����ϴ� �̺�Ʈ �Լ� ���� Ŭ����
    public class CharacterEvents
    {
        public static UnityAction<GameObject , float> characterDamage;
        //�� �̺�Ʈ �Լ� ����
        public static UnityAction<GameObject, float> characterHeal;
        
    }
}