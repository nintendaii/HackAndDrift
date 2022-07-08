using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class DefaultEnemy : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        public Vector3 GetPosition() {
            return transform.position;
        }

        public void Damage()
        {
            Debug.LogError("Damage got");
            PlayDamageAnimation();
        }

        private void PlayDamageAnimation()
        {
            spriteRenderer.color = Color.red;
            StartCoroutine(DelayAnim(.4f));
        }
        
        private IEnumerator DelayAnim(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);
                spriteRenderer.color = Color.white;
            }
        }
    }
}