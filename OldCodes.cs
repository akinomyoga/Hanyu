//
//	※	このファイルの内容はコンパイルされません。
//		使用しなくなったロジックを、「また必要になるかも知れない」という理由でとっておく場所です。
//
namespace Hanyu{
	public class Words1{
		//===== ここから
		#region ToTreeNode
		public System.Windows.Forms.TreeNode ToTreeNode(){
			switch(this.xdoc.DocumentElement.Name.ToLower()){
				case "wordbook":
                    return this.WordBook2Node(this.xdoc.DocumentElement);
				case "shelf":
					//TODO:
				case "library":
					//TODO:
				default:
					return new System.Windows.Forms.TreeNode("blank");
			}
		}
		private System.Windows.Forms.TreeNode WordBook2Node(System.Xml.XmlElement elem){
			System.Windows.Forms.TreeNode r=new System.Windows.Forms.TreeNode(elem.GetAttribute("title"),0,0);
			r.Tag=elem;
			System.Xml.XmlElement child;
			for(int i=0;i<elem.ChildNodes.Count;i++){
				if(elem.ChildNodes[i].NodeType!=System.Xml.XmlNodeType.Element)continue;
				child=(System.Xml.XmlElement)elem.ChildNodes[i];
				if(child.Name=="chapter"){
					r.Nodes.Add(this.Chapter2Node(child));
				}else if(child.Name=="page"){
					r.Nodes.Add(this.Page2Node(child));
				}else if(child.Name=="group"){
					r.Nodes.Add(this.Group2Node(child));
				}
			}
			return r;
		}
		private System.Windows.Forms.TreeNode Chapter2Node(System.Xml.XmlElement elem){
			System.Windows.Forms.TreeNode r=new System.Windows.Forms.TreeNode(elem.GetAttribute("number")+" "+elem.GetAttribute("title"),0,0);
			r.Tag=elem;
			System.Xml.XmlElement child;
			for(int i=0;i<elem.ChildNodes.Count;i++){
				if(elem.ChildNodes[i].NodeType!=System.Xml.XmlNodeType.Element)continue;
				child=(System.Xml.XmlElement)elem.ChildNodes[i];
				if(child.Name=="page"){
					r.Nodes.Add(this.Page2Node(child));
				}else if(child.Name=="group"){
					r.Nodes.Add(this.Group2Node(child));
				}
			}
			return r;
		}
		private System.Windows.Forms.TreeNode Page2Node(System.Xml.XmlElement elem){
			System.Windows.Forms.TreeNode r=new System.Windows.Forms.TreeNode(elem.GetAttribute("number")+" "+elem.GetAttribute("title"),1,1);
			r.Tag=elem;
			System.Xml.XmlElement child;
			for(int i=0;i<elem.ChildNodes.Count;i++){
				if(elem.ChildNodes[i].NodeType!=System.Xml.XmlNodeType.Element)continue;
				child=(System.Xml.XmlElement)elem.ChildNodes[i];
				if(child.Name=="group"){
					r.Nodes.Add(this.Group2Node(child));
				}
			}
			return r;
		}
		private System.Windows.Forms.TreeNode Group2Node(System.Xml.XmlElement elem){
			System.Windows.Forms.TreeNode r=new System.Windows.Forms.TreeNode(elem.GetAttribute("title"),2,2);
			r.Tag=elem;
			return r;
		}
		#endregion
		
		#region ToListViewItems
		/// <summary>
		/// 指定した要素の word 子要素を ListViewItem に変換して配列として返します。
		/// </summary>
		/// <param name="elem">指定する要素(word を子要素に持つ)</param>
		/// <returns>System.Windows.Forms.ListViewItem[]</returns>
		public System.Windows.Forms.ListViewItem[] ToListViewItems(System.Xml.XmlElement elem){
			System.Collections.ArrayList r=new System.Collections.ArrayList();
			System.Xml.XmlElement child;
			for(int i=0;i<elem.ChildNodes.Count;i++){
				if(elem.ChildNodes[i].NodeType!=System.Xml.XmlNodeType.Element)continue;
				child=(System.Xml.XmlElement)elem.ChildNodes[i];
				if(child.Name.ToLower()!="word")continue;
				r.Add((System.Windows.Forms.ListViewItem)(Hanyu.ElemWord)child);
			}
			return (System.Windows.Forms.ListViewItem[])r.ToArray(typeof(System.Windows.Forms.ListViewItem));
		}
		#endregion
		//===== ここまで
		public class WordGroup{
			public System.Windows.Forms.ListViewItem[] LVIWordContent(){
				//===== ここから
				//まず情報を取り出す
				System.Collections.ArrayList array=new System.Collections.ArrayList();
				System.Xml.XmlElement child;
				for(int i=0;i<this.elem.ChildNodes.Count;i++){
					if(this.elem.ChildNodes[i].NodeType!=System.Xml.XmlNodeType.Element)continue;
					child=(System.Xml.XmlElement)this.elem.ChildNodes[i];
					if(child.Name.ToLower()!="word")continue;
					array.AddRange(((Hanyu.Word)child).GetContents());
				}
				//一つずつ ListViewItem に変換していく
				System.Collections.ArrayList r=new System.Collections.ArrayList();
				System.Windows.Forms.ListViewItem item;
				Hanyu.ElemWordContent content;
				for(int i=0;i<array.Count;i++){
					content=(Hanyu.ElemWordContent)array[i];
					item=new System.Windows.Forms.ListViewItem(new string[]{content.ParentElemWord.Name,content.ListViewText});
					item.UseItemStyleForSubItems=false;
					item.SubItems[0].Font=new System.Drawing.Font("SimSun",10);
					item.Tag=content;
					//TODO:content.Part に応じて item に imageIndex を付ける
					r.Add(item);
				}
				//===== ここまで
				return (System.Windows.Forms.ListViewItem[])r.ToArray(typeof(System.Windows.Forms.ListViewItem));
			}
		}
	}
	public class Word{
		//===== ここから
		// <summary>
		/// ListViewItem[] を作成するために必要な、単語を説明する情報(それぞれの、意味、熟語、構文、例文)を返す。 
		/// </summary>
		/// <returns>
		/// System.Collections.ArrayList。登録されている物は、Word.Content。
		/// </returns>
		public System.Collections.ArrayList GetContents(){
			System.Collections.ArrayList array=new System.Collections.ArrayList();
			for(int i=0;i<this.elem.ChildNodes.Count;i++){
				if(this.elem.ChildNodes[i].NodeType==System.Xml.XmlNodeType.Element){
					array.Add((Hanyu.Word.Content)(System.Xml.XmlElement)this.elem.ChildNodes[i]);
				}
			}
			return array;
		}
		private string N2HTML(System.Xml.XmlElement elem){
			// 品詞
			string temp;
			switch(elem.Name.ToLower()){
					//TODO: アイコンを用意
				case "n":temp="[名]";break;
				case "ad":temp="[副]";break;
				case "a":temp="[形]";break;
				case "v":temp="[動]";break;
				case "inter":temp="[間]";break;
				case "prp":temp="[介]";break;
				case "particle":temp="[助]";break;
				case "cnj":temp="[接]";break;
				case "num":temp="[数]";break;
				default:
					return "<p>Words1::N2HTML<br/>指定した要素は、対応している要素ではありません</p>";
			}
			string r="<h2 style\"font-family:SimSun;\">"+temp+elem.GetAttribute("ja")+"</h2>";
			// Attribute : class
			temp=elem.GetAttribute("class");
			if(temp!=""){
				string[] temp1=temp.Split(new char[]{' ','\t','\n'});
				for(int i=0;i<temp1.Length;i++){
					r+=this.AttrCLASS2HTML(temp1[i]);
				}
			}
			//-----
			//   詳しい内容
			//-----
			string r2="";
			// Attribute : desc
			temp=elem.GetAttribute("desc");
			if(temp!="")r2+="<p style=\"font-family:'Microsoft Sans Serif';\">説明: "+temp+"</p>";
			// Childs : examples
			System.Xml.XmlElement child;
			for(int i=0;i<elem.ChildNodes.Count;i++){
				if(elem.ChildNodes[i].NodeType!=System.Xml.XmlNodeType.Element)continue;
				child=(System.Xml.XmlElement)elem.ChildNodes[i];
				if(child.Name.ToLower()=="exm")r2+=this.Exm2HTML(child);
			}
			if(r2!="")r+="<div style=\"border:1px solid gray;\">"+r2+"</div>";
			return r;
		}
		private string Q2HTML(System.Xml.XmlElement elem){
			System.Xml.XmlElement child;
			string r="<h2 style\"font-family:SimSun;\">[量]"+elem.GetAttribute("ja")+"</h2>";
			// Attribute : class
			string temp=elem.GetAttribute("class");
			if(temp!=""){
				string[] temp1=temp.Split(new char[]{' ','\t','\n'});
				for(int i=0;i<temp1.Length;i++){
					r+=this.AttrCLASS2HTML(temp1[i]);
				}
			}
			//-----
			//   詳しい内容
			//-----
			string r2="";
			// Attribute : desc
			temp=elem.GetAttribute("desc");
			if(temp!="")r2+="<p style=\"font-family:'Microsoft Sans Serif';\">説明: "+temp+"</p>";
			// Attribute : target
			temp=elem.GetAttribute("target");
			if(temp!="")r2+="<p style=\"font-family:'Microsoft Sans Serif';\">適用: "+temp+"</p>";
			// Childs : examples
			for(int i=0;i<elem.ChildNodes.Count;i++){
				if(elem.ChildNodes[i].NodeType!=System.Xml.XmlNodeType.Element)continue;
				child=(System.Xml.XmlElement)elem.ChildNodes[i];
				if(child.Name.ToLower()=="exm")r2+=this.Exm2HTML(child);
			}
			if(r2!="")r+="<div style=\"border:1px solid gray;\">"+r2+"</div>";
			return r;
		}
		private string Id2HTML(System.Xml.XmlElement elem){
			// 品詞
			string temp;
			switch(elem.Name.ToLower()){
				//TODO: アイコンを用意
				case "id":temp="[熟]";break;
				case "const":temp="[構]";break;
				default:
					return "<p>Words1::N2HTML\n指定した要素は、対応している要素ではありません</p>";
			}
			string r="<h2 style\"font-family:SimSun;\">"+temp+elem.GetAttribute("name")+"</h2>";
			// Attribute : class
			temp=elem.GetAttribute("class");
			if(temp!=""){
				string[] temp1=temp.Split(new char[]{' ','\t','\n'});
				for(int i=0;i<temp1.Length;i++){
					r+=this.AttrCLASS2HTML(temp1[i]);
				}
			}
			//-----
			//   詳しい内容
			//-----
			string r2="";
			// Attribute : ja
			temp=elem.GetAttribute("ja");
			if(temp!="")r2+="<p style=\"font-family:'Microsoft Sans Serif';\">意味: "+temp+"</p>";
			// Attribute : desc
			temp=elem.GetAttribute("desc");
			if(temp!="")r2+="<p style=\"font-family:'Microsoft Sans Serif';\">説明: "+temp+"</p>";
			// Childs : examples
			System.Xml.XmlElement child;
			for(int i=0;i<elem.ChildNodes.Count;i++){
				if(elem.ChildNodes[i].NodeType!=System.Xml.XmlNodeType.Element)continue;
				child=(System.Xml.XmlElement)elem.ChildNodes[i];
				if(child.Name.ToLower()=="exm")r2+=this.Exm2HTML(child);
			}
			if(r2!="")r+="<div style=\"border:1px solid gray;\">"+r2+"</div>";
			return r;
		}
		private string AttrCLASS2HTML(string className){
			//TODO:アイコンをつける。説明を付ける。
			return "<p>"+className+"</p>";
		}
		private string Exm2HTML(System.Xml.XmlElement elem){
			string r="<h2 style\"font-family:SimSun;\">[例]</h2>";
			// Attribute : class
			string temp=elem.GetAttribute("class");
			if(temp!=""){
				string[] temp1=temp.Split(new char[]{' ','\t','\n'});
				for(int i=0;i<temp1.Length;i++){
					//r+=this.AttrCLASS2HTML(temp1[i]);
				}
			}
			//-----
			//   詳しい内容
			//-----
			string r2="";
			// Attribute : name
			temp=elem.InnerText;
			if(temp!="")r2+="<p style=\"font-family:'Microsoft Sans Serif';\">文: "+temp+"</p>";
			// Attribute : ja
			temp=elem.GetAttribute("ja");
			if(temp!="")r2+="<p style=\"font-family:'Microsoft Sans Serif';\">意味: "+temp+"</p>";
			// Attribute : desc
			temp=elem.GetAttribute("desc");
			if(temp!="")r2+="<p style=\"font-family:'Microsoft Sans Serif';\">説明: "+temp+"</p>";
			//-----
			//   
			//-----
			if(r2!="")r+="<div style=\"border:1px solid gray;\">"+r2+"</div>";
			return r;
		}
		
		//===== ここまで
	}
	public class XmlPart{
		//===== ここから
		/// <summary>
		/// **CAUTION** 削除予定。GetElem(string className).FullHTML に移行。
		/// </summary>
		public string Class2HTML(string className){
			string[] arr=className.Split(new char[]{'.'});
			if(arr.Length<1)return "";
			System.Xml.XmlNodeList list=this.xdoc.DocumentElement.ChildNodes;
			string r="";
			int j;int listlen;
			System.Xml.XmlElement elem;
			// それぞれの階層の要素に対して結果を出力
			for(int i=0;i<arr.Length;i++){
				listlen=list.Count;
				for(j=0;j<listlen;j++){
					if(list[j].NodeType!=System.Xml.XmlNodeType.Element)continue;
					elem=(System.Xml.XmlElement)list[j];
					if(elem.GetAttribute("name")==arr[i]){
						r+=((XmlPart.Elem)elem).ToClassHTML();
						list=elem.ChildNodes;
						break;
					}
				}
				if(j==listlen)r+="<c:r>&gt;&gt;</c:r> ["+arr[i]+"] 不明 ";
			}
			if(r!="")r="<p class=\"class\">"+r+"</p>";
			return r;
		}
		/// <summary>
		/// 一致する最後の xml 要素に対して Elem のインスタンスを返します。
		/// </summary>
		/// <returns>
		/// 一致する物がない場合でも最後に一致した物を返す。
		/// 一致する物が一つもない場合や className に空白が指定された場合はは null を返す
		/// </returns>
		public Hanyu.XmlPart.Elem GetElem(string className){
			string[] arr=className.Split(new char[]{'.'});
			if(arr.Length<1)return null;
			System.Xml.XmlNodeList list=this.xdoc.DocumentElement.ChildNodes;
			Hanyu.XmlPart.Elem r=null;
			int j;int listlen;
			System.Xml.XmlElement elem;
			for(int i=0;i<arr.Length;i++){
				listlen=list.Count;
				for(j=0;j<listlen;j++){
					if(list[j].NodeType!=System.Xml.XmlNodeType.Element)continue;
					elem=(System.Xml.XmlElement)list[j];
					if(elem.GetAttribute("name")==arr[i]){
						r=(XmlPart.Elem)elem;
						list=elem.ChildNodes;
						break;
					}
				}
			}
			return r;
		}
		/// <summary>
		/// **CAUTION** 削除予定。GetElem(string className).IconHTML に移行。
		/// </summary>
		public string PartName2HTML(string partName){
			if(partName=="")return "";
			System.Xml.XmlNodeList list=this.xdoc.DocumentElement.ChildNodes;
			System.Xml.XmlElement elem;
			string icopath;
			for(int i=0;i<list.Count;i++){
				if(list[i].NodeType!=System.Xml.XmlNodeType.Element)continue;
				elem=(System.Xml.XmlElement)list[i];
				if(elem.GetAttribute("name")!=partName)continue;
				icopath=elem.GetAttribute("ico");
				if(icopath=="")return "["+elem.GetAttribute("ja")+"]";
				return "<img class=\"ico12\" src=\""+System.IO.Path.Combine(this.dirpath,icopath)+"\"/>";
			}
			return "["+partName+"]";
		}
		//===== ここまで
	}
}