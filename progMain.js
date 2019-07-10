"use strict";
const readline=require("readline");
readline.emitKeypressEvents(process.stdin);
const rl=readline.createInterface({input:process.stdin,output:process.stdout,terminal:false});
process.stdin.setRawMode(true);

const {Progress,ProgressColor}=require("./ProgressBar");

const g=function*(){
	const firstMsg="1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ 1stステップ";
	const secondMsg="2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ 2ndステップ";
	const thirdMsg="3rdステップ 3rdステップ 3rdステップ 3rdステップ 3rdステップ";
	process.stdout.write("READY...");
	yield process.stdin.once("keypress",key=>g.next(key));
	console.log("\rSTART!  ");

	const width=55;
	const works=270;
	//モノクロ版
	//const prg=new Progress(width,works);
	//カラー版
	const prg=new ProgressColor(width,works);

	for(let i=0;i<=works;i++){
		yield setTimeout(()=>g.next(),20);
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
	console.log("終了しました!");
	process.stdin.setRawMode(false);
	yield rl.once("line",val=>g.next(val));
	rl.pause();
}();
g.next();
