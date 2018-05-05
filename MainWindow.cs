
using System;
using System.Drawing;
using System.Windows.Forms;
using SetUp.MyForm;
using SetUp.Model;
using SetUp.Common;


namespace SetUp.Main{
	public class Mainwindow: Form{
        private MonthCalendar calender;

		public Mainwindow(){
			Text = "学生成绩管理系统";
			Size = new Size (800, 800);
			//创建菜单一个
			MenuStrip menuSrtip = new MenuStrip();
            menuSrtip.Parent = this;
			ToolStripMenuItem start = new ToolStripMenuItem ("开始");
			ToolStripMenuItem SelectScore = new ToolStripMenuItem ("成绩查询");
            SelectScore.Click += OnSelect;

			ToolStripMenuItem InsertScore = new ToolStripMenuItem ("插入数据");
            InsertScore.Click += OnInsert;
            
            ToolStripMenuItem AnalyItem = new ToolStripMenuItem("成绩分析");
            AnalyItem.Click += OnAnaly;

			ToolStripMenuItem exit = new ToolStripMenuItem ("退出");
			exit.Click += OnExit;
            
			start.DropDownItems.Add (SelectScore);
			start.DropDownItems.Add (InsertScore);
            start.DropDownItems.Add(AnalyItem);
			start.DropDownItems.Add (new ToolStripSeparator ());
			start.DropDownItems.Add (exit);
			menuSrtip.Items.Add (start);
            MainMenuStrip = menuSrtip;
            calender = new MonthCalendar();
            calender.Parent = this;
            calender.Location = new Point(300, 150);
            calender.Size = new Size(300, 400);
            CenterToScreen();
		}

        protected void OnSelect(object obj, EventArgs args){
            FormSelect fs = new FormSelect();
            fs.ShowDialog();
        }
        protected void OnInsert(object obj, EventArgs args){
            FormInsert fi = new FormInsert();
            fi.ShowDialog();
        }
        protected void OnAnaly(object  obj, EventArgs args){
            FormScoreAnaly fsa = new FormScoreAnaly();
            fsa.ShowDialog();
        }
		protected void OnExit(object obj, EventArgs args){
			Close ();
		}
	}
}
