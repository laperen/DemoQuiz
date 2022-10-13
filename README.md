# DemoQuiz

This unity project creates and previews JSON files, which contain formatted data to be presented as quizzes.

Start from the scene: StartScene

![scene](https://user-images.githubusercontent.com/43179300/195527228-7197e926-fbcc-4b8f-b80f-044e5803bdf0.png)

Saved quizzes are created in StreamingAssets, with each quiz under its own folder

![quizlocation](https://user-images.githubusercontent.com/43179300/195530248-26fe2f39-f5e9-4a23-a202-1eb65db23b6f.png)

Justifications:

In question editing, the checkboxes for setting an option to be an answer, does not automatically uncheck all other options in the question. This was left as-is intentionally, since there is no guarantee questions will always only have one correct answer in its options.

![multioption](https://user-images.githubusercontent.com/43179300/195548800-5d010d12-739c-4328-ae50-5a1e2f18b3de.png)

Questions have an additional weighted score. In the event certain questions are meant to be harder than others, this can be represented in the results through this weighted score.

Localization has been planned for but only implemented partially. Within the Quiz class, text is consolidated into one property. Objects meant to reflect text, such as questions and options, use integer keys to grab the relevant text from the property. In the event localization is needed, this property can be accessed to extract the text into CSV files for localization. The intention is to have these language CSV files placed in the individual quiz folders in Streaming Assets, and have objects meant to reflect text access those language CSVs instead of the text in the Quiz class.
