using System;

namespace ProgressBar{
	class Progress{
		//最大の桁数
		public int columns;
		//プログレスバーの長さ
		public int width;
		//進捗度
		public int par=0;
		//目標進捗度
		public int parMax;
		//最後に出力したカーソルの行
		protected int rowLate=Console.CursorTop;

		//モノクロ版プログレスバー
		public Progress(int width,int parMax){
			this.columns=Console.WindowWidth;
			this.width=width;
			this.parMax=parMax;
		}

		//プログレスバーの更新
		public virtual void update(string message){
			int row0=Console.CursorTop;

			float parcent=(float)par/parMax;
			int widthNow=(int)Math.Floor(width*parcent);

			string gauge=new string('>',widthNow)+new string(' ',width-widthNow);
			string status=$"({(parcent*100).ToString("f1")}%<-{par}/{parMax})";

			Console.Error.WriteLine($"#[{gauge}]#{status}");
			clearScreenDown();

			Console.Error.WriteLine(message);
			rowLate=Console.CursorTop;
			Console.SetCursorPosition(0,row0);
			par++;
		}

		//プログレスバーの完了
		public virtual void done(string doneAlert){
			int sideLen=(int)Math.Floor((float)(width-doneAlert.Length)/2);

			string gauge=new string('=',sideLen)+doneAlert;
			gauge+=new string('=',width-gauge.Length);
			string status=$"(100%<-{parMax}/{parMax})";

			clearScreenDown();
			Console.Error.WriteLine($"#[{gauge}]#{status}");
		}

		//コンソール表示の掃除
		protected void clearScreenDown(){
			int clearRange=rowLate-(Console.CursorTop-1);
			Console.Error.Write(new string(' ',columns*clearRange));
			Console.SetCursorPosition(Console.CursorLeft,Console.CursorTop-clearRange);
		}
	}

	//カラー版プログレスバー
	class ProgressColor:Progress{
		public ProgressColor(int width,int parMax):base(width,parMax){}

		//プログレスバーの更新
		public override void update(string message){
			int row0=Console.CursorTop;
			float parcent=(float)par/parMax;
			int widthNow=(int)Math.Floor(width*parcent);

			string status=$"({(parcent*100).ToString("f1")}%<-{par}/{parMax})";

			Console.BackgroundColor=ConsoleColor.Yellow;
			Console.ForegroundColor=ConsoleColor.DarkYellow;
			Console.Error.Write("{");
			Console.BackgroundColor=ConsoleColor.Cyan;
			Console.Error.Write(new string(' ',widthNow));
			Console.BackgroundColor=ConsoleColor.DarkCyan;
			Console.Error.Write(new string(' ',width-widthNow));
			Console.BackgroundColor=ConsoleColor.Yellow;
			Console.Error.Write("}");
			Console.ResetColor();
			Console.Error.WriteLine(status);
			clearScreenDown();

			Console.Error.WriteLine(message);
			rowLate=Console.CursorTop;
			Console.SetCursorPosition(0,row0);
			par++;
		}

		//プログレスバーの完了
		public override void done(string doneAlert){
			int sideLen=(int)Math.Floor((float)(width-doneAlert.Length)/2);

			string gauge=new string(' ',sideLen)+doneAlert;
			gauge+=new string(' ',width-gauge.Length);
			string status=$"(100%<-{parMax}/{parMax})";

			clearScreenDown();

			Console.BackgroundColor=ConsoleColor.Yellow;
			Console.ForegroundColor=ConsoleColor.DarkYellow;
			Console.Error.Write("{");
			Console.BackgroundColor=ConsoleColor.Green;
			Console.ForegroundColor=ConsoleColor.Red;
			Console.Error.Write(gauge);
			Console.BackgroundColor=ConsoleColor.Yellow;
			Console.ForegroundColor=ConsoleColor.DarkYellow;
			Console.Error.Write("}");
			Console.ResetColor();
			Console.Error.WriteLine(status);
		}
	}
}
