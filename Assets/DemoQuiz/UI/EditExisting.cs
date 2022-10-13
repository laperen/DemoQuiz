using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace DemoQuiz {
    public class EditExisting : MonoBehaviour {
        public RectTransform content;
        public GameObject btnprefab;

		private void Awake() {
			RenderQuizList();
		}
		public void CreateNewQuiz() {
			DataHolder.CreateNewQuiz();
		}
		private void RenderQuizList() {
			if (!Directory.Exists(DataHolder.basepath)) {
				Directory.CreateDirectory(DataHolder.basepath);
			}
			string[] quizpaths = Directory.GetDirectories(DataHolder.basepath);
			for (int i = 0, max = quizpaths.Length; i < max; i++) {
				string quizname = Path.GetFileName(quizpaths[i]);
				string[] files = Directory.GetFiles(quizpaths[i], "*.json");
				Transform child = null;
				if (content.childCount > i) {
					child = content.GetChild(i);
					child.gameObject.SetActive(true);
				}
				if (!child) {
					child = Instantiate(btnprefab, content).transform;
				}
				StringStore ss = child.GetComponent<StringStore>();
				ss.links = new List<StringEventLink>();
				ss.AddLink(files[0], DataHolder.LoadQuiz);
				ss.AddLink(quizpaths[i], DeleteQuiz);
				ss.AddLink(files[0], PreviewFromList);
				Text nametxt = child.GetChild(0).GetChild(0).GetComponent<Text>();
				nametxt.text = quizname;
			}
			if (content.childCount > quizpaths.Length) {
				for (int i = quizpaths.Length, max = content.childCount; i < max; i++) {
					Transform child = content.GetChild(i);
					child.gameObject.SetActive(false);
				}
			}
		}
		private void DeleteQuiz(string path) {
			if (!Directory.Exists(path)) { return; }
			Directory.Delete(path, true);
			RenderQuizList();
		}
		private void PreviewFromList(string path) {
			if (!File.Exists(path)) { return; }
			DataHolder.tempquiz = JsonUtility.FromJson<Quiz>(File.ReadAllText(path));
			DataHolder.currbackmenu = 0;
			SceneManager.LoadScene(2);
		}
	}
}