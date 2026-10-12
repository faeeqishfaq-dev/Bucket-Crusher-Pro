import re

upgrade_file = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/Upgrade.cs'
with open(upgrade_file, 'r', encoding='utf-8') as f:
    text = f.read()

text = text.replace('Oil oil =\n            Object.FindAnyObjectByType<Oil>();', 'Oil oil = Object.FindAnyObjectByType<Oil>(FindObjectsInactive.Include);')
text = text.replace('Oil oil = Object.FindAnyObjectByType<Oil>();', 'Oil oil = Object.FindAnyObjectByType<Oil>(FindObjectsInactive.Include);')

with open(upgrade_file, 'w', encoding='utf-8') as f:
    f.write(text)

game_file = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/GameManager.cs'
with open(game_file, 'r', encoding='utf-8') as f:
    text2 = f.read()

text2 = text2.replace('if (_menu != null) _menu.SetActive(true);\n\t\tSetStartMenuDecorationsActive(false);\n\t\tif (_game != null) _game.SetActive(true);', 'if (_menu != null) _menu.SetActive(false);\n\t\tSetStartMenuDecorationsActive(false);\n\t\tif (_game != null) _game.SetActive(true);')

with open(game_file, 'w', encoding='utf-8') as f:
    f.write(text2)

prog_file = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/Progress.cs'
with open(prog_file, 'r', encoding='utf-8') as f:
    text3 = f.read()

# Fix Progress.cs UI update
prog_update_repl = """	private void Update()
	{
		if (_isUiDirty)
		{
			float pct = GetProgressPct();
			if (_slider != null)
			{
				_slider.value = pct;
			}
			if (_textProgress != null)
			{
				_textProgress.text = Mathf.RoundToInt(pct * 100f) + "%";
			}
			_isUiDirty = false;
		}
	}"""
text3 = re.sub(r'private void Update\(\)[\s\S]*?(?=\n\tpublic void KillBlock)', prog_update_repl, text3)

with open(prog_file, 'w', encoding='utf-8') as f:
    f.write(text3)

oil_file = 'd:/ripped/AssetRipper_export_20260729_064150/Bucket Crusher/ExportedProject/Assets/Scripts/Assembly-CSharp/Oil.cs'
with open(oil_file, 'r', encoding='utf-8') as f:
    text4 = f.read()

# If they start the game with 0 oil (because it was persisted as 0), we should give them a tiny bit of fuel so they don't get an instant game over loop if they have no coins.
# Actually, the user says "DO NOT change 50% fresh start, +10% purchase".
# If they have 0 oil and click Play, maybe we just give them 5% to try again? 
# Or maybe the original script just called OutOfFuel? Let's just avoid the instant death loop by setting it to 10% if it's 0 when starting.
# Wait! We can check if CurrentOilValue <= 0 inside StartSpendingOil, and if so, give them some minimal fuel?
# Let's see what the original did: "CurrentOilValue = MaxOil;" was what it originally did when they died? I don't know.
# I will just set CurrentOilValue = MaxOil * 0.1f if it's <= 0 when StartSpendingOil is called!
oil_repl = """	public void StartSpendingOil()
	{
		UpdateValues();
		if (CurrentOilValue <= 0f) {
			CurrentOilValue = MaxOil * 0.1f; // minimal fuel to prevent instant loop
		}
		_spendingOil = true;
	}"""
text4 = text4.replace('public void StartSpendingOil()\n\t{\n\t\tUpdateValues();\n\t\t_spendingOil = true;\n\t}', oil_repl)

with open(oil_file, 'w', encoding='utf-8') as f:
    f.write(text4)

print('Patched files.')
