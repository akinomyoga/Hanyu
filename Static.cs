namespace Hanyu{
	public class statics{
		public static string EscapeHTML(string txt){
			return txt.Replace("&","&amp").Replace("<","&gt;").Replace("\"","&quot;");
		}
		public static string XmlPart2HTML(string txt,Hanyu.XmlPart xmlpart){
			string r="";
			int i;int i2=0;string part;string html;
			while(0<=(i=txt.IndexOf("[",i2))){
				r+=txt.Substring(i2,-i2+i++);//間の文字列を追加
				i2=txt.IndexOf("]",i);//括弧の終わりを取得
				if(i2<0){i2=i;break;}//括弧の終わりがない時はそれ以降を全て追加する様にする。
				part=txt.Substring(i,-i+i2++);//[と]に囲まれた文字列を取得
				html=xmlpart.GetElem(part).ToFullHTML();
				r+="<span class='inlineparttype' lang='ja'>"
					+((html=="")?" "+xmlpart.GetElemPart(part).IcoHTML+" ":xmlpart.GetElemPart(part).IcoHTML+" "+html)
					+"</span>";
			}
			r+=txt.Substring(i2,txt.Length-i2);
			return r;
		}
	}
	public class Pinyin{
		//field
		private static string[] before;
		private static string[] after;
		static Pinyin(){
			// アクセント記号の変換のための文字列配列を準備
			string[] array1=new string[]{"a","e","o","i","u","ā","ē","ō","ī","ū","á","é","ó","í","ú","ǎ","ě","ǒ","ǐ","ǔ","à","è","ò","ì","ù"};
			string[][] array2=new string[5][];
			array2[0]=new string[]{"a","ai","ao","an","ang","ar","air","aor","anr","angr"};
			array2[1]=new string[]{"e","ei","en","eng","er","eir","enr","engr"};
			array2[2]=new string[]{"o","ou","ong","or","our","ongr"};
			array2[3]=new string[]{"i","in","ing","ir","inr","ingr"};
			array2[4]=new string[]{"u","un","ur","unr"};
			System.Collections.ArrayList b=new System.Collections.ArrayList();
			System.Collections.ArrayList a=new System.Collections.ArrayList();
			for(int i=0;i<5;i++){
				for(int j=0;j<array2[i].Length;j++){
					for(int k=1;k<5;k++){
						b.Add(array2[i][j]+k.ToString());
						a.Add(array2[i][j].Replace(array1[i],array1[i+k*5]));
					}
				}
			}
			b.AddRange(new string[]{"v1","v2","v3","v4","v"});
			a.AddRange(new string[]{"ǖ","ǘ","ǚ","ǜ","ü"});
			Pinyin.before=(string[])b.ToArray(typeof(string));
			Pinyin.after=(string[])a.ToArray(typeof(string));
		}
		public static string Trans(string pinyin){
			for(int i=0;i<Pinyin.before.Length;i++){
				pinyin=pinyin.Replace(Pinyin.before[i],Pinyin.after[i]);
			}
			return pinyin;
		}
	}
	#region
	#endregion
}

