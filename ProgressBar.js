"use strict";
const readline=require("readline");

const ProgressBar=(()=>{
	const PB={};
	const lenB=Symbol();

	//モノクロ版プログレスバー
	PB.Progress=class{
		constructor(width,parMax){
			//最大の桁数
			this.columns=process.stdout.columns;
			//プログレスバーの長さ
			this.width=width;
			//進捗度
			this.par=0;
			//目標進捗度
			this.parMax=parMax;
		}

		//プログレスバーの更新
		update(message){
			const parcent=this.par/this.parMax;
			const widthNow=Math.floor(this.width*parcent);
			const rowCnt=Math.floor(this[lenB](message)/this.columns)+2;

			const gauge=">".repeat(widthNow)+" ".repeat(this.width-widthNow);
			const status=`(${(parcent*100).toFixed(1)}%<-${this.par}/${this.parMax})`;

			console.error(`#[${gauge}]#${status}`);
			readline.clearScreenDown(process.stdout);

			console.error(message);
			readline.moveCursor(process.stdout,0,-rowCnt);
			this.par++;
		}

		//プログレスバーの完了
		done(doneAlert){
			const sideLen=Math.floor((this.width-doneAlert.length)/2);

			var gauge="=".repeat(sideLen)+doneAlert;
			gauge+="=".repeat(this.width-gauge.length);
			const status=`(100%<-${this.parMax}/${this.parMax})`;

			readline.clearScreenDown(process.stdout);
			console.error(`#[${gauge}]#${status}`);
		}
	};
	//バイト(文字幅)数計算
	PB.Progress.prototype[lenB]=function(message){
		var len=0;
		for(let val of message){
			let cc=val.charCodeAt(0);
			if( 0x0000<=cc && cc<=0x024F &&
				cc!=0x0085 && cc!=0x089 && cc!=0x00A7 && cc!=0x00B0 &&
				cc!=0x00B1 && cc!=0x00D7 && cc!=0x00F7 ||
				cc==0xA5 || cc==0x203E || cc==0xF8F0 ||
				0xFF61<=cc && cc<=0xFFDC ||
				0xFFE8<=cc && cc<=0xFFEE
			){
				len=0|len+1;
			}
			else{
				len=0|len+2;
			}
		}
		return len;
	};

	//カラー版プログレスバー
	PB.ProgressColor=class extends PB.Progress{
		//プログレスバーの更新
		update(message){
			const parcent=this.par/this.parMax;
			const widthNow=Math.floor(this.width*parcent);
			const rowCnt=Math.floor(this[lenB](message)/this.columns)+2;

			const status=`(${(parcent*100).toFixed(1)}%<-${this.par}/${this.parMax})`;

			readline.clearScreenDown(process.stderr);
			console.error(
				"\u001b[43m\u001b[5m"+  /*BackLightYellow*/
				"\u001b[33m"+		   /*ForeDarkYellow*/
				"{"+
				"\u001b[46m"+		   /*BackLightCyan*/
				" ".repeat(widthNow)+
				"\u001b[25m"+		   /*BackDarkCyan*/
				" ".repeat(this.width-widthNow)+
				"\u001b[43m\u001b[5m"+  /*BackLightYellow*/
				"}"+
				"\u001b[0m"+			/*ResetColor*/
				status+
				"\n"+
				message
			);
			readline.moveCursor(process.stderr,0,-rowCnt);
			this.par=0|this.par+1
		}

		//プログレスバーの完了
		done(doneAlert){
			const sideLen=Math.floor((this.width-doneAlert.length)/2);

			var gauge=" ".repeat(sideLen)+doneAlert;
			gauge+=" ".repeat(this.width-gauge.length);
			const status=`(100%<-${this.parMax}/${this.parMax})`;

			readline.clearScreenDown(process.stderr);

			console.error(
				"\u001b[43m\u001b[5m"+  /*BackLightYellow*/
				"\u001b[33m"+		   /*ForeDarkYellow*/
				"{"+
				"\u001b[42m"+		   /*BackLightGreen*/
				"\u001b[31m\u001b[1m"+  /*ForeLightRed*/
				gauge+
				"\u001b[43m"+		   /*BackLightYellow*/
				"\u001b[33m\u001b[22m"+ /*ForeDarkYellow*/
				"}"+
				"\u001b[0m"+			/*ColorReset*/
				status
			);
		}
	};

	return PB;
})();
Object.freeze(ProgressBar);
module.exports=ProgressBar;