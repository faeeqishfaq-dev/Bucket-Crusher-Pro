import re

with open('Assets/Scripts/Assembly-CSharp/GameManager.cs', 'r', encoding='utf-8') as f:
    content = f.read()

# Replace EndGame
pattern_endgame = r'public void EndGame\(\)\s*\{.*?\s*OnGameEnd\?\.Invoke\(\);\s*\}'
replacement_endgame = """public void EndGame()
	{
		Debug.Log($"[GamePlayTest] GameManager.EndGame() executing! finised={finised}");
		if (finised) return;

		SetInputCanvasActive(false);

		if (currentOilUI == null) currentOilUI = UnityEngine.Object.FindAnyObjectByType<Oil>(FindObjectsInactive.Include);
		if (currentOilUI != null)
		{
			currentOilUI.StopSpend();
		}

		if (OnboardingManager.Instance != null)
		{
			OnboardingManager.Instance.GetNumberGamePlayed++;
		}

		OnGameEnd?.Invoke();
		BackToMenu();
	}"""
content = re.sub(pattern_endgame, replacement_endgame, content, flags=re.DOTALL)

# Replace FinishLevel
pattern_finishlevel = r'public void FinishLevel\(\)\s*\{.*?\s*OnGameEnd\?\.Invoke\(\);\s*\}'
replacement_finishlevel = """public void FinishLevel()
	{
		if (finised) return;
		finised = true;

		SetInputCanvasActive(false);

		if (currentOilUI != null)
		{
			currentOilUI.StopSpend();
		}

		if (gameProperties != null)
		{
			gameProperties.NextLevel();
		}

		OnGameEnd?.Invoke();
		BackToMenu();
	}"""
content = re.sub(pattern_finishlevel, replacement_finishlevel, content, flags=re.DOTALL)

with open('Assets/Scripts/Assembly-CSharp/GameManager.cs', 'w', encoding='utf-8') as f:
    f.write(content)
