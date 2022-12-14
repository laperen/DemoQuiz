using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace DemoQuiz {

	public class MonoEvents : MonoBehaviour {
		public UnityEvent AwakeEvt, StartEvt, EnableEvt, DisableEvt, DestroyEvt;

		private void Awake() {
			AwakeEvt.Invoke();
		}
		private void Start() {
			StartEvt.Invoke();
		}
		private void OnEnable() {
			EnableEvt.Invoke();
		}
		private void OnDisable() {
			DisableEvt.Invoke();
		}
		private void OnDestroy() {
			DestroyEvt.Invoke();
		}
		public void ChangeToScene(string scenename) {
			SceneManager.LoadScene(scenename);
		}
	}
}