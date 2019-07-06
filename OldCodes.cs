//
//	��	���̃t�@�C���̓��e�̓R���p�C������܂���B
//		�g�p���Ȃ��Ȃ������W�b�N���A�u�܂��K�v�ɂȂ邩���m��Ȃ��v�Ƃ������R�łƂ��Ă����ꏊ�ł��B
//
namespace Hanyu{
	public class Words1{
		//===== ��������
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
		/// �w�肵���v�f�� word �q�v�f�� ListViewItem �ɕϊ����Ĕz��Ƃ��ĕԂ��܂��B
		/// </summary>
		/// <param name="elem">�w�肷��v�f(word ���q�v�f�Ɏ���)</param>
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
		//===== �����܂�
		public class WordGroup{
			public System.Windows.Forms.ListViewItem[] LVIWordContent(){
				//===== ��������
				//�܂��������o��
				System.Collections.ArrayList array=new System.Collections.ArrayList();
				System.Xml.XmlElement child;
				for(int i=0;i<this.elem.ChildNodes.Count;i++){
					if(this.elem.ChildNodes[i].NodeType!=System.Xml.XmlNodeType.Element)continue;
					child=(System.Xml.XmlElement)this.elem.ChildNodes[i];
					if(child.Name.ToLower()!="word")continue;
					array.AddRange(((Hanyu.Word)child).GetContents());
				}
				//����� ListViewItem �ɕϊ����Ă���
				System.Collections.ArrayList r=new System.Collections.ArrayList();
				System.Windows.Forms.ListViewItem item;
				Hanyu.ElemWordContent content;
				for(int i=0;i<array.Count;i++){
					content=(Hanyu.ElemWordContent)array[i];
					item=new System.Windows.Forms.ListViewItem(new string[]{content.ParentElemWord.Name,content.ListViewText});
					item.UseItemStyleForSubItems=false;
					item.SubItems[0].Font=new System.Drawing.Font("SimSun",10);
					item.Tag=content;
					//TODO:content.Part �ɉ����� item �� imageIndex ��t����
					r.Add(item);
				}
				//===== �����܂�
				return (System.Windows.Forms.ListViewItem[])r.ToArray(typeof(System.Windows.Forms.ListViewItem));
			}
		}
	}
	public class Word{
		//===== ��������
		// <summary>
		/// ListViewItem[] ���쐬���邽�߂ɕK�v�ȁA�P������������(���ꂼ��́A�Ӗ��A�n��A�\���A�ᕶ)��Ԃ��B 
		/// </summary>
		/// <returns>
		/// System.Collections.ArrayList�B�o�^����Ă��镨�́AWord.Content�B
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
			// �i��
			string temp;
			switch(elem.Name.ToLower()){
					//TODO: �A�C�R����p��
				case "n":temp="[��]";break;
				case "ad":temp="[��]";break;
				case "a":temp="[�`]";break;
				case "v":temp="[��]";break;
				case "inter":temp="[��]";break;
				case "prp":temp="[��]";break;
				case "particle":temp="[��]";break;
				case "cnj":temp="[��]";break;
				case "num":temp="[��]";break;
				default:
					return "<p>Words1::N2HTML<br/>�w�肵���v�f�́A�Ή����Ă���v�f�ł͂���܂���</p>";
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
			//   �ڂ������e
			//-----
			string r2="";
			// Attribute : desc
			temp=elem.GetAttribute("desc");
			if(temp!="")r2+="<p style=\"font-family:'Microsoft Sans Serif';\">����: "+temp+"</p>";
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
			string r="<h2 style\"font-family:SimSun;\">[��]"+elem.GetAttribute("ja")+"</h2>";
			// Attribute : class
			string temp=elem.GetAttribute("class");
			if(temp!=""){
				string[] temp1=temp.Split(new char[]{' ','\t','\n'});
				for(int i=0;i<temp1.Length;i++){
					r+=this.AttrCLASS2HTML(temp1[i]);
				}
			}
			//-----
			//   �ڂ������e
			//-----
			string r2="";
			// Attribute : desc
			temp=elem.GetAttribute("desc");
			if(temp!="")r2+="<p style=\"font-family:'Microsoft Sans Serif';\">����: "+temp+"</p>";
			// Attribute : target
			temp=elem.GetAttribute("target");
			if(temp!="")r2+="<p style=\"font-family:'Microsoft Sans Serif';\">�K�p: "+temp+"</p>";
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
			// �i��
			string temp;
			switch(elem.Name.ToLower()){
				//TODO: �A�C�R����p��
				case "id":temp="[�n]";break;
				case "const":temp="[�\]";break;
				default:
					return "<p>Words1::N2HTML\n�w�肵���v�f�́A�Ή����Ă���v�f�ł͂���܂���</p>";
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
			//   �ڂ������e
			//-----
			string r2="";
			// Attribute : ja
			temp=elem.GetAttribute("ja");
			if(temp!="")r2+="<p style=\"font-family:'Microsoft Sans Serif';\">�Ӗ�: "+temp+"</p>";
			// Attribute : desc
			temp=elem.GetAttribute("desc");
			if(temp!="")r2+="<p style=\"font-family:'Microsoft Sans Serif';\">����: "+temp+"</p>";
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
			//TODO:�A�C�R��������B������t����B
			return "<p>"+className+"</p>";
		}
		private string Exm2HTML(System.Xml.XmlElement elem){
			string r="<h2 style\"font-family:SimSun;\">[��]</h2>";
			// Attribute : class
			string temp=elem.GetAttribute("class");
			if(temp!=""){
				string[] temp1=temp.Split(new char[]{' ','\t','\n'});
				for(int i=0;i<temp1.Length;i++){
					//r+=this.AttrCLASS2HTML(temp1[i]);
				}
			}
			//-----
			//   �ڂ������e
			//-----
			string r2="";
			// Attribute : name
			temp=elem.InnerText;
			if(temp!="")r2+="<p style=\"font-family:'Microsoft Sans Serif';\">��: "+temp+"</p>";
			// Attribute : ja
			temp=elem.GetAttribute("ja");
			if(temp!="")r2+="<p style=\"font-family:'Microsoft Sans Serif';\">�Ӗ�: "+temp+"</p>";
			// Attribute : desc
			temp=elem.GetAttribute("desc");
			if(temp!="")r2+="<p style=\"font-family:'Microsoft Sans Serif';\">����: "+temp+"</p>";
			//-----
			//   
			//-----
			if(r2!="")r+="<div style=\"border:1px solid gray;\">"+r2+"</div>";
			return r;
		}
		
		//===== �����܂�
	}
	public class XmlPart{
		//===== ��������
		/// <summary>
		/// **CAUTION** �폜�\��BGetElem(string className).FullHTML �Ɉڍs�B
		/// </summary>
		public string Class2HTML(string className){
			string[] arr=className.Split(new char[]{'.'});
			if(arr.Length<1)return "";
			System.Xml.XmlNodeList list=this.xdoc.DocumentElement.ChildNodes;
			string r="";
			int j;int listlen;
			System.Xml.XmlElement elem;
			// ���ꂼ��̊K�w�̗v�f�ɑ΂��Č��ʂ��o��
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
				if(j==listlen)r+="<c:r>&gt;&gt;</c:r> ["+arr[i]+"] �s�� ";
			}
			if(r!="")r="<p class=\"class\">"+r+"</p>";
			return r;
		}
		/// <summary>
		/// ��v����Ō�� xml �v�f�ɑ΂��� Elem �̃C���X�^���X��Ԃ��܂��B
		/// </summary>
		/// <returns>
		/// ��v���镨���Ȃ��ꍇ�ł��Ō�Ɉ�v��������Ԃ��B
		/// ��v���镨������Ȃ��ꍇ�� className �ɋ󔒂��w�肳�ꂽ�ꍇ�͂� null ��Ԃ�
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
		/// **CAUTION** �폜�\��BGetElem(string className).IconHTML �Ɉڍs�B
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
		//===== �����܂�
	}
}