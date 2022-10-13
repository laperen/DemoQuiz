using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace DemoQuiz {
    public class QuizCreateHandler : MonoBehaviour {
        public RectTransform questionscontent;
        public GameObject questionlineprefab;
        private static Quiz tempquiz;
        public StringEvt QuizName;

        public static QuizCreateHandler instance;

        private string basepath = Path.Combine(Application.streamingAssetsPath, "QuizFiles");

        public QuizCreateHandler() {
            instance = this;
        }
		public void Init() {
            QuizName.Invoke(tempquiz.text.GetValueByKey(tempquiz.quiznamekey));
            RenderQuestions();
		}
		public static void LoadQuiz(string path) {
            if (!File.Exists(path)) { return; }
            string data = File.ReadAllText(path);
            tempquiz = JsonUtility.FromJson<Quiz>(data);
            SceneManager.LoadScene(1);
        }
        public void CreateNewQuiz() {
            tempquiz = new Quiz("New Quiz");
            SceneManager.LoadScene(1);
        }
        public void ChangeQuizName(string value) {
            tempquiz.text.SetValue(tempquiz.quiznamekey, value);
        }
        public void SettleLayout() {
            for (int i = 0; i < 3; i++) {
                LayoutRebuilder.ForceRebuildLayoutImmediate(questionscontent);
            }
        }
        public void CreateQuestion() {
            tempquiz.NewQuestion("question text");
            RenderQuestions();
        }
        public void RenderQuestions() {
            for (int i = 0, max = tempquiz.questions.Count; i < max; i++) {
                Question q = tempquiz.questions[i];
                Transform child = null;
                if (questionscontent.childCount >= max) {
                    child = questionscontent.GetChild(i);
                    child.gameObject.SetActive(true);
                }
                if (!child) {
                    child = Instantiate(questionlineprefab, questionscontent).transform;
                }
                child.GetComponent<QuestionLine>().RenderQuestion(i, tempquiz);
            }
            if (questionscontent.childCount > tempquiz.questions.Count) {
                for (int i = tempquiz.questions.Count, max = questionscontent.childCount; i < max; i++) {
                    questionscontent.GetChild(i).gameObject.SetActive(false);
                }
            }
            SettleLayout();
        }


        private void SaveQuizData() {
            string quizname = tempquiz.text.GetValueByKey(tempquiz.quiznamekey);
            string quizdir = Path.Combine(basepath, quizname);
            if (!Directory.Exists(quizdir)) {
                Directory.CreateDirectory(quizdir);
            }
            string filepath = Path.Combine(quizdir, $"{quizname}.json");
            string data = JsonUtility.ToJson(tempquiz);
            File.WriteAllText(filepath, data);
        }
        public void SaveQuiz() {
            SaveQuizData();    
            //SceneManager.LoadScene(1);
        }
    }
}