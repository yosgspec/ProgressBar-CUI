Option Strict On
Imports System.Threading
Imports ProgressBar

Module Program
	Sub Main()
		Const firstMsg="1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ"
		Const secondMsg="2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ"
		Const thirdMsg="3rdステップ 3rdステップ 3rdステップ 3rdステップ 3rdステップ"
		Console.Write("READY...")
		Console.ReadKey()
		Console.WriteLine(vbCr & "START!  ")

		Const width=CInt(55)
		Const works=CInt(270)
		'モノクロ版
		'Dim prg As New Progress(width,works)
		'カラー版
		Dim prg As New ProgressColor(width,works)

		For i=0 To works
			Thread.Sleep(20)
			If i<130 Then
				prg.update(firstMsg)
			ElseIf i<210 Then
				prg.update(secondMsg)
			Else
				prg.update(thirdMsg)
			End If
		Next
		prg.done("Done!")
		Console.WriteLine("終了しました!")
		Console.ReadLine()
	End Sub
End Module
