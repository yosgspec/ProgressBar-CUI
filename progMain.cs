using System;
using System.Threading;
using ProgressBar;

class Program{
	static void Main(){
		const string firstMsg="1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ";
		const string secondMsg="2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ";
		const string thirdMsg="3rdステップ 3rdステップ 3rdステップ 3rdステップ 3rdステップ";
		Console.Write("READY...");
		Console.ReadKey();
		Console.WriteLine("\rSTART!  ");

		const int width=55;
		const int works=270;
		//モノクロ版
		//var prg=new Progress(width,works);
		//カラー版
		var prg=new ProgressColor(width,works);

		for(var i=0;i<=works;i++){
			Thread.Sleep(20);
			if(i<130){
				prg.update(firstMsg);
			}
			else if(i<210){
				prg.update(secondMsg);
			}
			else{
				prg.update(thirdMsg);
			}
		}
		prg.done("Done!");
		Console.WriteLine("終了しました!");
		Console.ReadLine();
	}
}
