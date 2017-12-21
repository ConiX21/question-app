SELECT u.*, r.TrueReponse FROM UtilisateurReponse u
JOIN question q on u.Question_IdQuestions = q.IdQuestions
JOIN reponse r ON u.ValeurReponseUtilisateur = r.IdReponse
WHERE IDQuestionnaire = 2;

SELECT u.* FROM UtilisateurReponse u
JOIN question q on u.Question_IdQuestions = q.IdQuestions

WHERE IDQuestionnaire = 1;

SELECT * FROM REPonse