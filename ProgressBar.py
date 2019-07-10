import sys
import math
import os
import unicodedata
import colorama
from colorama import Fore,Back,Style
colorama.init()

#モノクロ版プログレスバー
class Progress:
	def __init__(self,width,parMax):
		#最大の行数
		self.columns=os.get_terminal_size().columns
		#プログレスバーの長さ
		self.width=width
		#進捗度
		self.par=0
		#目標進捗度
		self.parMax=parMax

	#プログレスバーの更新
	def update(self,message):
		parcent=self.par/self.parMax
		widthNow=math.floor(self.width*parcent)
		rowCnt=math.floor(self.__lenB(message)/self.columns)+2

		gauge=">"*widthNow+" "*(self.width-widthNow)
		status=f"({round(parcent*100,1)}%<-{self.par}/{self.parMax})"

		sys.stderr.write(
			f"#[{gauge}]#{status}\n"+
			"\u001b[0J"+			#clearScreenDown
			message+
			f"\u001b[{rowCnt}A\r\n" #moveCursorUp
		)
		self.par=self.par+1

	#プログレスバーの完了
	def done(self,doneAlert):
		sideLen=math.floor((self.width-len(doneAlert))/2)
		gauge="="*sideLen+doneAlert
		gauge+="="*(self.width-len(gauge))

		status=f"(100%<-{self.parMax}/{self.parMax})"

		sys.stderr.write(
			"\u001b[0J"+	#clearScreenDown
			f"#[{gauge}]#{status}\n"
		)

	#バイト(文字幅)数計算
	def __lenB(self,str):
		len=0
		for val in str:
			cw=unicodedata.east_asian_width(val)
			if cw in u"WFA":
				len=len+2
			else:
				len=len+1
		return len

#カラー版プログレスバー
class ProgressColor(Progress):
	#プログレスバーの更新
	def update(self,message):
		parcent=self.par/self.parMax
		widthNow=math.floor(self.width*parcent)
		rowCnt=math.floor(self._Progress__lenB(message)/self.columns)+2

		status=f"({round(parcent*100,1)}%<-{self.par}/{self.parMax})"

		sys.stderr.write(
			Style.BRIGHT+
			Back.YELLOW+
			Fore.YELLOW+
			"{"+
			Back.CYAN+
			Fore.CYAN+
			"▤"*widthNow+
			" "*(self.width-widthNow)+
			Back.YELLOW+
			Fore.YELLOW+
			"}"+
			Style.RESET_ALL+
			status+
			"\n"+
			"\u001b[0J"+			#clearScreenDown
			message+
			f"\u001b[{rowCnt}A\r\n" #moveCursorUp
		)
		self.par=self.par+1

	#プログレスバーの完了
	def done(self,doneAlert):
		sideLen=math.floor((self.width-len(doneAlert))/2)
		doneAlert=" "+doneAlert+" "
		gauge=(
			Fore.GREEN+
			"▤"*sideLen+
			Fore.RED+
			doneAlert+
			Fore.GREEN+
			"▤"*(self.width-len("#"*sideLen+doneAlert))
		)
		status=f"(100%<-{self.parMax}/{self.parMax})"

		sys.stderr.write(
			"\u001b[0J"+	#clearScreenDown
			Style.BRIGHT+
			Back.YELLOW+
			Fore.YELLOW+
			"{"+
			Back.GREEN+
			gauge+
			Back.YELLOW+
			Fore.YELLOW+
			"}"+
			Style.RESET_ALL+
			status+
			"\n"
		)
