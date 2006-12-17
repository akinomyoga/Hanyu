using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace Hanyu{
	/// <summary>
	/// 中国語単語表示のメインウィンドウ
	/// </summary>
	public class frmMain : System.Windows.Forms.Form{
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.ListView listView1;
		private AxSHDocVw.AxWebBrowser Browser;
		private System.Windows.Forms.ColumnHeader clmName;
		private System.Windows.Forms.ColumnHeader clmPinyin;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Splitter splitter2;
		private System.Windows.Forms.Splitter splitter3;
		private System.Windows.Forms.TreeView treePart;
		private System.Windows.Forms.ImageList imagePart;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ToolBarButton toolbarWord;
		private System.Windows.Forms.ToolBarButton toolbarContent;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.ComponentModel.IContainer components;

		public frmMain(){
			//Hanyu.Pinyin.Initialize();
			InitializeComponent();
			//this.Browser.Navigate("about:blank");
			this.Browser.Navigate(@"C:\Documents and Settings\koichi\デスクトップ\College\Hanyu\WordView.htm");
			//test
			this.treePart.Nodes.AddRange(this.xmlpart.ToTreeNodes());
			System.Windows.Forms.TreeNode tn=this.words.ToTreeNode();
			for(int i=0;i<tn.Nodes.Count;i++)this.treeView1.Nodes.Add(tn.Nodes[i]);
		}

		/// <summary>
		/// 使用されているリソースに後処理を実行します。
		/// </summary>
		protected override void Dispose( bool disposing ){
			if( disposing ){
				if (components != null){
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows フォーム デザイナで生成されたコード 
		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent(){
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmMain));
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.listView1 = new System.Windows.Forms.ListView();
			this.clmName = new System.Windows.Forms.ColumnHeader();
			this.clmPinyin = new System.Windows.Forms.ColumnHeader();
			this.Browser = new AxSHDocVw.AxWebBrowser();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.treePart = new System.Windows.Forms.TreeView();
			this.imagePart = new System.Windows.Forms.ImageList(this.components);
			this.label1 = new System.Windows.Forms.Label();
			this.splitter2 = new System.Windows.Forms.Splitter();
			this.splitter3 = new System.Windows.Forms.Splitter();
			this.panel2 = new System.Windows.Forms.Panel();
			this.toolBar1 = new System.Windows.Forms.ToolBar();
			this.toolbarWord = new System.Windows.Forms.ToolBarButton();
			this.toolbarContent = new System.Windows.Forms.ToolBarButton();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			((System.ComponentModel.ISupportInitialize)(this.Browser)).BeginInit();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.AllowDrop = true;
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView1.HideSelection = false;
			this.treeView1.ImageList = this.imageList1;
			this.treeView1.Location = new System.Drawing.Point(0, 220);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(152, 317);
			this.treeView1.TabIndex = 0;
			this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
			this.treeView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeView1_DragEnter);
			this.treeView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView1_DragDrop);
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(12, 12);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.clmName,
																						this.clmPinyin});
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
			this.listView1.FullRowSelect = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(0, 28);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(184, 509);
			this.listView1.SmallImageList = this.imageList1;
			this.listView1.TabIndex = 2;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listView1_ItemDrag);
			this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
			// 
			// clmName
			// 
			this.clmName.Text = "単語";
			this.clmName.Width = 80;
			// 
			// clmPinyin
			// 
			this.clmPinyin.Text = "発音";
			this.clmPinyin.Width = 80;
			// 
			// Browser
			// 
			this.Browser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Browser.Enabled = true;
			this.Browser.Location = new System.Drawing.Point(342, 0);
			this.Browser.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("Browser.OcxState")));
			this.Browser.Size = new System.Drawing.Size(498, 537);
			this.Browser.TabIndex = 4;
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2,
																					  this.menuItem3});
			this.menuItem1.Text = "ファイル(&F)";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "開く(&O)";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.treeView1);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.splitter1);
			this.panel1.Controls.Add(this.treePart);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(152, 537);
			this.panel1.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.label2.Dock = System.Windows.Forms.DockStyle.Top;
			this.label2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
			this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.label2.Location = new System.Drawing.Point(0, 204);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(152, 16);
			this.label2.TabIndex = 4;
			this.label2.Text = "ファイル";
			// 
			// splitter1
			// 
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter1.Location = new System.Drawing.Point(0, 200);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(152, 4);
			this.splitter1.TabIndex = 3;
			this.splitter1.TabStop = false;
			// 
			// treePart
			// 
			this.treePart.AllowDrop = true;
			this.treePart.Dock = System.Windows.Forms.DockStyle.Top;
			this.treePart.ImageList = this.imagePart;
			this.treePart.ItemHeight = 14;
			this.treePart.Location = new System.Drawing.Point(0, 16);
			this.treePart.Name = "treePart";
			this.treePart.Size = new System.Drawing.Size(152, 184);
			this.treePart.TabIndex = 5;
			this.treePart.DragOver += new System.Windows.Forms.DragEventHandler(this.treePart_DragOver);
			this.treePart.DragEnter += new System.Windows.Forms.DragEventHandler(this.treePart_DragEnter);
			this.treePart.DragLeave += new System.EventHandler(this.treePart_DragLeave);
			this.treePart.DragDrop += new System.Windows.Forms.DragEventHandler(this.treePart_DragDrop);
			// 
			// imagePart
			// 
			this.imagePart.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.imagePart.ImageSize = new System.Drawing.Size(12, 12);
			this.imagePart.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imagePart.ImageStream")));
			this.imagePart.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(128)));
			this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(152, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "品詞・分類";
			// 
			// splitter2
			// 
			this.splitter2.Location = new System.Drawing.Point(152, 0);
			this.splitter2.Name = "splitter2";
			this.splitter2.Size = new System.Drawing.Size(3, 537);
			this.splitter2.TabIndex = 6;
			this.splitter2.TabStop = false;
			// 
			// splitter3
			// 
			this.splitter3.Location = new System.Drawing.Point(339, 0);
			this.splitter3.Name = "splitter3";
			this.splitter3.Size = new System.Drawing.Size(3, 537);
			this.splitter3.TabIndex = 7;
			this.splitter3.TabStop = false;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.listView1);
			this.panel2.Controls.Add(this.toolBar1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel2.Location = new System.Drawing.Point(155, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(184, 537);
			this.panel2.TabIndex = 8;
			// 
			// toolBar1
			// 
			this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																						this.toolbarWord,
																						this.toolbarContent});
			this.toolBar1.ButtonSize = new System.Drawing.Size(55, 22);
			this.toolBar1.DropDownArrows = true;
			this.toolBar1.ImageList = this.imagePart;
			this.toolBar1.Location = new System.Drawing.Point(0, 0);
			this.toolBar1.Name = "toolBar1";
			this.toolBar1.ShowToolTips = true;
			this.toolBar1.Size = new System.Drawing.Size(184, 28);
			this.toolBar1.TabIndex = 3;
			this.toolBar1.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
			this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
			// 
			// toolbarWord
			// 
			this.toolbarWord.ImageIndex = 0;
			this.toolbarWord.Pushed = true;
			this.toolbarWord.Text = "単語";
			// 
			// toolbarContent
			// 
			this.toolbarContent.ImageIndex = 1;
			this.toolbarContent.Text = "意味";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
			this.menuItem3.Text = "保存(&S)";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// frmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
			this.ClientSize = new System.Drawing.Size(840, 537);
			this.Controls.Add(this.Browser);
			this.Controls.Add(this.splitter3);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.splitter2);
			this.Controls.Add(this.panel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu1;
			this.Name = "frmMain";
			this.Text = "中文";
			((System.ComponentModel.ISupportInitialize)(this.Browser)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main(){
			Application.Run(new frmMain());
		}

		// General Fields
		Words1 words=new Words1(@"C:\Documents and Settings\koichi\デスクトップ\College\Hanyu\word4.xml");
		XmlPart xmlpart=new XmlPart(@"C:\Documents and Settings\koichi\デスクトップ\College\Hanyu\parts.xml");


		private void listView1_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e){
			this.listView1.DoDragDrop(this.listView1.SelectedItems,
				System.Windows.Forms.DragDropEffects.Move|System.Windows.Forms.DragDropEffects.Link);
		}

		private void treeView1_DragEnter(object sender, System.Windows.Forms.DragEventArgs e){
			if(e.Data.GetDataPresent(typeof(System.Windows.Forms.ListView.SelectedListViewItemCollection))){
				e.Effect=DragDropEffects.Move;
			}else e.Effect=DragDropEffects.None;
		}
		private void treeView1_DragDrop(object sender, System.Windows.Forms.DragEventArgs e){
			System.Drawing.Point pt=this.treeView1.PointToClient(new System.Drawing.Point(e.X,e.Y));
            System.Windows.Forms.TreeNode tn=this.treeView1.GetNodeAt(pt);	
		}

		#region treePart
		private System.Drawing.Font FontDefault=new System.Drawing.Font("MS UI Gothic",9,System.Drawing.FontStyle.Regular);
		private System.Drawing.Font FontSelected=new System.Drawing.Font("MS UI Gothic",9,System.Drawing.FontStyle.Underline);
		private System.Windows.Forms.TreeNode treePartDropTarget;
		private void treePart_DragEnter(object sender, System.Windows.Forms.DragEventArgs e){
			if(!e.Data.GetDataPresent(typeof(System.Windows.Forms.ListView.SelectedListViewItemCollection)))return;
			System.Windows.Forms.ListView.SelectedListViewItemCollection items
				=(System.Windows.Forms.ListView.SelectedListViewItemCollection)
					e.Data.GetData(typeof(System.Windows.Forms.ListView.SelectedListViewItemCollection));
			for(int i=0;i<items.Count;i++){
				System.Type type=items[i].Tag.GetType();
				if(type==typeof(Hanyu.Word)){
					//何もしない
				}else if(type.IsSubclassOf(typeof(Hanyu.Word.Content))){
					e.Effect=DragDropEffects.Link;
					return;
				}
			}
			e.Effect=DragDropEffects.None;
		}
		private void treePart_DragDrop(object sender, System.Windows.Forms.DragEventArgs e){
			//GetDropTarget
            System.Windows.Forms.TreeNode tn=this.treePart.GetNodeAt(
				this.treePart.PointToClient(new System.Drawing.Point(e.X,e.Y))
			);
			if(tn==null||tn.Tag==null||tn.Tag.GetType()!=typeof(System.Xml.XmlElement))return;
			string className=(string)(XmlPart.Elem)(System.Xml.XmlElement)tn.Tag;
			//GetDropObject
			if(!e.Data.GetDataPresent(typeof(System.Windows.Forms.ListView.SelectedListViewItemCollection)))return;
			System.Windows.Forms.ListView.SelectedListViewItemCollection items
				=(System.Windows.Forms.ListView.SelectedListViewItemCollection)
					e.Data.GetData(typeof(System.Windows.Forms.ListView.SelectedListViewItemCollection));
			//SetClass
			for(int i=0;i<items.Count;i++){
				if(!items[i].Tag.GetType().IsSubclassOf(typeof(Hanyu.Word.Content)))continue;
				((Hanyu.Word.Content)items[i].Tag).Class.Add(className);
			}
			//DropTarget を元に戻す
			if(this.treePartDropTarget==null)return;
			this.treePartDropTarget.NodeFont=this.FontDefault;
			this.treePartDropTarget=null;
		}
		private void treePart_DragOver(object sender, System.Windows.Forms.DragEventArgs e){
			System.Drawing.Point pt=this.treePart.PointToClient(new System.Drawing.Point(e.X,e.Y));
			System.Windows.Forms.TreeNode tn=this.treePart.GetNodeAt(pt);
			if(this.treePartDropTarget==null){
				tn.NodeFont=this.FontSelected;
				this.treePartDropTarget=tn;
			}else if(tn!=this.treePartDropTarget){
				this.treePartDropTarget.NodeFont=this.FontDefault;
				tn.NodeFont=this.FontSelected;
				this.treePartDropTarget=tn;
			}
		}
		private void treePart_DragLeave(object sender, System.EventArgs e){
			if(this.treePartDropTarget==null)return;
			this.treePartDropTarget.NodeFont=this.FontDefault;
			this.treePartDropTarget=null;
		}
		#endregion

		#region Showing Items in this.listView1
		private Hanyu.Words1.WordGroup currentWordGroup;
		private bool listWord=true;
		private void treeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e){
			if(e.Node.Tag==null||e.Node.Tag.GetType()!=typeof(Hanyu.Words1.WordGroup))return;
			this.CurrentWordGroup=(Hanyu.Words1.WordGroup)e.Node.Tag;
		}
		public Hanyu.Words1.WordGroup CurrentWordGroup{
			get{return this.CurrentWordGroup;}
			set{
				this.currentWordGroup=value;
				if(value==null)return;
				this.listView1.Items.Clear();
				this.listView1.Items.AddRange(this.listWord?this.currentWordGroup.LVIWord():this.currentWordGroup.LVIWordContent(this.xmlpart));
			}
		}
		public bool ListViewWord{
			get{return this.toolBar1.Buttons[0].Pushed;}
			set{
				if(this.listWord==value)return;
				this.listWord=value;
				this.toolBar1.Buttons[0].Pushed=value;
				this.toolBar1.Buttons[1].Pushed=!value;
				this.listView1.Items.Clear();
				if(value){
					this.listView1.Columns[1].Text="発音";
					this.listView1.SmallImageList=this.imageList1;
					if(this.currentWordGroup!=null)this.listView1.Items.AddRange(this.currentWordGroup.LVIWord());
				}else{
					this.listView1.Columns[1].Text="内容";
					this.listView1.SmallImageList=this.xmlpart.images;
					if(this.currentWordGroup!=null)this.listView1.Items.AddRange(this.currentWordGroup.LVIWordContent(this.xmlpart));
				}
			}
		}
		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e){
			this.ListViewWord=(e.Button.Text=="単語");
		}
		#endregion

		#region Showing in this.Browser
		private mshtml.HTMLDocument document{
			get{return (mshtml.HTMLDocument)this.Browser.Document;}
		}
		private void setHTMLToBrowser(string html){
			this.document.body.innerHTML=html;
		}
		private void listView1_SelectedIndexChanged(object sender, System.EventArgs e){
			if(this.listView1.SelectedItems.Count>0){
				System.Type type=this.listView1.SelectedItems[0].Tag.GetType();
				if(type==typeof(Hanyu.Word)){
					Hanyu.Word elem=(Hanyu.Word)this.listView1.SelectedItems[0].Tag;
					this.setHTMLToBrowser(elem.ToHTML(this.xmlpart));
				}else if(type.IsSubclassOf(typeof(Hanyu.Word.Content))){
					Hanyu.Word.Content content=(Hanyu.Word.Content)this.listView1.SelectedItems[0].Tag;
					this.setHTMLToBrowser(content.ParentElemWord.htmlTitle+"<hr/>"+content.ToHTML(this.xmlpart));
				}
			}
		}
		#endregion

		private void menuItem3_Click(object sender, System.EventArgs e){
			this.words.Save();
		}

		#region Entry to Class
		
		#endregion
	}
}
