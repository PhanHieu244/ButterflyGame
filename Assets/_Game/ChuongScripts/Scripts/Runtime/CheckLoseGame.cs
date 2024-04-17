using ChuongCustom;
using UnityEngine;

namespace _Game.ChuongScripts.Scripts.Runtime
{
    public class CheckLoseGame : MonoBehaviour
    {
        private int warningLose;
        private float time;

        private void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log($"trigger {col.name}");
            if (!col.tag.Equals("ball")) return;
            if (col.isTrigger) return;
            warningLose++;
            Debug.Log($"count {warningLose}");
        }

        private void OnTriggerStay2D(Collider2D col)
        {
            Debug.Log($"trigger {col.name}");
            if (!col.tag.Equals("ball")) return;
            if (col.isTrigger) return;
            time += Time.fixedDeltaTime;
            if (time < 1.2f) return;
            InGameManager.OnLose?.Invoke();
            warningLose = 0;
            time = 0;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.tag.Equals("ball")) return;
            if (other.isTrigger) return;
            warningLose--;
            Debug.Log($"count {warningLose}");
            if (warningLose <= 0)
            {
                time = 0;
                warningLose = 0;
            }
        }
    }
}