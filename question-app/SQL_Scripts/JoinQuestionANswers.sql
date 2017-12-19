SELECT *  FROM dbo.UserAnswer u
INNER JOIN questionnaire q ON u.QuestionnaireIdQuestionnaire = q.idQuestionnaire
INNER JOIN question qu ON u.QuestionIdQuestions = qu.idQuestions
INNER JOIN answer a ON u.answer = a.idAnswer
