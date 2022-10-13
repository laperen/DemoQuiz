using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace DemoQuiz {
	[Serializable]
	public class StringEvt : UnityEvent<string> { }
	[Serializable]
	public class BoolEvt : UnityEvent<bool> { }

	public class DataHolder {
		public static string basepath = Path.Combine(Application.streamingAssetsPath, "QuizFiles");
		public static Quiz tempquiz;
		public static int currbackmenu;

		public static void LoadQuiz(string path) {
			if (!File.Exists(path)) { return; }
			string data = File.ReadAllText(path);
			tempquiz = JsonUtility.FromJson<Quiz>(data);
			currbackmenu = 1;
			SceneManager.LoadScene(1);
		}
		public static void CreateNewQuiz() {
			tempquiz = new Quiz("New Quiz");
			SceneManager.LoadScene(1);
		}
		public static void SaveQuizData() {
			string quizname = tempquiz.text.GetValueByKey(tempquiz.quiznamekey);
			string quizdir = Path.Combine(basepath, quizname);
			if (!Directory.Exists(quizdir)) {
				Directory.CreateDirectory(quizdir);
			}
			string filepath = Path.Combine(quizdir, $"{quizname}.json");
			string data = JsonUtility.ToJson(tempquiz);
			File.WriteAllText(filepath, data);
		}
	}
	public class RectUtil {
		public static void SettleLayout(RectTransform rtrans) {
			for (int i = 0; i < 3; i++) {
				LayoutRebuilder.ForceRebuildLayoutImmediate(rtrans);
			}
		}
	}

	[Serializable]
	public class SavableDictionary<Key, Value> {
		public List<Key> keys;
		public List<Value> values;
		public int runningIndex;

		public SavableDictionary() {
			keys = new List<Key>();
			values = new List<Value>();
		}
		public int Count() {
			return keys.Count;
		}
		public SavableDictionary<Key, Value> Add(Key k, Value v) {
			if (null == keys) { keys = new List<Key>(); }
			if (null == values) { values = new List<Value>(); }

			keys.Add(k);
			values.Add(v);
			runningIndex++;
			return this;
		}
		public SavableDictionary<Key, Value> Remove(Key k) {
			if (!keys.Contains(k)) { return this; }
			int index = keys.IndexOf(k);
			return RemoveByIndex(index);
		}
		public SavableDictionary<Key, Value> Remove(Value v) {
			if (!values.Contains(v)) { return this; }
			int index = values.IndexOf(v);
			return RemoveByIndex(index);
		}
		public SavableDictionary<Key, Value> RemoveByIndex(int index) {
			if (index < 0 || index >= keys.Count) { return this; }
			keys.RemoveAt(index);
			values.RemoveAt(index);
			return this;
		}
		public SavableDictionary<Key, Value> Clear() {
			keys.Clear();
			values.Clear();
			return this;
		}
		public bool ContainsKey(Key k) {
			return keys.Contains(k);
		}
		public Value GetValueByKey(Key k) {
			if (!ContainsKey(k)) { return default; }
			return values[keys.IndexOf(k)];
		}
		public Value GetValueByIndex(int index) {
			if (index < 0 || index >= values.Count) { return default; }
			return values[index];
		}
		public Key GetKey(int index) {
			if (index < 0 || index >= values.Count) { return default; }
			return keys[index];
		}
		public void SetValue(Key key, Value newval) {
			if (!ContainsKey(key)) { return; }
			values[keys.IndexOf(key)] = newval;
		}
		public Dictionary<Key, Value> ToDictionary() {
			Dictionary<Key, Value> retval = new Dictionary<Key, Value>();
			for (int i = 0; i < keys.Count; ++i) {
				retval.Add(keys[i], values[i]);
			}
			return retval;
		}
	}
}
public class StringEventLink {
	public string value;
	public Action<string> evt;

	public void InvokeEvent() {
		if (null == evt) { return; }
		evt.Invoke(value);
	}
}
public class IntEventLink {
	public int value;
	public Action<int> evt;
	public void InvokeEvent() {
		if (null == evt) { return; }
		evt.Invoke(value);
	}
}