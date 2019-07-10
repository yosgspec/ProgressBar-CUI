import os
from time import sleep
from ProgressBar import Progress,ProgressColor

if __name__=="__main__":
	firstMsg="1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ"
	secondMsg="2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ"
	thirdMsg="3rdステップ 3rdステップ 3rdステップ 3rdステップ 3rdステップ"
	print("READY...",end="")
	if os.name=="nt":
		import msvcrt
		msvcrt.getch()
	else:
		input()
	print("\rSTART!  ")

	width=55
	works=270
	#モノクロ版
	#prg=Progress(width,works)
	#カラー版
	prg=ProgressColor(width,works)

	for i in range(0,works+1):
		sleep(0.02)
		if i<130:
			prg.update(firstMsg)
		elif i<210:
			prg.update(secondMsg)
		else:
			prg.update(thirdMsg)
	prg.done("Done!")
	print("終了しました!")
	input()
