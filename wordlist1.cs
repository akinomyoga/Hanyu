namespace Hanyu{
	public class Words1{
		public string path;
		private System.Xml.XmlDocument xdoc;
		public Words1(string path){
			this.path=path;
			this.xdoc=new System.Xml.XmlDocument();
			this.xdoc.Load(path);
		}
		public System.Windows.Forms.TreeNode ToTreeNode(){
			return (System.Windows.Forms.TreeNode)(Hanyu.Words1.WordGroup)this.xdoc.DocumentElement;
		}
		public static explicit operator System.Windows.Forms.TreeNode(Hanyu.Words1 words){
			return (System.Windows.Forms.TreeNode)(Hanyu.Words1.WordGroup)words.xdoc.DocumentElement;
		}
		public void Save(){
			this.xdoc.Save(this.path);
		}
		public void SaveAs(string path){
			this.path=path;
			this.xdoc.Save(path);
		}

		#region <class> WordGroup
		public class WordGroup{
			private System.Xml.XmlElement elem;
			public WordGroup(System.Xml.XmlElement elem){
				this.elem=elem;
			}
			//----------------------------------------------
			//		Properties
			//----------------------------------------------
			public string Title{
				get{return this.elem.GetAttribute("title");}
				set{this.elem.SetAttribute("title",value);}
			}
			public Hanyu.Words1.WordGroup.GroupType Type{
				get{
					switch(this.elem.Name.ToLower()){
						case "library":return Hanyu.Words1.WordGroup.GroupType.Library;
						case "shelf":return Hanyu.Words1.WordGroup.GroupType.Shelf;
						case "wordbook":return Hanyu.Words1.WordGroup.GroupType.WordBook;
						case "chapter":return Hanyu.Words1.WordGroup.GroupType.Chapter;
						case "page":return Hanyu.Words1.WordGroup.GroupType.Page;
						case "group":return Hanyu.Words1.WordGroup.GroupType.Group;
						default:return Hanyu.Words1.WordGroup.GroupType.NotGroup;
					}
				}
			}
			public enum GroupType{Library,Shelf,WordBook,Chapter,Page,Group,NotGroup}
			public string Number{
				get{return this.elem.GetAttribute("number");}
				set{this.elem.SetAttribute("number",value);}
			}
			/// <summary>
			/// 保持している単語の一覧を ListViewItem の配列として返す。
			/// </summary>
			public System.Windows.Forms.ListViewItem[] LVIWord(){
				System.Collections.ArrayList r=new System.Collections.ArrayList();
				System.Xml.XmlElement child;
				for(int i=0;i<this.elem.ChildNodes.Count;i++){
					if(this.elem.ChildNodes[i].NodeType!=System.Xml.XmlNodeType.Element)continue;
					child=(System.Xml.XmlElement)this.elem.ChildNodes[i];
					if(child.Name.ToLower()!="word")continue;
					r.Add((System.Windows.Forms.ListViewItem)(Hanyu.Word)child);
				}
				return (System.Windows.Forms.ListViewItem[])r.ToArray(typeof(System.Windows.Forms.ListViewItem));
			}
			/// <summary>
			/// 保持している単語の中身の情報を、ListViewItem の配列として返す。
			/// </summary>
			public System.Windows.Forms.ListViewItem[] LVIWordContent(Hanyu.XmlPart xmlpart){
				System.Collections.ArrayList r=new System.Collections.ArrayList();
				System.Xml.XmlElement child;
				for(int i=0;i<this.elem.ChildNodes.Count;i++){
					if(this.elem.ChildNodes[i].NodeType!=System.Xml.XmlNodeType.Element)continue;
					child=(System.Xml.XmlElement)this.elem.ChildNodes[i];
					if(child.Name.ToLower()!="word")continue;
					r.AddRange(((Hanyu.Word)child).LVIContents());
				}
				//TODO:それぞれの content.Part に応じて item に imageIndex を付ける
				System.Windows.Forms.ListViewItem item;
				Hanyu.Word.Content content;
				for(int i=0;i<r.Count;i++){
					item=(System.Windows.Forms.ListViewItem)r[i];
					content=(Hanyu.Word.Content)item.Tag;
					item.ImageIndex=xmlpart.GetImageIndexOf(xmlpart.GetIconPath(content.Part));
				}
				return (System.Windows.Forms.ListViewItem[])r.ToArray(typeof(System.Windows.Forms.ListViewItem));
			}
			/// <summary>
			/// WordGroup の持っている単語のグループ分けの構造を TreeNode に変換する。
			/// </summary>
			public static explicit operator System.Windows.Forms.TreeNode(Hanyu.Words1.WordGroup wg){
				string title=wg.Number+" "+wg.Title;
				int type=(int)wg.Type;
				System.Windows.Forms.TreeNode r=new System.Windows.Forms.TreeNode(title.Trim(),type,type);
				r.Tag=wg;
				if(type==6||type==5)return r;
				//子グループを処理
				int chtype;
				Hanyu.Words1.WordGroup child;
				for(int i=0;i<wg.elem.ChildNodes.Count;i++){
					if(wg.elem.ChildNodes[i].NodeType!=System.Xml.XmlNodeType.Element)continue;
					child=(Hanyu.Words1.WordGroup)(System.Xml.XmlElement)wg.elem.ChildNodes[i];
					chtype=(int)child.Type;
					if(chtype!=6&&Hanyu.Words1.WordGroup.ParentChild[type,chtype]){
						r.Nodes.Add((System.Windows.Forms.TreeNode)child);
					}
				}
				return r;
			}
			private static bool[,] ParentChild={
				{false,true,true,false,false,false}//library
				,{false,false,true,false,false,false}//shelf
				,{false,false,false,true,true,true}//wordbook
				,{false,false,false,false,true,true}//chapter
				,{false,false,false,false,false,true}//page
			};
			/// <summary>
			/// XML 要素から WordGroup に変換する
			/// </summary>
			public static explicit operator Hanyu.Words1.WordGroup(System.Xml.XmlElement elem){
				return new Hanyu.Words1.WordGroup(elem);
			}
		}
		#endregion
	}

	public class Word{
		System.Xml.XmlElement elem;
		System.Collections.ArrayList contents=new System.Collections.ArrayList();
		public Word(System.Xml.XmlElement elem){
			if(elem.Name.ToLower()!="word")throw new System.Exception("指定した XML 要素は word ではありません");
			this.elem=elem;
			this.initContents();
		}
		private void initContents(){
			this.contents.Clear();
			Hanyu.Word.Content content;
			for(int i=0;i<this.elem.ChildNodes.Count;i++){
				if(this.elem.ChildNodes[i].NodeType!=System.Xml.XmlNodeType.Element)continue;
				content=(Hanyu.Word.Content)(System.Xml.XmlElement)this.elem.ChildNodes[i];
				if(content!=null)this.contents.Add(content);
			}
		}
		public static explicit operator Word(System.Xml.XmlElement elem){return new Word(elem);}

		//-----PROPERTIES
		public string Pinyin{
			get{return Hanyu.Pinyin.Trans(this.elem.GetAttribute("pinyin"));}
		}
		public string Name{
			get{return this.elem.GetAttribute("name");}
		}
		public string htmlTitle{
			get{
				return "<h1 lang=\"zh\" class=\"zh\">"+this.Name+"</h1><p>発音: <span class=\"pinyin\" lang=\"en\">"+this.Pinyin+"</span></p>";
			}
		}

		#region User Interfaces
		public static explicit operator System.Windows.Forms.ListViewItem(Hanyu.Word e){
			System.Windows.Forms.ListViewItem item
				=new System.Windows.Forms.ListViewItem(new string[]{e.Name,e.Pinyin},6);
			item.SubItems[0].Font=new System.Drawing.Font("SimSun",10);
			item.SubItems[1].Font=new System.Drawing.Font("Microsoft Sans Serif",8);
			item.SubItems[1].ForeColor=System.Drawing.Color.BlueViolet;
			item.UseItemStyleForSubItems=false;
			item.Tag=e;
			return item;
		}
		public string ToHTML(Hanyu.XmlPart xmlpart){
			/*System.Xml.XmlElement child;
			Hanyu.Word.Content content;
			string r=this.htmlTitle+"<hr/>";
			for(int i=0;i<this.elem.ChildNodes.Count;i++){
				if(this.elem.ChildNodes[i].NodeType!=System.Xml.XmlNodeType.Element)continue;
				child=(System.Xml.XmlElement)this.elem.ChildNodes[i];
				content=(Hanyu.Word.Content)child;
				if(content!=null){
					r+=content.ToHTML(xmlpart);
					if(child.Name.ToLower()!="exm")r+="<hr size=\"1px\"/>";
				}
			}
			return r;
			/*/
			string r=this.htmlTitle+"<hr/>";
			Hanyu.Word.Content content;
			for(int i=0;i<this.contents.Count;i++){
				content=(Hanyu.Word.Content)this.contents[i];
				r+=content.ToHTML(xmlpart);
				if(content.GetType()!=typeof(Hanyu.Word.Example))r+="<hr size=\"1px\"/>";
			}
			return r;
			//*/
		}
		public System.Collections.ArrayList LVIContents(){
			System.Collections.ArrayList array=new System.Collections.ArrayList();
			System.Windows.Forms.ListViewItem item;
			/*for(int i=0;i<this.elem.ChildNodes.Count;i++){
				if(this.elem.ChildNodes[i].NodeType==System.Xml.XmlNodeType.Element){
					item=(System.Windows.Forms.ListViewItem)(Hanyu.Word.Content)(System.Xml.XmlElement)this.elem.ChildNodes[i];
					item.SubItems[0].Text=this.Name;
					array.Add(item);
				}
			}/*/
			for(int i=0;i<this.contents.Count;i++){
				item=(System.Windows.Forms.ListViewItem)(Hanyu.Word.Content)this.contents[i];
				item.SubItems[0].Text=this.Name;
				array.Add(item);
			}//*/
			return array;
		}
		#endregion

		#region Content
		[System.Flags()]
		public enum ContentType{Mean=0x1,Construct=0x2,Postfix=0x4,Prefix=0x8,Idiom=0x10,Const=0x1e,Example=0x20};
		//================================================================================
		//				class Word.Content
		//================================================================================
		public abstract class Content{
			protected System.Xml.XmlElement elem;
			protected Hanyu.Word.AttrClass attrClass;
			protected Content(System.Xml.XmlElement elem,string tagName){
				if(elem.Name.ToLower()!=tagName)throw new System.Exception("指定した XML 要素は "+tagName+" ではありません");
				this.elem=elem;
				this.attrClass=new Word.AttrClass(elem);
			}

			#region Attributes
			public string attrJa{
				get{return this.elem.GetAttribute("ja");}
				set{this.elem.SetAttribute("ja",value);}
			}
			public string attrEn{
				get{return this.elem.GetAttribute("en");}
				set{this.elem.SetAttribute("en",value);}
			}
			public string attrDesc{
				get{return this.elem.GetAttribute("desc");}
				set{this.elem.SetAttribute("desc",value);}
			}
			public string htmlDesc{
				get{
					string desc=this.elem.GetAttribute("desc");
					if(desc=="")return "";
					return "<p class=\"desc\"><c:g>▼</c:g>"+Hanyu.statics.EscapeHTML(desc)+"</p>";
				}
			}
			public string attrTarget{
				get{return this.elem.GetAttribute("target");}
				set{this.elem.SetAttribute("target",value);}
			}
			public string htmlTarget{
				get{
					string target=this.elem.GetAttribute("target");
					if(target=="")return "";
					return "<h3>適用</h3><p>"+target+"</p>";
				}
			}
			public string attrLevel{
				get{return this.elem.GetAttribute("level");}
				set{this.elem.SetAttribute("level",value);}
			}
			#endregion

			public Hanyu.Word.AttrClass Class{
				get{return this.attrClass;}
			}
			public string Part{
				get{return this.attrClass.Part;}
			}
			public abstract string ToHTML(XmlPart xmlpart);
			public abstract ContentType Type{get;}
			protected string htmlChilds(XmlPart xmlpart){
				string r="";
				System.Xml.XmlElement child;
				for(int i=0;i<this.elem.ChildNodes.Count;i++){
					if(this.elem.ChildNodes[i].NodeType!=System.Xml.XmlNodeType.Element)continue;
					child=(System.Xml.XmlElement)this.elem.ChildNodes[i];
					if(child.Name!="exm")continue;
					r+=new Hanyu.Word.Example(child).ToHTML(xmlpart);
				}
				return r;
			}
			public Hanyu.Word ParentElemWord{
				get{return new Hanyu.Word((System.Xml.XmlElement)this.elem.ParentNode);}
			}
			//--- インスタンスを作る
			public static explicit operator Hanyu.Word.Content(System.Xml.XmlElement elem){
				switch(elem.Name.ToLower()){
					case "mean":return new Hanyu.Word.Mean(elem);
					case "const":return new Hanyu.Word.Const(elem,Hanyu.Word.ContentType.Construct);
					case "id":return new Hanyu.Word.Const(elem,Hanyu.Word.ContentType.Idiom);
					case "prefix":return new Hanyu.Word.Const(elem,Hanyu.Word.ContentType.Prefix);
					case "postfix":return new Hanyu.Word.Const(elem,Hanyu.Word.ContentType.Postfix);
					case "exm":return new Hanyu.Word.Example(elem);
					default:return null;
				}
			}
			public abstract System.Windows.Forms.ListViewItem.ListViewSubItem ListViewSubItem{get;}
			public static explicit operator System.Windows.Forms.ListViewItem(Hanyu.Word.Content content){
				System.Windows.Forms.ListViewItem item=new System.Windows.Forms.ListViewItem("");
				item.SubItems.Add(content.ListViewSubItem);
				item.UseItemStyleForSubItems=false;
				item.SubItems[0].Font=new System.Drawing.Font("SimSun",10);
				item.Tag=content;
				return item;
			}
		}
		public class Mean:Hanyu.Word.Content{
			public Mean(System.Xml.XmlElement elem):base(elem,"mean"){}
			public override string ToHTML(XmlPart xmlpart){
				string r="<h2 class=\"mean\">"
					+((this.Part=="")?"[?]":xmlpart.GetElemPart(this.Part).IcoHTML)
					+" "+Hanyu.statics.EscapeHTML(this.attrJa)+"</h2>";
				r+=this.attrClass.ToHTML(xmlpart);
				r+=this.htmlDesc;
				//TODO:level
				//TODO:en
				//TODO:quant(名詞の場合)
				r+=this.htmlChilds(xmlpart);
				return r;
			}
			public override System.Windows.Forms.ListViewItem.ListViewSubItem ListViewSubItem{
				get{
					System.Windows.Forms.ListViewItem.ListViewSubItem r=new System.Windows.Forms.ListViewItem.ListViewSubItem();
					string text=this.attrJa;
					if(text==""){
						r.Text=this.attrDesc;
						r.ForeColor=System.Drawing.Color.Green;
					}else{
						r.Text=text;
						r.ForeColor=System.Drawing.Color.Blue;
					}
					return r;
				}
			}
			public override ContentType Type{get{return ContentType.Mean;}}

		}
		public class Const:Hanyu.Word.Content{
			Word.ContentType type;
			//STATICS
			private static ContentType[] Types={ContentType.Construct,ContentType.Postfix,ContentType.Prefix,ContentType.Idiom};
			private static string[] TypeElem={"const","postfix","prefix","id"};
			private static string[] TypeName={"構文","接尾辞","接頭辞","熟語"};
			private static string[] TypeNameShort={"構","尾","頭","熟"};
			public static string ElementNameOfType(ContentType type){
				for(int i=0;i<Const.Types.Length;i++)if(type==Const.Types[i])return Const.TypeElem[i];
				return "";
			}
			public static string NameOfType(ContentType type){
				for(int i=0;i<Const.Types.Length;i++)if(type==Const.Types[i])return Const.TypeName[i];
				return "";
			}
			public static string ShortNameOfType(ContentType type){
				for(int i=0;i<Const.Types.Length;i++)if(type==Const.Types[i])return Const.TypeNameShort[i];
				return "";
			}
			//CONSTRUCTOR
			public Const(System.Xml.XmlElement elem,Word.ContentType type):base(elem,Word.Const.ElementNameOfType(type)){
				this.type=type;
			}
			//PROPERTY
			public string attrPinyin{
				get{return this.elem.GetAttribute("pinyin");}
				set{this.elem.SetAttribute("pinyin",value);}
			}
			public string htmlPinyin{
				get{
					string r=this.elem.GetAttribute("pinyin");
					return (r=="")?"":"[ <span class=\"pinyin\">"+Hanyu.Pinyin.Trans(r)+"</span> ]";
				}
			}
			public string attrName{
				get{return this.elem.GetAttribute("name");}
				set{this.elem.SetAttribute("name",value);}
			}
			public string htmlName{
				get{return "<span lang=\"zh\"class=\"zh\">"+Hanyu.statics.EscapeHTML(this.elem.GetAttribute("name"))+"</span>";}
			}
			//METHODS
			public override string ToHTML(XmlPart xmlpart){
				string r="<h2 class=\"mean\"><span class=\"itemtype\">"+Word.Const.NameOfType(this.type)+"</span>";
				if(this.Part!="")r+=xmlpart.GetElemPart(this.Part).IcoHTML+": ";
				if(this.type==Word.ContentType.Construct||this.type==Word.ContentType.Idiom){
					r+=" "+this.htmlName+" "+this.htmlPinyin;
				}
				r+="</h2>";
				r+="<p>"+this.elem.GetAttribute("ja")+"</p>";
				r+=this.attrClass.ToHTML(xmlpart);
				r+=this.htmlTarget;
				r+=this.htmlDesc;
				//TODO:level
				//TODO:en
				//TODO:quant(名詞の場合)
				r+=this.htmlChilds(xmlpart);
				return r;
			}
			public override System.Windows.Forms.ListViewItem.ListViewSubItem ListViewSubItem{
				get{
					System.Windows.Forms.ListViewItem.ListViewSubItem r=new System.Windows.Forms.ListViewItem.ListViewSubItem();
					r.Text=Hanyu.Word.Const.ShortNameOfType(this.type)+")";
					string text=this.attrName;
					if(text==""){
						r.Text+=this.attrDesc;
						r.ForeColor=System.Drawing.Color.Green;
					}else{
						r.Text+=text;
						r.Font=new System.Drawing.Font("SimSun",10);
					}
					return r;
				}
			}
			public override ContentType Type{get{return ContentType.Const;}}
		}
		public class Example:Hanyu.Word.Content{
			public Example(System.Xml.XmlElement elem):base(elem,"exm"){}
			public override string ToHTML(XmlPart xmlpart){
				string title="<b>例</b> ";
				if(this.Part!="")title+=xmlpart.GetElemPart(this.Part).IcoHTML;
				title+=" <span class=\"zh\">"+Hanyu.statics.EscapeHTML(this.elem.InnerText)+"</span>";
				string div="";
				div+=this.elem.GetAttribute("ja");
				div+=this.attrClass.ToHTML(xmlpart);
				div+=this.htmlDesc;
				//TODO:level
				//TODO:en
				if(div=="")return "<p class=\"exm\">"+title+"</p>";
				else return "<p class=\"exmt\">"+title+"</p><div class=\"exm\">"+div+"</div>";
			}
			public override System.Windows.Forms.ListViewItem.ListViewSubItem ListViewSubItem{
				get{
					System.Windows.Forms.ListViewItem.ListViewSubItem r=new System.Windows.Forms.ListViewItem.ListViewSubItem();
					r.Text="[例文] "+this.elem.InnerText;
					r.Font=new System.Drawing.Font("SimSun",10);
					return r;
				}
			}
			public override ContentType Type{get{return ContentType.Example;}}
		}
		public class AttrClass{
			System.Xml.XmlAttribute attr;
			System.Collections.ArrayList classes;
			public AttrClass(System.Xml.XmlElement elem){
				this.attr=elem.GetAttributeNode("class");
				if(this.attr==null){
					elem.SetAttribute("class","");
					this.attr=elem.GetAttributeNode("class");
				}
				this.classes=new System.Collections.ArrayList();
				string[] a=this.attr.Value.Split(new char[]{' '});
				for(int i=0;i<a.Length;i++)this.classes.Add(a[i]);
			}
			public string this[int i]{
				get{return (string)this.classes[i];}
			}
			/// <summary>
			/// 品詞を取得
			/// </summary>
			/// <returns>このクラスの品詞を取得</returns>
			public string Part{
				get{
					if(this.classes.Count==0)return "";
					return ((string)this.classes[0]).Split(new char[]{'.'})[0];
				}
			}
			/// <summary>
			/// クラスの追加
			/// </summary>
			/// <param name="className">追加するクラスを指定</param>
			public void Add(string className){
				string cls;
				for(int i=0;i<this.classes.Count;i++){
					cls=(string)this.classes[i];
					if(cls.StartsWith(className))return;
					if(className.StartsWith(cls)){
						this.classes[i]=className;
						this.attr.Value=this.ClassText();
						return;
					}
				}
			}
			/// <summary>
			/// クラスを削除
			/// </summary>
			/// <param name="className">削除するクラスを指定</param>
			public void Remove(string className){
				for(int i=0;i<this.classes.Count;i++){
					if(className==(string)this.classes[i]){
						this.classes.RemoveAt(i);
						this.attr.Value=this.ClassText();
						return;
					}
				}
			}
			//
			public string ClassText(){
				if(this.classes.Count==0)return "";
				string r=(string)this.classes[0];
				if(this.classes.Count>1)for(int i=1;i<this.classes.Count;i++){
					r+=" "+(string)this.classes[i];
				}
				return r;
			}
			public bool IsMember(string className){
				for(int i=0;i<this.classes.Count;i++){
					if(((string)this.classes[i]).StartsWith(className))return true;
				}
				return false;
			}
			public string ToHTML(XmlPart xmlpart){
				string r="";
				Hanyu.XmlPart.Elem elm;
				for(int i=0;i<this.classes.Count;i++){
					elm=xmlpart.GetElem((string)this.classes[i]);
					if(elm!=null)r+=elm.FullHTML;//Class2HTML((string)this.classes[i]);
				}
				return r;
			}
		}
		#endregion
	}
}