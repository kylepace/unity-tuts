using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class EndGameTrigger : MonoBehaviour
    {
        public int levelnum = 0;
		public bool finalLevel;
		public GameObject playAgainButton;
		public GameObject inGameText;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
				inGameText.SetActive (true);
				if (finalLevel) {
					playAgainButton.SetActive (true);
				} else {
					StartCoroutine ("jumpToNextLevel");
				}
            }
        }

		public void restartGame() {
			Application.LoadLevel("Level1");
		}

		IEnumerator jumpToNextLevel() {
			yield return new WaitForSeconds (2.0f);
			Application.LoadLevel("Level" + levelnum);
		}
    }
}
