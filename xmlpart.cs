namespace Hanyu{
	public class XmlPart{
		private string path;
		public string Path{
			get{return this.path;}
			set{
				this.path=value;
				this.xdoc.DocumentElement.SetAttribute("icobase",System.IO.Path.GetDirectoryName(value));
			}
		}
		public string dirpath{
			get{return this.xdoc.DocumentElement.GetAttribute("icobase");}
		}
		private System.Xml.XmlDocument xdoc;
		public System.Windows.Forms.ImageList images;
		private System.Collections.Hashtable imageindexes;
		//Constructor
		public XmlPart(string path){
			this.xdoc=new System.Xml.XmlDocument();
			this.xdoc.Load(path);
			this.Path=path;
			// images
			this.images=new System.Windows.Forms.ImageList();
			this.images.ColorDepth=System.Windows.Forms.ColorDepth.Depth24Bit;
			this.images.ImageSize=new System.Drawing.Size(12,12);
			System.Drawing.Bitmap bmp;
			try{bmp=new System.Drawing.Bitmap(System.IO.Path.Combine(this.dirpath,this.xdoc.DocumentElement.GetAttribute("ico")));}
			catch{bmp=new System.Drawing.Bitmap(12,12);}
			this.images.Images.Add(bmp);
			this.imageindexes=new System.Collections.Hashtable();
			//hshElems
			this.hshElems=new System.Collections.Hashtable();
			this.hshParts=new System.Collections.Hashtable();
		}

		#region ToTreeNodes
		public System.Windows.Forms.TreeNode[] ToTreeNodes(){
			System.Collections.ArrayList r=new System.Collections.ArrayList();
			System.Xml.XmlNodeList list=this.xdoc.DocumentElement.ChildNodes;
			for(int i=0;i<list.Count;i++){
				if(list[i].NodeType!=System.Xml.XmlNodeType.Element)continue;
				r.Add(this.PartType2Node((System.Xml.XmlElement)list[i]));
			}
			return (System.Windows.Forms.TreeNode[])r.ToArray(typeof(System.Windows.Forms.TreeNode));
		}
		/// <summary>
		/// 指定した XML 要素から TreeNode を作成します。
		/// </summary>
		/// <param name="elem">XML 要素(&lt;part/&gt; または &lt;type/&gt;)</param>
		/// <returns>TreeNode を返します</returns>
		private System.Windows.Forms.TreeNode PartType2Node(System.Xml.XmlElement elem){
			int num=(elem.Name=="part")?1:(elem.Name=="type")?2:0;
			System.Windows.Forms.TreeNode r=new System.Windows.Forms.TreeNode(elem.GetAttribute("ja"),num,num);
			System.Xml.XmlNodeList list=elem.ChildNodes;
			for(int i=0;i<list.Count;i++){
				if(list[i].NodeType!=System.Xml.XmlNodeType.Element)continue;
				r.Nodes.Add(this.PartType2Node((System.Xml.XmlElement)list[i]));
			}
			r.Tag=elem;
			return r;
		}
		#endregion

		public int GetImageIndexOf(string pathName){
			if(pathName=="")return 0;
			if(!this.imageindexes.ContainsKey(pathName)){
				int r=this.images.Images.Count;
				try{this.images.Images.Add(new System.Drawing.Bitmap(pathName));}catch{return 0;}
				this.imageindexes.Add(pathName,r);
				return r;
			}
			return (int)this.imageindexes[pathName];
		}
		public string GetIconPath(string className){
			Hanyu.XmlPart.Elem elem=this.GetElem(className);
			if(elem==null)return "";
			return elem.IcoPath;
		}

		#region Hanyu.XmlPart.Elem の検索
		private System.Collections.Hashtable hshElems;
		private System.Collections.Hashtable hshParts;
		public Hanyu.XmlPart.Elem GetElem(string path){
			if(path=="")return null;
			/*hash*/if(this.hshElems.ContainsKey(path))return (Hanyu.XmlPart.Elem)this.hshElems[path];//*/
			string[] path2=path.Split(new char[]{'.'},2);
			Hanyu.XmlPart.Elem elem=(path2.Length==1)?this.GetChild(path2[0]):this.GetChild(path2[0]).GetElem(path2[1]);
			/*hash*/this.hshElems.Add(path,elem);//*/
			return elem;
		}
		private Hanyu.XmlPart.Elem GetChild(string name){
			System.Xml.XmlNodeList list=this.xdoc.DocumentElement.ChildNodes;
			int iM=list.Count;
			System.Xml.XmlElement elem1;
			Hanyu.XmlPart.Elem elem2;
			for(int i=0;i<iM;i++){
				if(list[i].NodeType!=System.Xml.XmlNodeType.Element)continue;
				elem1=(System.Xml.XmlElement)list[i];
				if(elem1.GetAttribute("name")!=name)continue;
				elem2=(Hanyu.XmlPart.Elem)elem1;
				if(elem2==null)continue;
				return elem2;
			}
			return new Hanyu.XmlPart.ElemNull(name);
		}
		/// <summary>
		/// 品詞の名前から、Hanyu.XmlPart.Elem を返します。
		/// </summary>
		/// <param name="partName">品詞識別子を指定します。例: n,v,a,ad,q,...。</param>
		/// <returns>
		/// 通常の場合、トップレベルの Hanyu.XmlPart.ElemPart のインスタンスを返します。
		/// partName="" の時は null を返します。
		/// 指定した品詞が存在しない場合は Hanyu.XmlPart.ElemNull のインスタンスを返します。
		/// </returns>
		public Hanyu.XmlPart.Elem GetElemPart(string partName){
			//--partName 引数処理
			if(partName=="")return null;
			string[] partName2=partName.Split(new char[]{'.'},2);
			if(partName2.Length>1)partName=partName2[0];
			//--検索
			/*hash*/if(this.hshParts.ContainsKey(partName))return (Hanyu.XmlPart.Elem)this.hshParts[partName];//*/
			System.Xml.XmlNodeList list=this.xdoc.DocumentElement.ChildNodes;
			System.Xml.XmlElement elem;
			Hanyu.XmlPart.Elem elem2;
			for(int i=0;i<list.Count;i++){
				if(list[i].NodeType!=System.Xml.XmlNodeType.Element)continue;
				elem=(System.Xml.XmlElement)list[i];
				if(elem.Name.ToLower()!="part"||elem.GetAttribute("name")!=partName)continue;
				elem2=(Hanyu.XmlPart.ElemPart)elem;
				/*hash*/this.hshParts.Add(partName,elem2);//*/
				return elem2;
			}
			elem2=new Hanyu.XmlPart.ElemNull(partName);
			/*hash*/this.hshParts.Add(partName,elem2);//*/
			return elem2;
		}
		#endregion

		#region <class> Elem
		public abstract class Elem{
			protected System.Xml.XmlElement elem;
			public Elem(System.Xml.XmlElement elem){
				this.elem=elem;
			}
			public virtual string Name{
				get{return this.elem.GetAttribute("name");}
				set{this.elem.SetAttribute("name",value);}
			}
			public virtual Hanyu.XmlPart.Elem Parent{
				get{return (Hanyu.XmlPart.Elem)(System.Xml.XmlElement)this.elem.ParentNode;}
			}

			public static explicit operator Elem(System.Xml.XmlElement e){
				if(e==null)return null;
				if(e.Name=="part")return new XmlPart.ElemPart(e);
				if(e.Name=="type")return new XmlPart.ElemType(e);
				return null;
			}

			#region HTML への内容の出力
			public abstract string ToClassHTML();
			public string FullHTML{
				get{
					string r=this.ToFullHTML();
					if(r=="")return r;
					return (r=="")?r:"<p class=\"class\">"+r+"</p>";
				}
			}
			internal virtual string ToFullHTML(){
				return (this.Parent==null)?this.ToClassHTML():this.Parent.ToFullHTML()+this.ToClassHTML();
			}
			protected virtual string htmlDesc{get{return Hanyu.statics.EscapeHTML(this.elem.GetAttribute("desc"));}}
			public string IcoHTML{
				get{
					string icopath=this.IcoPath;
					return (icopath=="")?"<span title=\""+this.htmlDesc+"\">["+this.Name+"]</span>"
						:"<img class=\"ico12\" alt=\""+this.htmlDesc+"\" src=\""+icopath+"\"/>";
				}
			}
			internal virtual string IcoPath{
				get{
					string icopath=this.elem.GetAttribute("ico");
					string debug=this.elem.OwnerDocument.DocumentElement.GetAttribute("icobase");
					if(icopath=="")return "";
					return System.IO.Path.Combine(
						this.elem.OwnerDocument.DocumentElement.GetAttribute("icobase")
						,icopath
					);
				}
			}
			#endregion

			#region n.prop.orgn.schl 等の表現との相互変換
			/// <summary>
			/// Elem オブジェクトの指定するクラスをフルパスで返します。例えば、"n.prop.orgn.schl"
			/// </summary>
			public static explicit operator string(XmlPart.Elem elem){
				string r=elem.Name;
				System.Xml.XmlElement e=elem.elem;
				while(e.ParentNode!=null&&e.ParentNode.NodeType==System.Xml.XmlNodeType.Element&&e.ParentNode.Name!="wordtype"){
					e=(System.Xml.XmlElement)e.ParentNode;
					r=e.GetAttribute("name")+"."+r;
				}
				return r;
			}
			public Hanyu.XmlPart.Elem GetElem(string path){
				if(path=="")return this;
				string[] path2=path.Split(new char[]{'.'},2);
				return (path2.Length==1)?this.GetChild(path2[0]):this.GetChild(path2[0]).GetElem(path2[1]);
			}
			public virtual Hanyu.XmlPart.Elem GetChild(string name){
				int iM=this.elem.ChildNodes.Count;
				System.Xml.XmlElement elem1;
				Hanyu.XmlPart.Elem elem2;
				for(int i=0;i<iM;i++){
					if(this.elem.ChildNodes[i].NodeType!=System.Xml.XmlNodeType.Element)continue;
					elem1=(System.Xml.XmlElement)this.elem.ChildNodes[i];
					if(elem1.GetAttribute("name")!=name)continue;
					elem2=(Hanyu.XmlPart.Elem)elem1;
					if(elem2==null)continue;
					return elem2;
				}
				return new Hanyu.XmlPart.ElemNull(name,this);
			}
			#endregion
		}
		public class ElemPart:XmlPart.Elem{
			internal ElemPart(System.Xml.XmlElement elem):base(elem){}
			public override string ToClassHTML(){return "";}
			internal override string ToFullHTML(){return "";}
			public static explicit operator ElemPart(System.Xml.XmlElement e){return new XmlPart.ElemPart(e);}
		}
		public class ElemType:XmlPart.Elem{
			internal ElemType(System.Xml.XmlElement elem):base(elem){}
			public override string ToClassHTML(){
				return "<c:r>&gt;&gt;</c:r> "+this.IcoHTML+" "+elem.GetAttribute("ja")+" ";
			}
			public static explicit operator ElemType(System.Xml.XmlElement e){return new XmlPart.ElemType(e);}
		}
		public class ElemNull:XmlPart.Elem{
			private string name;
			private Hanyu.XmlPart.Elem parent;
			internal ElemNull(string name):base(null){
				this.name=name;
				//this.parent=null;
			}
			internal ElemNull(string name,System.Xml.XmlElement parent):this(name,(Hanyu.XmlPart.Elem)parent){}
			internal ElemNull(string name,Hanyu.XmlPart.Elem parent):base(null){
				this.name=name;
				this.parent=parent;
			}
			public override string Name{
				get{return this.name;}
				set{this.name=value;}
			}
			public override Hanyu.XmlPart.Elem Parent{
				get{return this.parent;}
			}

			#region HTML への内容の出力
			public override string ToClassHTML(){
				return "<c:r>&gt;&gt;</c:r> ["+this.name+"] 不明 ";
			}
			internal override string ToFullHTML(){
				return (this.parent!=null)?
					this.Parent.ToFullHTML()+" "+this.ToClassHTML()
					:this.ToClassHTML();
			}
			protected override string htmlDesc{
				get{return "登録されていない品詞分類です。修正または登録を行って下さい。";}
			}
			internal override string IcoPath{get{return "";}}
			#endregion

			public override Hanyu.XmlPart.Elem GetChild(string name){
				return new Hanyu.XmlPart.ElemNull(name,this);
			}
		}
		#endregion </class>
	}
}