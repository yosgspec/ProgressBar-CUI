Option Strict On
Option Infer On

Namespace ProgressBar
	'モノクロ版プログレスバー
	Class Progress
		'最大の桁数
		Public columns As Integer
		'プログレスバーの長さ
		Public width As Integer
		'進捗度
		Public par As Integer=0
		'目標進捗度
		Public parMax As Integer
		'最後に出力したカーソルの行
		Protected rowLate As Integer=Console.CursorTop

		Sub New(width As Integer,parMax As Integer)
			Me.columns=Console.WindowWidth
			Me.width=width
			Me.parMax=parMax
		End Sub

		'プログレスバーの更新
		Overridable Sub update(message As String)
			Dim row0=Console.CursorTop

			Dim parcent=CSng(par/parMax)
			Dim widthNow=CInt(Math.Floor(width*parcent))

			Dim gauge=New String(">"c,widthNow) & New String(" "c,width-widthNow)
			Dim status=$"({(parcent*100).ToString("f1")}%<-{par}/{parMax})"

			Console.Error.WriteLine($"#[{gauge}]#{status}")
			clearScreenDown()

			Console.Error.WriteLine(message)
			rowLate=Console.CursorTop
			Console.SetCursorPosition(0,row0)
			par+=1
		End Sub

		'プログレスバーの完了
		Overridable Sub done(doneAlert As String)
			Dim sideLen=(width-doneAlert.Length)\2

			Dim gauge=New String("="c,sideLen) & doneAlert
			gauge &=New String("="c,width-gauge.Length)
			Dim status=$"(100%<-{parMax}/{parMax})"

			clearScreenDown()
			Console.Error.WriteLine($"#[{gauge}]#{status}")
		End Sub

		'コンソール表示の掃除
		Protected Sub clearScreenDown()
			Dim clearRange=rowLate-(Console.CursorTop-1)
			Console.Error.Write(New String(" "c,columns*clearRange))
			Console.SetCursorPosition(0,Console.CursorTop-clearRange)
		End Sub
	End Class

	'カラー版プログレスバー
	Class ProgressColor: Inherits Progress
		Sub New(width As Integer,parMax As Integer)
			MyBase.New(width,parMax)
		End Sub

		'プログレスバーの更新
		Overrides Sub update(message As String)
			Dim row0=Console.CursorTop
			Dim parcent=CSng(par/parMax)
			Dim widthNow=CInt(Math.Floor(width*parcent))

			Dim status=$"({(parcent*100).ToString("f1")}%<-{par}/{parMax})"

			Console.BackgroundColor=ConsoleColor.Yellow
			Console.ForegroundColor=ConsoleColor.DarkYellow
			Console.Error.Write("{")
			Console.BackgroundColor=ConsoleColor.Cyan
			Console.Error.Write(New String(" "c,widthNow))
			Console.BackgroundColor=ConsoleColor.DarkCyan
			Console.Error.Write(New String(" "c,width-widthNow))
			Console.BackgroundColor=ConsoleColor.Yellow
			Console.Error.Write("}")
			Console.ResetColor()
			Console.Error.WriteLine(status)
			clearScreenDown()

			Console.Error.WriteLine(message)
			rowLate=Console.CursorTop
			Console.SetCursorPosition(0,row0)
			par+=1
		End Sub

		'プログレスバーの完了
		Overrides Sub done(doneAlert As String)
			Dim sideLen=(width-doneAlert.Length)\2

			Dim gauge=New String(" "c,sideLen) & doneAlert
			gauge &=New String(" "c,width-gauge.Length)
			Dim status=$"(100%<-{parMax}/{parMax})"

			clearScreenDown()

			Console.BackgroundColor=ConsoleColor.Yellow
			Console.ForegroundColor=ConsoleColor.DarkYellow
			Console.Error.Write("{")
			Console.BackgroundColor=ConsoleColor.Green
			Console.ForegroundColor=ConsoleColor.Red
			Console.Error.Write(gauge)
			Console.BackgroundColor=ConsoleColor.Yellow
			Console.ForegroundColor=ConsoleColor.DarkYellow
			Console.Error.Write("}")
			Console.ResetColor()
			Console.Error.WriteLine(status)
		End Sub
	End Class
End Namespace
