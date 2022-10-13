using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DemoQuiz {
    public class EditExisting : MonoBehaviour {
        public RectTransform content;
        public GameObject btnprefab;

		private string basepath = Path.Combine(Application.streamingAssetsPath, "QuizFiles");

		private void Awake() {
			RenderQuizList();
		}
		private void RenderQuizList() {
			if (!Directory.Exists(basepath)) {
				Directory.CreateDirectory(basepath);
			}
			string[] quizpaths = Directory.GetDirectories(basepath);
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
				ss.AddLink(files[0], QuizCreateHandler.LoadQuiz);
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
			Quiz quizdata = JsonUtility.FromJson<Quiz>(File.ReadAllText(path));
			//run preview
		}
	}
}