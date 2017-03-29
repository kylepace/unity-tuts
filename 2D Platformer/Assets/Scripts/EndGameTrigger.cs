using UnityEngine;

namespace Assets.Scripts
{
    public class EndGameTrigger : MonoBehaviour
    {
        public int levelnum = 0;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                Application.LoadLevel("Level" + levelnum);
            }
        }
    }
}
